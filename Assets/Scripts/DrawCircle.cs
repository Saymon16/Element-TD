using UnityEngine;
using System.Collections;

public class DrawCircle : MonoBehaviour
{
	[Range(1f,40f)]
	public float radius;
	[Range(0.01f,2f)]
	public float threshold;
	public Vector3 offset;

	public int pointNumber;
	Vector2[] points;

	public float refreshTimer;
	float nextRefresh;

	void Start()
	{
		CalculateCircle();
		nextRefresh = refreshTimer + Time.time;
	}

	void CalculateCircle()
	{

		float pointNumberf = (2 * Mathf.PI * radius) / threshold;
		float degUnit = 360 / pointNumberf;
		pointNumber = Mathf.RoundToInt(pointNumberf);
		points = new Vector2[pointNumber];

		for (int i = 0; i < pointNumber; i++) {
			points[i].x = Mathf.Cos(i * degUnit * Mathf.Deg2Rad)*radius;
			points[i].y = Mathf.Sin(i * degUnit * Mathf.Deg2Rad)*radius;
		}
	}

	void Update()
	{
		if (nextRefresh <= Time.time) {
			nextRefresh = refreshTimer + Time.time;
			CalculateCircle();
		}

		for (int i = 0; i < pointNumber - 1; i++) {	
			Vector3 p1 = new Vector3(transform.position.x + points[i].x, transform.position.y, transform.position.z + points[i].y);
			Vector3 p2 = new Vector3(transform.position.x + points[i + 1].x, transform.position.y, transform.position.z + points[i + 1].y);
			Debug.DrawLine(p1 + offset, p2 + offset, Color.grey);
		}
		Vector3 p3 = new Vector3(transform.position.x + points[pointNumber-1].x, transform.position.y, transform.position.z + points[pointNumber-1].y);
		Vector3 p4 = new Vector3(transform.position.x + points[0].x, transform.position.y, transform.position.z + points[0].y);
		Debug.DrawLine(p3 + offset, p4 + offset, Color.grey);
	}
}
