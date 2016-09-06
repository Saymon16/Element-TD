using UnityEngine;
using System.Collections;

public class Projectile : MonoBehaviour {

	public Transform origin;
	public Transform target;
	public float speed;
	public float damage;

	void Update () {
		if (target != null) {
			Vector3 dir = (target.transform.position - transform.position).normalized;
			transform.Translate(dir * speed * Time.deltaTime);
		} else {
			Destroy(gameObject);
		}
	}
}
