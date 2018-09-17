using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnManager : MonoBehaviour {

	public float spawnDelay;
	public int spawnPhase;
	public GameObject spawnPos;

	public GameObject slimePrefab;
	public GameObject bronzeKnightPrefab;
	public GameObject silverKnightPrefab;
	public GameObject goldKnightPrefab;

	private GameObject[] spawnTransforms;
	public static int enemyCount = 0;

	private int	slimeSpawnCount = 0;

	void Awake () {
		spawnTransforms = GameObject.FindGameObjectsWithTag("SpawnPos" + spawnPhase.ToString());
		StartCoroutine(SpawnEnemy ());
	}

	void Update() {
		if (spawnPhase == 0 && slimeSpawnCount == 4 && enemyCount == 0 || Input.GetKeyDown(KeyCode.Q)) {
			ChangePhase ();
		}
	}

	void ChangePhase() {
		GameObject[] walls = GameObject.FindGameObjectsWithTag("Wall" + spawnPhase.ToString());
		foreach (GameObject wall in walls)
			wall.SetActive(false);
		
		spawnPhase++;
		spawnTransforms = GameObject.FindGameObjectsWithTag("SpawnPos" + spawnPhase.ToString());

		GameObject[] walls2 = GameObject.FindGameObjectsWithTag("Wall" + spawnPhase.ToString());
		foreach (GameObject wall in walls2)
			wall.SetActive(true);

		GameObject area = GameObject.FindGameObjectWithTag("Area" + spawnPhase.ToString());
		for(int i = 0; i < area.transform.childCount; i++) 
		{ 
			Transform child = area.transform.GetChild(i); 
			child.gameObject.SetActive(true);
			FadeManager.instance.FaidIn (child.gameObject);
		}
	}

	IEnumerator SpawnEnemy()
	{
		while (true) {
			if (spawnPhase == 0 && slimeSpawnCount < 4) {
				GameObject slime = Instantiate (slimePrefab, spawnTransforms [Random.Range (0, spawnTransforms.Length)].transform.position, Quaternion.identity);
				FadeManager.instance.FaidIn (slime);
				slimeSpawnCount++;
				enemyCount++;
			}
			yield return new WaitForSeconds (spawnDelay);
		}
	}
}
