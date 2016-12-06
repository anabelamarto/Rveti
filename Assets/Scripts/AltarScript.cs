using UnityEngine;
using System.Collections;

public class AltarScript : MonoBehaviour {

	GameManagerScript gameManagerScript;

	public bool inReach;

	// Use this for initialization
	void Awake () {
		gameManagerScript = GameObject.FindGameObjectWithTag ("GameManager").GetComponent<GameManagerScript> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (inReach) {
			if(Input.GetKeyDown(KeyCode.E)){
				gameManagerScript.ActivateAltarPanel ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			inReach = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			inReach = false;
		}
	}
}
