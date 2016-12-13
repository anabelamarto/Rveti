using UnityEngine;
using System.Collections;

public class WallTrapScript : MonoBehaviour {

//	private AudioSource audio;
//	public AudioClip[] arrowFire;

	public GameObject projectileToFire;

	public float fireRate;
	public bool canFire;

	public Utilities.UtilitiesScript.Direction fireDirection;

	void Start () {
	
	}

	void Update () {
		if (canFire) {
			StartCoroutine ("Fire");
		}
	}

	IEnumerator Fire(){
		canFire = false;
		float timer = 0;
		Vector3 positionToTake = Vector3.zero;
		switch (fireDirection) {
		case Utilities.UtilitiesScript.Direction.Down:
			positionToTake = new Vector3 (transform.position.x, transform.position.y - 0.5f, transform.position.z);
			break;
		case Utilities.UtilitiesScript.Direction.Up:
			positionToTake = new Vector3 (transform.position.x, transform.position.y + 0.5f, transform.position.z);
			break;
		case Utilities.UtilitiesScript.Direction.Right:
			positionToTake = new Vector3 (transform.position.x + 0.5f, transform.position.y, transform.position.z);
			break;
		case Utilities.UtilitiesScript.Direction.Left:
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
}
