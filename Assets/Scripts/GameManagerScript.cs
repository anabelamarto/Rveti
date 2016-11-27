using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public bool paused;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetKeyDown (KeyCode.P)) {
			paused = !paused;
		}

		if (paused) {
			Time.timeScale = 0;
		} else {
			Time.timeScale = 1;
		}
	}

	void FixedUpdate(){
		
	}
}
