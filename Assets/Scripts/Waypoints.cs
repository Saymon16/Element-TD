using UnityEngine;
using System.Collections;

public class Waypoints : MonoBehaviour
{
	public bool drawGizmos;
	Transform[] waypoints;


	public Transform GetNextWaypoint(Transform currentWaypoint)
	{
		if (currentWaypoint == null) {
			return this.transform.GetChild(0).transform;
		} else {
			if (currentWaypoint.GetSiblingIndex() < this.transform.childCount - 1) {
				return this.transform.GetChild(currentWaypoint.GetSiblingIndex() + 1).transform;
			} else {
				return null;
			}
		}
	}

	void Start()
	{
		waypoints = new Transform[this.transform.childCount];
		for (int i = 0; i < this.transform.childCount; i++) {
			waypoints[i] = this.transform.GetChild(i);
		}
	}

	void Update()
	{
		if (drawGizmos) {
			for (int i = 0; i < this.transform.childCount-1; i++) {				
				Debug.DrawLine(waypoints[i].transform.position, waypoints[i + 1].transform.position, Color.red);
			}
		}
	}
























	/*public Waypoint nextWaypoint;
	public bool isLast = false;
	public bool isFirst = false;
	public bool drawGizmos;

	void Update(){
		if (drawGizmos && !isLast)
			Debug.DrawLine(this.transform.position, nextWaypoint.transform.position, Color.red);
	}


	void OnTriggerEnter(Collider col)
	{
		if (col.tag == "Enemy") {
			
			Enemy e = col.GetComponent<Enemy>();
			if (!isLast) {
				//e.target = nextWaypoint;

			} else {
				
				//e.HasReachedEnd ();
			}
		}
	}
*/

		
	

}