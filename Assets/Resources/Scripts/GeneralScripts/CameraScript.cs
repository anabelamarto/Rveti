using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class CameraScript : MonoBehaviour {

	private UnityAction shakeListener;

	private Camera cam;

	public float shake = 0;
	public float shakeAmount = 0.7f;
	public float decreaseFactor = 1.0f;

	public Transform target;
	public Vector3 myPosition;
	public Vector3 targetPosition;

	public float widthRatio;
	public float heightRatio;

	public float xMin;
	public float xMax;
	public float yMin;
	public float yMax;
	public float side;



	void Awake(){
		shakeListener = new UnityAction (ActivateShake);
		cam = GetComponent<Camera> ();
	}

	void Start(){
		float targetaspect = widthRatio / heightRatio;

		// determine the game widow's current aspect ratio
		float windowaspect = (float)Screen.width / (float)Screen.height;

		// current viewport height should be scaled by this amount
		float scaleHeight = windowaspect / targetaspect;

		// obtain camera component so we can modify its viewport
		Camera camera = GetComponent<Camera> ();

		//if scaled height is less than current height, add letterbox
		if (scaleHeight < 1.0f) {
			Rect rect = camera.rect;

			rect.width = 1.0f;
			rect.height = scaleHeight;
			rect.x = 0;
			rect.y = (1.0f - scaleHeight) / 2.0f;

			camera.rect = rect;
		} else {
			float scalewidth = 1.0f / scaleHeight;

			Rect rect = camera.rect;

			rect.width = scalewidth;
			rect.height = 1.0f;
			rect.x = (1.0f - scalewidth) / 2.0f;
			rect.y = 0;

			camera.rect = rect;
		}

		target = GameObject.FindGameObjectWithTag ("Player").transform;
	}

	void Update () {
		
	}

	void LateUpdate () {
		if (side == 10) {
			Camera.main.orthographicSize = 5.6f;
			transform.position = new Vector3 (Mathf.Clamp (target.transform.position.x, xMin + 10f, xMax - 10f), Mathf.Clamp (target.transform.position.y, yMin + 5.7f, yMax - 5.7f), -10f);
			myPosition = transform.localPosition;
			targetPosition = target.localPosition;
		} else {
			Camera.main.orthographicSize = 8f;
			transform.position = new Vector3 (Mathf.Clamp (target.transform.position.x, xMin + 14.25f, xMax - 14.25f), Mathf.Clamp (target.transform.position.y, yMin + 8f, yMax - 8f), -10f);
			myPosition = transform.localPosition;
			targetPosition = target.localPosition;
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

	public void setLimits(float _xMin, float _xMax, float _yMin, float _yMax, float _side){
		xMin = _xMin;
		xMax = _xMax;
		yMin = _yMin;
		yMax = _yMax;
		side = _side/2;
	}
}
