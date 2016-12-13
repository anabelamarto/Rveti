using UnityEngine;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public static HeroScript heroScript;
	public GameObject altarPanel;

	public bool paused;

	// Use this for initialization
	void Start () {
		heroScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<HeroScript> ();
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

	public void ActivateAltarPanel(){
		PauseGame ();
		altarPanel.SetActive (true);
	}

	public void DeactivateAltarPanel(){
		UnpauseGame ();
		altarPanel.SetActive (false);
	}

	void PauseGame(){
		paused = true;
	}

	void UnpauseGame(){
		paused = false;
	}
}
