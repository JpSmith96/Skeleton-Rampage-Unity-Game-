
using UnityEngine;

public class GunScript : MonoBehaviour {
	public float damage = 10f, range = 100f, impactForce = 30f;
	public Camera cam;
	public ParticleSystem muzzle;
	public GameObject impact;
	public int grenadeCount = 6;
	public GameObject Grenade;
	EnemyController ec;
	Rigidbody rb;
	public GameObject enemy;
	float timer;
	public AudioSource audio;
	GameObject clone;
	BoxCollider grenadeBox;
	Collider[] exploRadious;


	// Update is called once per frame

	void Start(){
		rb = Grenade.GetComponent<Rigidbody> ();
		ec = enemy.GetComponent<EnemyController> ();


	}

	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//If the player presses their fire1 button and the timescale is greater than 0.1 the player's gun will fire
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Update () {
		//throwGrenade ();
		

		if (Input.GetButtonDown ("Fire1") && Time.timeScale > 0.1) {
			Shoot ();
		}
	}


	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Plays audio and particle effects
	//Fires a raycast forward from the cameras position and forward rotation and stores the info in hit as long as the object is in range.
	//If the raycast hits an enemy object, it will deal damage to the enemy and apply a negative direction force to the enemy
	//It will also instantiate a particle effect where the raycast hits to allow the player to see where their shots land
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void Shoot(){
		audio.Play ();
		muzzle.Play();
		RaycastHit hit;
		if (Physics.Raycast (cam.transform.position, cam.transform.forward, out hit, range)) {

			EnemyController target = hit.transform.GetComponent<EnemyController> ();
			if (target != null && target.health > 0) {
				target.TakeDamage (damage);
			}

			if (hit.rigidbody != null && hit.rigidbody.name != "Grenade") {
				hit.rigidbody.AddForce (-hit.normal * impactForce);
			}
			GameObject impactOb = Instantiate (impact, hit.point, Quaternion.LookRotation (hit.normal));
			Destroy (impactOb, 2f);


		}

	}
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	//Not implemented
	//------------------------------------------------------------------------------------------------------------------------------------------------------------------------------
	void throwGrenade(){
		if (Input.GetKeyDown (KeyCode.G) && grenadeCount > 0) {			
			grenadeCount--;

			clone = Instantiate (Grenade, transform.position, transform.rotation);

			//rb2.velocity = new Vector3 (rb2.transform.forward.x * hSpeed, vSpeed, rb2.transform.forward.z * hSpeed);
			clone.GetComponent<Rigidbody> ().AddForce ((transform.up * 500) + (transform.forward * 1000));
			//grenadeBox = clone.GetComponent<BoxCollider> ();


			//grenadeBox.enabled = true;
			//grenadeBox.isTrigger = true;
			//exploRadious = Physics.OverlapSphere(clone.transform.position,20);



			Destroy (clone,5f);

		}






	}

	public void grenadeDamage(float amount){
		ec.TakeDamage (amount);
	}
		
		


}
