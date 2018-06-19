using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;



public class PauseController : MonoBehaviour {
	public bool paused;
	public Button res, exit, mainMenu, restart;
	public HealthScript hp;
	public ScoreController scoreControl;
	Scene activeScene;
	string myScene;

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//initialises pause as false and gets the name of the active scene
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Start () {
		paused = false;
		activeScene = SceneManager.GetActiveScene();
		myScene = activeScene.name;

		Resume ();

	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//if the player presses escape, the game is equal to the opposite of the pause bool
	//if the player dies, the death menu will appear.
	//if the pause bool is true the timescale will be set to 0, the cursor will be unlocked and the pause buttons will be activated, if false then the opposite will happen
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update ()
	{
		
		if(Input.GetKeyDown(KeyCode.Escape)){
			paused = !paused;


		}

		if (hp.health <= 0) {	
			paused = true;
			dead ();
		}
			

		if (paused) {
			Time.timeScale = 0;
			Cursor.lockState = CursorLockMode.None;
			Cursor.visible = true;
			res.gameObject.SetActive (true);
			exit.gameObject.SetActive (true);
			mainMenu.gameObject.SetActive (true);



		} else if (!paused) {
			res.gameObject.SetActive (false);
			exit.gameObject.SetActive (false);
			mainMenu.gameObject.SetActive (false);
			Cursor.lockState = CursorLockMode.Locked;
			Cursor.visible = false;
			Time.timeScale = 1;

		}
	}
		
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Stops timescale and activates the exit and restart buttons
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void dead(){
		Time.timeScale = 0;
		Cursor.lockState = CursorLockMode.None;
		Cursor.visible = true;
		restart.gameObject.SetActive (true);
		exit.gameObject.SetActive (true);
	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//restarts the timescale and locks cursor and removes menu buttons
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Resume(){
		paused = false;
		Time.timeScale = 1;
		Cursor.lockState = CursorLockMode.Locked;
		Cursor.visible = false;
		res.gameObject.SetActive (false);
		exit.gameObject.SetActive (false);
		mainMenu.gameObject.SetActive (false);

	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Quits the game and set new highscore
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Exit(){
		scoreControl.updateHighScore ();
		Application.Quit ();
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Returns to main menu and will update high score
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void MainMenu(){
		scoreControl.updateHighScore ();

		SceneManager.LoadScene (0);

	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Restarts active scene
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void RestartGame(){
		SceneManager.LoadScene (myScene);
	}

}
