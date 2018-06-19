using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class SpawnScript : MonoBehaviour {

	public float spawnTime = 4f;
	public Transform[] spawnPoints;
	public GameObject enemyObject;
	public int maxEnemies = 10;
	int currentEnemies = 0;
	int currentKilled = 0;
	public bool hardMode;
	string myScene;

	Scene activeScene;
	GameObject playerOb;
	HealthScript pc;
	EnemyController ec;


	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Gets active scene and if scene is equal to tutorial then hardmode is always false
	//it will then see if the player pref hard mode is true and then set the difficulty accordingly
	//It then assigns the objects and repeatedly spawns enemies
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Start () {
		activeScene = SceneManager.GetActiveScene ();
		myScene = activeScene.name;

		if (myScene == "Tutorial") {
			hardMode = false;
			Debug.Log ("Tutorial hard mode off");
		}

		int h = PlayerPrefs.GetInt("isHard");

		if (h == 0) {
			hardMode = false;
		} else {
			hardMode = true;
		}

		//Debug.Log(hardMode);
		playerOb = GameObject.FindGameObjectWithTag("Player");
		pc = playerOb.GetComponent<HealthScript> ();
		ec = enemyObject.GetComponent<EnemyController> ();

		InvokeRepeating ("Spawn", 0, spawnTime);
	}
	
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Used to spawn enemies, if the player is dead then it will not spawn enemies
	//It will get a random spawn point and then instantiate an enemy at that location
	//if the current enemies is the same as the max enemies, then the spawn invoke is cancelled 
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Spawn(){
		if (pc.health <= 0) {
			return;
		}

		int spawnPos = Random.Range (0, spawnPoints.Length);

		GameObject enemyClone = Instantiate (enemyObject, spawnPoints [spawnPos].position, spawnPoints [spawnPos].rotation);
		enemyClone.tag = "enemyClone";
		enemyClone.SetActive(true);
		currentEnemies++;
		if (currentEnemies >= maxEnemies)
			CancelInvoke ("Spawn");


	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//This method is used monitor the current enemies in the scene
	//It's also used to make the game progressively harder if the hardmode bool is true.
	//will increase max enemies every 5 kills
	//will increase enemies health by 2.5 every 10 kills
	//will increase enemies speed (up to maximum of 8) every 20 kills
	//Will also restart the invoking process if the current enemies is less than max enemies
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void removeFromCurrent(){
		currentEnemies--;
		currentKilled++;

		if (currentKilled % 5 == 0 && hardMode){
			maxEnemies++;
			//ec.health += 10f;
		}

		if (currentKilled % 10 == 0 && hardMode) {
			ec.health += 2.5f;
		}

		if (currentKilled % 20 == 0 && hardMode && ec.speed <= 8) {
			ec.speed += 1f;
		}


		if (currentEnemies < maxEnemies)
			InvokeRepeating ("Spawn", spawnTime, spawnTime);


	}
}
