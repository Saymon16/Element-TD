using UnityEngine;
using System;
using System.Collections;
using UnityEngine.UI;

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

	public float score;
	public Element element;
	public float gold;
	public float speed;
	public float health;
	public float maxhealth;
	public Image healthBar;

	public event Action reachedEndEvent;
	public event Action diedEvent;

	Transform targetWaypoint = null;
	Waypoints wp;

	// Use this for initialization
	void Start()
	{
		maxhealth = health;
		wp = GameObject.FindObjectOfType<Waypoints>();
		targetWaypoint = wp.GetNextWaypoint(targetWaypoint);
	}
	
	// Update is called once per frame
	void Update()
	{		
		RefreshHealthBar();
		if (targetWaypoint != null) {
			Move();
		} else {
			if (reachedEndEvent != null) {
				reachedEndEvent();
			}
			Destroy(gameObject);
		}
	}

	void RefreshHealthBar()
	{
		if (health == maxhealth) {
			healthBar.rectTransform.parent.gameObject.SetActive(false);
		} else {
			healthBar.rectTransform.parent.gameObject.SetActive(true);
		}
		float scale = Mathf.Clamp01(health / maxhealth);
		healthBar.rectTransform.localScale = new Vector3(scale, 1, 1);
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

	public void TakeHit(float amount, Transform origin)
	{
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
			TakeHit(p.damage, p.origin);
			col.gameObject.GetComponent<Renderer>().enabled = false;
			col.gameObject.GetComponent<SphereCollider>().enabled = false;
			col.gameObject.GetComponent<ParticleSystem>().Stop();
		}
	}
}
