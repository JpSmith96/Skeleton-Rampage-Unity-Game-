using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;


public class HealthScript : MonoBehaviour {
	public float health = 100f;
	public Text hpText,doubleXPText;
	public Text deadText;
	public ScoreController scoreController;
	public ParticleSystem nukeEffect;
	public AudioSource nukeSound;
	public Image crossHair;
	public bool isDoubleXP;
	public EnemyController ec;
	float timer;


	void Awake(){
		hpText.text = "Health: " + health.ToString ();


	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//if the doublexp bool is active the label for doubleXP is set to let the player know
	//if the timer is past 20 seconds then doublexp is deactivated
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void FixedUpdate(){
		doubleXPText.text = "";
		if (isDoubleXP) {
			doubleXPText.text = "DoubleXP Active!";

			timer += Time.deltaTime;

			if (timer >= 20) {
				isDoubleXP = false;
			}
		}

	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//if the player collides with a health pack and their health is not already at max, 10 hp will be restored
	//if the player collides with a doublexp pack then isDoubleXP is set to true and the timer is reset
	//if the player collides with the nuke, then the sound and particle effects are played and 1000 damage is dealt to all enemies currently active in the scene
	//(BUG: Sometimes causes multiple enemies to respawn on top of each other sending them into the sky)
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		if (other.gameObject.tag == "HealthPack") {
			Destroy (other.gameObject);
			//other.gameObject.SetActive (false);

			if (health < 100)
				health += 10;
			if (health > 100)
				health = 100;

			hpText.text = "Health: " + health.ToString ();


		}

		if (other.gameObject.tag == "doubleXp") {
			timer = 0;
			Destroy (other.gameObject);
			isDoubleXP = true;
			//other.gameObject.SetActive (false);


		}

		if (other.gameObject.tag == "Nuke") {
			Destroy (other.gameObject);
			nukeSound.Play ();
			//nukeEffect.gameObject.SetActive (true);
			nukeEffect.Play ();
			GameObject[] enemies = GameObject.FindGameObjectsWithTag("enemyClone");

			foreach (GameObject enemy in enemies) {
				enemy.GetComponent<EnemyController> ().TakeDamage (1000);

			}

			//other.gameObject.SetActive (false);


		}
	}
		
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Used to deal damage to the player, if the players hp is 0 the player will be killed
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void TakeDamage(float amount){
		health -= amount;
		hpText.text = "Health: " + health.ToString ();

		if (health <= 0f) {
			dead ();
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Destroys players game object, enables the death text, and disables the crosshair
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void dead(){
		if (health <= 0) {
			Destroy (gameObject);
			deadText.gameObject.SetActive (true);
			crossHair.gameObject.SetActive (false);
		}
	}
}
