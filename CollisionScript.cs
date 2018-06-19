using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/*
 * -----------------------------------------------------------------------------------------------------------------------------------------------------------
 * Class not implemented
 * -----------------------------------------------------------------------------------------------------------------------------------------------------------
 * 
 * */

public class CollisionScript : MonoBehaviour {

	public GameObject grenadeObject;
	SphereCollider grenadeRadius;




	void OnTriggerEnter(Collider other){
		Debug.Log ("Actually working");
		if (other.gameObject.tag == "Grenade") {
			Debug.Log ("grenade hit floor");
			grenadeRadius.isTrigger = true;
			grenadeRadius.radius = 10f;
			grenadeRadius.center = Vector3.zero;
			SphereCollider temp = Instantiate (grenadeRadius, grenadeObject.transform.position, grenadeObject.transform.rotation);
			temp.gameObject.tag = "Grenade";
		}
	}
}
