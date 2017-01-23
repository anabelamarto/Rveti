using UnityEngine;
using System.Collections;

public class EventTriggerTestScript : MonoBehaviour {

	void Update () {
		if (Input.GetKeyDown ("q")) {
			EventManagerScript.TriggerEvent ("test");
			EventManagerScript.TriggerEvent ("cameraShake");
			EventManagerScript.TriggerEvent ("wineTrap");
			EventManagerScript.TriggerEvent ("floorDraw");
		}

		if (Input.GetKeyDown (KeyCode.T)) {
			EventManagerScript.TriggerEvent ("deactivateRoom");
		}

		if (Input.GetKeyDown (KeyCode.Y)) {
			EventManagerScript.TriggerEvent ("activateRoom");
		}
	}
}
