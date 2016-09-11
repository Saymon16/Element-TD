using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class BuildingManager : MonoBehaviour
{

	public Transform tilesHolder;
	public GameObject[] towerPrefabs;
	Tile[] tiles;
	string lastChoice = "";
	public Slider slider;

	void Start()
	{
		tiles = new Tile[tilesHolder.childCount];
		for (int i = 0; i < tilesHolder.childCount; i++) {
			tiles[i].holder = tilesHolder.GetChild(i);
			tiles[i].free = true;
		}
	}

	void PlaceTower(GameObject towerPrefab, Transform parent)
	{
		if (tiles[parent.GetSiblingIndex()].free) {
			Instantiate(towerPrefab, parent.position, Quaternion.identity, parent);
			tiles[parent.GetSiblingIndex()].free = false;
		}
	}

	public void ChooseTower(string name)
	{
		lastChoice = name;
	}

	GameObject NameToPrefab(string name)
	{
		switch (name) {
			case "Splinter":
				return towerPrefabs[0];
				break;
			case "Droplet":
				return towerPrefabs[1];
				break;
			case "Ember":
				return towerPrefabs[2];
				break;
			case "Bullet":
				return towerPrefabs[3];
				break;
			case "Arrow":
				return towerPrefabs[4];
				break;
			case "Pebble":
				return towerPrefabs[5];
				break;
		}
		return null;
	}

	public void TimeScale(){
		switch ((int)slider.value) {
			case 0:
				Time.timeScale = .5f;
				break;
			case 1:
				Time.timeScale = 1f;
				break;
			case 2:
				Time.timeScale = 2f;
				break;
			case 3:
				Time.timeScale = 5f;
				break;
		}
	}

	int NameToPrice(string name)
	{
		int t1, t2, t3, t4;
		t1 = 50;
		t2 = 100;
		t3 = 350;
		t4 = 500;
		switch (name) {
			case "Splinter":
				return t1;
				break;
			case "Droplet":
				return t1;
				break;
			case "Ember":
				return t1;
				break;
			case "Bullet":
				return t1;
				break;
			case "Arrow":
				return t1;
				break;
			case "Pebble":
				return t1;
				break;
		}
		return 0;
	}

	void Update()
	{
		if (Input.GetMouseButtonDown(0)) {
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			RaycastHit hit;
			if (Physics.Raycast(ray, out hit)) {
				if (hit.collider.transform.tag == "BuildingTile" && lastChoice != "") {					
					PlaceTower(NameToPrefab(lastChoice), hit.transform.parent);
					// gold -= NameToPrice(lastChoice);
				}
			}
		}
	}
}

[System.Serializable]
public struct Tile
{
	public Transform holder;
	public bool free;
}
