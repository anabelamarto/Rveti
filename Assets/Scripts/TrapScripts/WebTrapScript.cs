using UnityEngine;
using System.Collections;

public class WebTrapScript : MonoBehaviour {

	public float speedModifier;

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && !GameManagerScript.heroScript.steppingOnWeb) {
			GameManagerScript.heroScript.steppingOnWeb = true;
			GameManagerScript.heroScript.movementSpeed -= speedModifier;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player" && GameManagerScript.heroScript.steppingOnWeb) {
			GameManagerScript.heroScript.movementSpeed += speedModifier;
			GameManagerScript.heroScript.steppingOnWeb = false;
		}
	}
}
