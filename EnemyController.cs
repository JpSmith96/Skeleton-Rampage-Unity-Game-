
using UnityEngine;
using UnityEngine.UI;


public class EnemyController : MonoBehaviour {
	public float health = 10f;
	public Transform player;
	public float speed = 4f;
	public float chargeDistance = 10f, attackDelay = 0.5f, damage = 10f;
	float timer, timer2;
	public ScoreController sc;
	public SpawnScript spawn;
	public GameObject healthPack, doubleScorePack, nukeObject;
	public GunScript gs;
	public AudioSource audio;
	public HealthScript hs;






	GameObject playerOb;
	Rigidbody rb;
	HealthScript pc;
	bool collided, isDoubleXp;



	Animator anim;
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//If the difficulty is hard, then the enemy speed is increased by 2
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Awake(){
		playerOb = GameObject.FindGameObjectWithTag("Player");
		anim = GetComponent<Animator>();
		pc = playerOb.GetComponent<HealthScript> ();
		rb = gameObject.GetComponent<Rigidbody> ();

		if (spawn.hardMode) {
			speed += 2;
		}




	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//if the players health is above 0, the timer is greater than or equal to the delay, and the player and the enemy have collided the attack function will be called
	//if collided is false then the attacking animation is set to false
	//if the enemy health is greater than 0 they will chase the player and their animation is set to running
	//otherwise the running animation is set to false
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void FixedUpdate(){
		timer += Time.deltaTime;




		if (pc.health > 0) {
			if (timer >= attackDelay && collided) {
				Attack ();
			}
			if (collided == false) {
				anim.SetBool ("isAttacking", false);

				if (health > 0) {
					anim.SetBool ("isRunning", true);
					Chase ();
				} else {
					anim.SetBool ("isRunning", false);


				}
			}

		} else {
			anim.SetBool ("isRunning", false);

		}






	}


	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//If the enemy collides with the player, the collided bool is set true
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void OnTriggerEnter(Collider other){
		if (other.gameObject == playerOb) {
			collided = true;
			//Debug.Log (other.gameObject.name);

		}

		if (other.gameObject.tag == "Grenade") {

			if (health > 0)
				gs.grenadeDamage (100);
		}
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Collided set to false when the player leaves the enemies trigger collider
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void OnTriggerExit(Collider other){
		if (other.gameObject == playerOb) {
			collided = false;
			//Debug.Log (other.gameObject.name);

		}
	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//deals damage to the enemy, if their health is less than or equal to 0, they will die
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	public void TakeDamage(float amount){
		health -= amount;
		if (health <= 0f) {
			Die ();
		}
	}
		
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//plays death sound and disables running animation
	//will destroy the enemies collider so the player cant deal damage to the enemy after they're dead
	//finds the enemies death position and stores it in healthPackPos which is used as the spawn point for all power-ups
	//creates 3 random number generators, one for each powerup
	//healthpack has 5% chance to spawn at enemies death location, doublexp is 2% chance, and nuke is 2% too
	//enemy is then set to kinematic to stop them moving and their death animation will play before destroying them after 2 seconds
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Die(){
		anim.SetBool ("isRunning", false);
		audio.Play ();
		Destroy (gameObject.GetComponent<CapsuleCollider> ());
		Vector3 healthPackPos = new Vector3 (transform.position.x, transform.position.y + 1, transform.position.z);
		System.Random rand = new System.Random ();
		int healthPackChance = rand.Next (0, 100);
		int doubleXpChance = rand.Next (0, 100);
		int nukeChance = rand.Next (0, 100);

		if (healthPackChance >= 95) {
			Instantiate (healthPack, healthPackPos, transform.rotation);
		}

		if (doubleXpChance >= 98) {
			Instantiate (doubleScorePack, healthPackPos, transform.rotation);

		}
		if (nukeChance >= 98) {
			Instantiate (nukeObject, healthPackPos, transform.rotation);

		}

		rb.isKinematic = true;
		if (hs.isDoubleXP) {
			sc.addToScore (20);
		}

		if (!(hs.isDoubleXP)) {
			sc.addToScore (10);
		}

		spawn.removeFromCurrent ();
		anim.SetTrigger ("isDead");
		Destroy(gameObject, 2f);
	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Looks at player and then runs towards them
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Chase(){
		
		transform.LookAt(player);

		transform.position += transform.forward * speed * Time.deltaTime;

	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Deals damage to the player as long as their health is greater than 0
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Attack(){		
		anim.SetBool("isAttacking", true);	
		timer = 0f;
		//Destroy (playerOb);
		if (pc.health > 0)
			pc.TakeDamage (damage);

	}
		
}
