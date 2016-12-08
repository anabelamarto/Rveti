using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class EventTestScript : MonoBehaviour {

	private UnityAction someListener;

	void Awake(){
		someListener = new UnityAction (SomeFunction);
	}

	void OnEnable(){
		EventManagerScript.StartListening ("test", someListener);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("test", someListener);
	}

	void SomeFunction()
	{
		Debug.Log ("Some Funcion was called!");
	}
}
