using UnityEngine;
using System.Collections;

public class EventTriggerTestScript : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown ("q")) {
			EventManagerScript.TriggerEvent ("test");
			EventManagerScript.TriggerEvent ("cameraShake");
			EventManagerScript.TriggerEvent ("wineTrap");
		}
	}
}
