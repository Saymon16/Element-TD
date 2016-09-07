using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveSpawner : MonoBehaviour
{


	public WaveGenerator waveGenerator;
	public Transform spawnPoint;
	public Transform minionHolder;
	public int enemyCount;
	public float timeBetweenSpawns;
	public float timeBetweenWaves;
	public GameObject minionPrefab;
	public int initialScore = 100;
	Wave waveHolder;
	float nextSpawn;
	int waveIndex;
	int currentWaveSpawnsRemaining;
	bool canSpawn = true;
	bool noMoreWaves = false;
	int lastScore;
	float waveNumberInc;
	int waveCount;
	void Start()
	{
		waveHolder = new Wave();
		waveNumberInc = 3f;
		lastScore = initialScore;
		InitWaves(Mathf.RoundToInt(waveNumberInc),initialScore);
		nextSpawn = timeBetweenSpawns + Time.time;
		waveIndex = 0;
		currentWaveSpawnsRemaining = waveHolder.waves[0].number;
	}

	void InitWaves(int wavesNumber,int score){		
		waveHolder.waves = new WaveAttribute[wavesNumber];
		for (int i = 0; i < wavesNumber; i++) {
			waveHolder.waves[i] = waveGenerator.GenerateRound(score);
		}
	}

	void Update()
	{
		if (noMoreWaves) {
			waveNumberInc += 0.4f;
			lastScore += (5*waveCount);
			InitWaves(Mathf.RoundToInt(waveNumberInc),Mathf.RoundToInt(lastScore));
			nextSpawn = timeBetweenSpawns + Time.time;
			waveIndex = 0;
			currentWaveSpawnsRemaining = waveHolder.waves[0].number;
			noMoreWaves = false;
		}

		enemyCount = minionHolder.childCount;
		if (!canSpawn && enemyCount == 0) {
			canSpawn = true;
			nextSpawn = timeBetweenWaves + Time.time;
		}
		if (Time.time > nextSpawn && canSpawn && !noMoreWaves) {
			nextSpawn = Time.time + timeBetweenSpawns;
			if (currentWaveSpawnsRemaining > 0) {
				SpawnNextMinion();
				currentWaveSpawnsRemaining--;
			} else {
				canSpawn = false;
				if (waveIndex < waveHolder.waves.Length-1) {
					waveIndex++;
					waveCount++;
					currentWaveSpawnsRemaining = waveHolder.waves[waveIndex].number;
				} else {
					noMoreWaves = true;
				}
			}
		}
	}

	public void SendWaveEarly()
	{
		if (!canSpawn) {
			canSpawn = true;
		}
	}

	void SpawnNextMinion()
	{
		GameObject g = Instantiate(minionPrefab, spawnPoint.position, Quaternion.identity, minionHolder) as GameObject;
		Enemy e = g.GetComponent<Enemy>();
		Material m = g.transform.GetChild(0).GetComponent<MeshRenderer>().material;
		m.color = waveHolder.waves[waveIndex].color;
		g.transform.localScale = Vector3.one * waveHolder.waves[waveIndex].size;
		g.transform.position = new Vector3(g.transform.position.x, waveHolder.waves[waveIndex].size * 0.25f, g.transform.position.z);
		e.speed = waveHolder.waves[waveIndex].speed;
		e.health = waveHolder.waves[waveIndex].health;
		e.gold = waveHolder.waves[waveIndex].gold;
		switch (waveHolder.waves[waveIndex].element) {
			case WaveAttribute.Element.N:
				e.element = Enemy.Element.N;
				break;
			case WaveAttribute.Element.M:
				e.element = Enemy.Element.M;
				break;
			case WaveAttribute.Element.T:
				e.element = Enemy.Element.T;
				break;
			case WaveAttribute.Element.F:
				e.element = Enemy.Element.F;
				break;
			case WaveAttribute.Element.B:
				e.element = Enemy.Element.B;
				break;
			case WaveAttribute.Element.E:
				e.element = Enemy.Element.E;
				break;		
		}
	}






}
