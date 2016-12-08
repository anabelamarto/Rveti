using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private UnityAction shakeListener;

	public float shake = 0;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	void Awake(){
		shakeListener = new UnityAction (ActivateShake);
	}

	void Update () {
		if (shake > 0) {
			transform.localPosition = Random.insideUnitCircle * shakeAmount;
			transform.localPosition = new Vector3 (transform.localPosition.x, transform.localPosition.y, -10);
			shake -= Time.deltaTime * decreaseFactor;
		} else {
			shake = 0.0f;
		}
	}

	void ActivateShake()
	{
		shake = 1.0f;
	}

	void OnEnable(){
		EventManagerScript.StartListening ("cameraShake", shakeListener);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("cameraShake", shakeListener);
	}
}
