using UnityEngine;
using System.Collections;

[System.Serializable]
public class WaveAttribute
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
	public int number;
	public float speed;
	public float health;
	public Color color;
	public float size = 1;
	public int gold;
}


