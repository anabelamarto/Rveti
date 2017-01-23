using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;
using System.Collections;

public class GameManagerScript : MonoBehaviour {

	public static HeroScript heroScript;
	private UnityAction altarActivationListener;
	private UnityAction altarDeactivationListener;
	private UnityAction endedGame;


	public bool paused;
	public bool altarActive;

	public RectTransform endPanel;

	void Awake(){
		altarActivationListener = new UnityAction (AltarActivation);
		altarDeactivationListener = new UnityAction (AltarDeactivation);
		endedGame = new UnityAction (ThanksPanel);
	}

	// Use this for initialization
	void Start () {
		heroScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<HeroScript> ();
		EventManagerScript.TriggerEvent ("activateRoom");
		EventManagerScript.TriggerEvent ("deactivateRoom");
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

	void OnEnable(){
		EventManagerScript.StartListening ("activateAltar", altarActivationListener);
		EventManagerScript.StartListening ("deactivateAltar", altarDeactivationListener);
		EventManagerScript.StartListening ("ended", endedGame);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("activateAltar", altarActivationListener);
		EventManagerScript.StopListening ("deactivateAltar", altarDeactivationListener);
		EventManagerScript.StopListening ("ended", endedGame);
	}

	void PauseGame(){
		paused = true;
	}

	void UnpauseGame(){
		paused = false;
	}

	void AltarActivation(){
		heroScript.isKneeling = true;
		heroScript.anim.ResetTrigger ("Reset");
		heroScript.anim.SetTrigger ("Kneel");
//		altarActive = true;
//		PauseGame ();
	}

	void AltarDeactivation(){
		heroScript.isKneeling = false;
		heroScript.anim.SetTrigger ("Reset");
		heroScript.anim.ResetTrigger ("Kneel");
//		altarActive = false;
//		UnpauseGame ();
	}

	void ThanksPanel(){
		StartCoroutine ("Thanks");
	}

	IEnumerator Thanks(){
		float timer = 0;
		endPanel.gameObject.SetActive (true);
		//alpha in
		while (timer < 5) {
			timer += Time.deltaTime;
			yield return null;
		}
		//wait a lil
		timer = 0;
		while (timer < 5) {
			timer += Time.deltaTime;
			yield return null;
		}
		Application.LoadLevel(Application.loadedLevel);
		yield return null;
	}
}
