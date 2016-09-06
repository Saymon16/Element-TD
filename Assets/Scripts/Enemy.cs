using UnityEngine;
using System;
using System.Collections;

public class Enemy : MonoBehaviour
{
	public enum Element
	{

		N,
		M,
		T,
		F,
		B,
		E}

	;
	public Element element;
	public float speed;
	public float health;

	public event Action reachedEndEvent;
	public event Action diedEvent;

	Transform targetWaypoint = null;
	Waypoints wp;

	// Use this for initialization
	void Start()
	{
		wp = GameObject.FindObjectOfType<Waypoints>();
		targetWaypoint = wp.GetNextWaypoint(targetWaypoint);
	}
	
	// Update is called once per frame
	void Update()
	{		
		if (targetWaypoint != null) {
			Move();
		} else {
			if (reachedEndEvent != null) {
				reachedEndEvent();
			}
			Destroy(gameObject);
		}
	}

	public void Die()
	{
		if (diedEvent != null) {
			diedEvent();
		}
		Destroy(gameObject);
	}

	void Move()
	{
		Vector3 dir = (targetWaypoint.transform.position - transform.position).normalized;
		dir = new Vector3(dir.x, 0, dir.z);
		transform.Translate(dir * Time.deltaTime * speed);
	}

	public void TakeHit(float amount,Transform origin){
		health -= amount;
		if (health <= 0) {
			origin.GetComponent<Turret>().killCount++;
			Die();
		}
	}

	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "WP") {
			targetWaypoint = wp.GetNextWaypoint(targetWaypoint);
		}
		if (col.tag == "Projectile") {
			Projectile p = col.GetComponent<Projectile>();
			TakeHit(p.damage,p.origin);
			Destroy(col.gameObject);
		}
	}
}
