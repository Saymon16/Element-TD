using UnityEngine;
using System.Collections;

public class Turret : MonoBehaviour
{

	public enum Tier
	{
		T1,
		T2,
		T3,
		T4}

	;

	public enum Element
	{
		
		N,
		M,
		T,
		F,
		B,
		E,
		O}

	;


	public Tier tier;
	public Element first;
	public Element second;
	public Element third;

	public int killCount;

	public float timeBetweenShots;
	float nextShot;
	public float range = 10f;
	public float projectileSpeed;
	public int baseDamage;
	public GameObject projectilePrefab;
	public float projectileOffsetY;
	float nextTargetScan;
	float sqrRange;
	Transform minionHolder;
	Transform target;


	void Start()
	{
		minionHolder = GameObject.FindObjectOfType<WaveSpawner>().minionHolder;
		sqrRange = range * range;
		target = null;
		nextShot = timeBetweenShots + Time.time;
		nextTargetScan = Time.time + 0.5f;
	}


	void Update()
	{
		if (target != null) {
			if (InRange(target)) {
				if (Time.time >= nextShot) {
					nextShot = timeBetweenShots + Time.time;
					ShootAt(target);
				}
			} else {
				target = null;
			}
		} else {
			if (Time.time >= nextTargetScan) {
				nextTargetScan = 0.5f + Time.time;
				ChooseNextTarget();
			}
		}
	}

	float CalculateDamage()
	{
		return (float)baseDamage;
	}

	bool InRange(Transform t)
	{
		float sqrDist = (t.position.x - transform.position.x) * (t.position.x - transform.position.x) + (t.position.z - transform.position.z) * (t.position.z - transform.position.z);
		return sqrDist <= sqrRange;
	}

	void ChooseNextTarget()
	{
		target = null;
		for (int i = 0; i < minionHolder.childCount; i++) {
			if (InRange(minionHolder.GetChild(i).transform)) {
				target = minionHolder.GetChild(i).transform;
				break;			 
			}
		}
	}

	void ShootAt(Transform t)
	{
		GameObject g = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + projectileOffsetY, transform.position.z), Quaternion.identity) as GameObject;
		Projectile p = g.GetComponent<Projectile>();
		p.origin = this.transform;
		p.damage = CalculateDamage();
		p.speed = projectileSpeed;
		p.target = t;
	}

}
