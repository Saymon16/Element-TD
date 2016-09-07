using UnityEngine;
using System.Collections;

public class WaveGenerator : MonoBehaviour
{

	public float numberC;
	public float healthC;
	public float speedC;

	public Kind[] kinds;

	public WaveAttribute GenerateRound(int score)
	{
		int r, tmpScore;
		tmpScore = score;
		WaveAttribute w = new WaveAttribute();

		r = Random.Range(0, 6);
		w.color = kinds[r].color;
		w.element = kinds[r].element;

		r = Random.Range(1, 30);
		w.number = r;
		tmpScore -= Mathf.RoundToInt((float)r / numberC);

		float s = Random.Range(0.2f, 3f);
		w.speed = s;
		tmpScore -= Mathf.RoundToInt(s / speedC);

		w.health = Mathf.RoundToInt(tmpScore / healthC);

		w.size = Mathf.Clamp(((s - 3.7f) / -3.5f), 0.2f, 1.5f);

		w.gold = Mathf.RoundToInt(Mathf.Max (1,(float)score * 0.2f / w.number));

		w.score = Mathf.RoundToInt((r * numberC) + (w.health * healthC) + (s * speedC));

		return w;

	}




}

[System.Serializable]
public struct Kind
{
	public WaveAttribute.Element element;
	public Color color;
}