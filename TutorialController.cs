using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class TutorialController : MonoBehaviour {

	public Text tutorialText;
	public GameObject Trigger1, Trigger2;
	int tutorialStage = 0;
	float timer = 0;
	void Start(){

	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//If the player presses left click a timer is started then every 15 seconds the message is replaced with a new one
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update(){
		if (Input.GetButtonDown ("Fire1") || tutorialStage >= 1) {
			timer += Time.deltaTime;

			tutorialStage = 1;
			tutorialText.text = "Shoot the Skeletons using the fire1 button";
			Trigger1.SetActive (false);

			if (timer >= 15) {
				tutorialText.text = "Skeletons will deal 10 damage to you, When your health is 0, you die!";
			}

			if (timer >= 30) {
				tutorialText.text = "Red boxes restore 10hp until you're at 100 again (5% chance per kill) \n" + "Blue boxes give you double XP for 20 seconds (2% chance per kill) \n" +
					"These Boxes will be dropped by enemies when they die";
				Trigger2.SetActive (false);
			}

			if (timer >= 45) {
				tutorialText.text = "Thers is also a 2% chance per kill for an enemy to drop a nuke, which will kill all curently spawned enemies, giving you a moment to recover";

			}

			if (timer >= 60) {
				tutorialText.text = "If you wish to quit the tutorial, press ESC and click main menu. You're welcome to stay in the tutorial for as long as you want";

			}


		}
	}




}
