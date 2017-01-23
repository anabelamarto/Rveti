using UnityEngine;
using System.Collections;

public class WineTrapScript : MonoBehaviour {

	public bool triggered = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			if (!triggered) {
				triggered = true;
				EventManagerScript.TriggerEvent ("wineTrap");
			}
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			triggered = false;
		}
	}
}
