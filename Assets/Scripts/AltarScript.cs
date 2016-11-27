using UnityEngine;
using System.Collections;

public class AltarScript : MonoBehaviour {

	public bool inReach;

	// Use this for initialization
	void Awake () {
	
	}
	
	// Update is called once per frame
	void Update () {
		
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
