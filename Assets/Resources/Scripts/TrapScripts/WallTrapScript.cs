using UnityEngine;
using System.Collections;

public class WallTrapScript : MonoBehaviour {

//	private AudioSource audio;
//	public AudioClip[] arrowFire;

	public GameObject projectileToFire;

	public float fireRate;
	public float initialDelay;
	public bool canFire;

	public Utilities.UtilitiesScript.Direction fireDirection;

	void Start () {
		Reset ();
	}

	void Update () {
		if (canFire) {
			StartCoroutine ("Fire");
		}
	}

	void Reset(){
		if (initialDelay > 0) {
			canFire = false;
			StartCoroutine ("IniatlDelayFire");
		} else {
			canFire = true;
		}
	}

	IEnumerator Fire(){
		canFire = false;
		float timer = 0;
		Vector3 positionToTake = Vector3.zero;
		switch (fireDirection) {
		case Utilities.UtilitiesScript.Direction.South:
			positionToTake = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
			break;
		case Utilities.UtilitiesScript.Direction.North:
			positionToTake = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
			break;
		case Utilities.UtilitiesScript.Direction.East:
			positionToTake = new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z);
			break;
		case Utilities.UtilitiesScript.Direction.West:
			positionToTake = new Vector3 (transform.position.x - 0.5f, transform.position.y, transform.position.z);
			break;
		}

		GameObject projectile = (GameObject)Instantiate (projectileToFire, positionToTake, Quaternion.identity, gameObject.transform);
		WallTrapArrowScript projectileScript = projectile.GetComponent<WallTrapArrowScript> ();
		projectileScript.direction = fireDirection;
		projectileScript.firedBy = gameObject;
		while (fireRate > timer) {
			timer += Time.deltaTime;
			yield return null;
		}
		canFire = true;
		yield return null;
	}

	IEnumerator IniatlDelayFire(){
		float timer = 0;
		while (initialDelay > timer) {
			timer += Time.deltaTime;
			yield return null;
		}
		canFire = true;
		yield return null;
	}

	void OnDisable(){
		StopAllCoroutines ();
		canFire = false;
	}

	void OnEnable(){
		Reset ();
	}
}
