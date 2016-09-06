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
	float damage;
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
					ShootAt(target, damage);
				}
			} else {
				target = null;
			}
		} else {
			if (Time.time >= nextTargetScan) {
				nextTargetScan = 0.5f + Time.time;
				ChooseNextTarget();
				damage = CalculateDamage();
			}
		}
	}


	float EvaluateElementCoefficient(Enemy.Element e, Turret.Element t)
	{
		switch (e) {
			case Enemy.Element.N:
				return 1f;
				break;
			case Enemy.Element.M:
				switch (t) {
					case Turret.Element.N:
						return 1f;
						break;
					case Turret.Element.M:
						return 1f;
						break;
					case Turret.Element.T:
						return 0.8f;
						break;
					case Turret.Element.F:
						return 1.2f;
						break;
					case Turret.Element.B:
						return 0.8f;
						break;
					case Turret.Element.E:
						return 1.2f;
						break;			
				}
				break;
			case Enemy.Element.T:
				switch (t) {
					case Turret.Element.N:
						return 1f;
						break;
					case Turret.Element.M:
						return 1.2f;
						break;
					case Turret.Element.T:
						return 1f;
						break;
					case Turret.Element.F:
						return 0.8f;
						break;
					case Turret.Element.B:
						return 1.2f;
						break;
					case Turret.Element.E:
						return 0.8f;
						break;			
				}
				break;
			case Enemy.Element.F:
				switch (t) {
					case Turret.Element.N:
						return 1f;
						break;
					case Turret.Element.M:
						return 0.8f;
						break;
					case Turret.Element.T:
						return 1.2f;
						break;
					case Turret.Element.F:
						return 1f;
						break;
					case Turret.Element.B:
						return 0.8f;
						break;
					case Turret.Element.E:
						return 1.2f;
						break;			
				}
				break;
			case Enemy.Element.B:
				switch (t) {
					case Turret.Element.N:
						return 1f;
						break;
					case Turret.Element.M:
						return 1.2f;
						break;
					case Turret.Element.T:
						return 0.8f;
						break;
					case Turret.Element.F:
						return 1.2f;
						break;
					case Turret.Element.B:
						return 1f;
						break;
					case Turret.Element.E:
						return 0.8f;
						break;			
				}
				break;
			case Enemy.Element.E:
				switch (t) {
					case Turret.Element.N:
						return 1f;
						break;
					case Turret.Element.M:
						return 0.8f;
						break;
					case Turret.Element.T:
						return 1.2f;
						break;
					case Turret.Element.F:
						return 0.8f;
						break;
					case Turret.Element.B:
						return 1.2f;
						break;
					case Turret.Element.E:
						return 1f;
						break;			
				}
				break;			
		}
	}

	float CalculateDamage()
	{
		Enemy e = target.GetComponent<Enemy>();
		float coef;
		float c1, c2, c3;
		c1 = EvaluateElementCoefficient(e.element, this.first);
		c2 = EvaluateElementCoefficient(e.element, this.second);
		c3 = EvaluateElementCoefficient(e.element, this.third);
		switch (tier) {
			case Tier.T1:
				coef = c1;
				break;
			case Tier.T2:
				coef = c1;
				break;
			case Tier.T3:
				coef = Mathf.Max(c1, c2);
				break;
			case Tier.T4:
				coef = Mathf.Max(c1, Mathf.Max(c2, c3));
				break;
		}
		return coef * baseDamage;
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

	void ShootAt(Transform t, float d)
	{
		GameObject g = Instantiate(projectilePrefab, new Vector3(transform.position.x, transform.position.y + projectileOffsetY, transform.position.z), Quaternion.identity) as GameObject;
		Projectile p = g.GetComponent<Projectile>();
		p.origin = this.transform;
		p.damage = d;
		p.speed = projectileSpeed;
		p.target = t;
	}

}
