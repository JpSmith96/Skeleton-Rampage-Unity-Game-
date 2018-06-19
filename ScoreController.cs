using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class ScoreController : MonoBehaviour {
	public float score = 0f;
	public Text scoreText, highScoreText;
	float highScore;
	HealthScript hp;
	GameObject Player;
	Scene activeScene;
	string myScene;

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Sets the high score value to the current scene
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Awake () {
		activeScene = SceneManager.GetActiveScene ();
		myScene = activeScene.name;

		if (myScene == "Main") {
			highScore = PlayerPrefs.GetFloat ("HighScore");
		} else if (myScene == "Second") {
			highScore = PlayerPrefs.GetFloat ("HighScoreCity");
		}

		scoreText.text = "Score: " + score.ToString ();
		highScoreText.text = "High Score: " + highScore.ToString ();
		Player = GameObject.FindGameObjectWithTag ("Player");
		hp = Player.GetComponent<HealthScript> ();

		
	}
	
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//used to display current score and then if the player dies, to update the highscore
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update () {
		getScore ();
		if (hp.health <= 0) {
			updateHighScore ();
		}
	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//will update the highscore depending on the current scene, only if the curresnt score is higher than the saved highscore 
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void updateHighScore(){
		if (PlayerPrefs.GetFloat ("HighScore") < score && myScene == "Main") {
			PlayerPrefs.SetFloat ("HighScore", score);
		} else if (PlayerPrefs.GetFloat ("HighScoreCity") < score && myScene == "Second") {
			PlayerPrefs.SetFloat ("HighScoreCity", score);

		}
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Returns current score
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void getScore(){
		scoreText.text = "Score: " + score.ToString ();

	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//chaneges the value of score after each kill
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void addToScore(float amount){
		score += amount;

	}
}
