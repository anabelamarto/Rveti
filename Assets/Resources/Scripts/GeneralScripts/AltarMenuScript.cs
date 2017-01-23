using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class AltarMenuScript : MonoBehaviour {

	public GameObject altarPanel;

	private UnityAction altarActivationListener;

	void Awake(){
		altarActivationListener = new UnityAction (ActivateAltarPanel);
	}

	public void ActivateAltarPanel(){
		//altarPanel.SetActive (true);
	}

	public void DeactivateAltarPanel(){
		altarPanel.SetActive (false);
	}

	void OnEnable(){
		EventManagerScript.StartListening ("activateAltar", altarActivationListener);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("activateAltar", altarActivationListener);
	}

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
