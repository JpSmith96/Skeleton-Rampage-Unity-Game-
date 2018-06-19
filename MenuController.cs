using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

using UnityEngine.SceneManagement;


public class MenuController : MonoBehaviour {

	public Button easy, hard, play, options, exit, goBack, subButton, roofButton, tutButton;
	public Slider volumeSlide;
	public Text volumeText;
	static int isHard;

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Disables all other menu buttons apart from the difficulty buttons and the go back button
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void DifficultySelect(){
		play.gameObject.SetActive (false);
		options.gameObject.SetActive (false);
		exit.gameObject.SetActive (false);
		easy.gameObject.SetActive (true);
		hard.gameObject.SetActive (true);
		tutButton.gameObject.SetActive (false);
		goBack.gameObject.SetActive (true);

	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Disables all buttons apart from play, options, exit, and tutorial
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void GoBack(){
		play.gameObject.SetActive (true);
		options.gameObject.SetActive (true);
		exit.gameObject.SetActive (true);
		tutButton.gameObject.SetActive (true);

		easy.gameObject.SetActive (false);
		hard.gameObject.SetActive (false);
		goBack.gameObject.SetActive (false);
		volumeSlide.gameObject.SetActive (false);
		volumeText.gameObject.SetActive (false);
		subButton.gameObject.SetActive (false);
		roofButton.gameObject.SetActive (false);

	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Changes the game volume based on the slider in the options menu
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void OnValueChanged(){
		AudioListener.volume = volumeSlide.value;
	}


	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Sets hard to false and then sets the player prefs int "ishard" to the is hard int
	//then moves player onto the map selections stage
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void StartGameEasy(){
		isHard = 0;
		PlayerPrefs.SetInt ("isHard", isHard);
		easy.gameObject.SetActive (false);
		hard.gameObject.SetActive (false);
		subButton.gameObject.SetActive (true);
		roofButton.gameObject.SetActive (true);



	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Loads the tutorial scene.
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Tutorial(){
		SceneManager.LoadScene (3);
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Loads the suburb scene named "Main"
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Suburbia(){
		SceneManager.LoadScene (1);
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//loads the roof scene named "Secondary"
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Roof(){
		SceneManager.LoadScene (2);
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//same as start game easy but the isHard int is true
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void StartGameHard(){
		isHard = 1;
		PlayerPrefs.SetInt ("isHard", isHard);
		easy.gameObject.SetActive (false);
		hard.gameObject.SetActive (false);
		subButton.gameObject.SetActive (true);
		roofButton.gameObject.SetActive (true);

	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Quits the game
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void ExitGame(){
		Application.Quit ();
	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Disables all buttons and activates the volume slider and go back button
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void Options(){
		play.gameObject.SetActive (false);
		options.gameObject.SetActive (false);
		tutButton.gameObject.SetActive (false);

		exit.gameObject.SetActive (false);
		easy.gameObject.SetActive (false);
		hard.gameObject.SetActive (false);
		volumeSlide.gameObject.SetActive (true);
		volumeText.gameObject.SetActive (true);
		goBack.gameObject.SetActive (true);
	}

}