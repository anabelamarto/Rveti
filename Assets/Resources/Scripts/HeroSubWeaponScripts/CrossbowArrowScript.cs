using UnityEngine;
using System.Collections;

public class CrossbowArrowScript : MonoBehaviour {

	public float horizontalDirection;
	public float verticalDirection;
	public float speed;
	public float damage;

	private bool selfDestroy;
	private Rigidbody2D rigidBody;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		if (horizontalDirection > 0){
			if (verticalDirection == 0) {
				transform.eulerAngles = new Vector3 (0, 0, 90);
			}
			if (verticalDirection > 0) {
				transform.eulerAngles = new Vector3 (0, 0, 135);
			}

			if (verticalDirection < 0) {
				transform.eulerAngles = new Vector3 (0,0,45);
			}
		}

		if (horizontalDirection < 0){
			if (verticalDirection == 0) {
				transform.eulerAngles = new Vector3 (0, 0, 270);
			}

			if (verticalDirection > 0) {
				transform.eulerAngles = new Vector3 (0,0,225);
			}

			if (verticalDirection < 0) {
				transform.eulerAngles = new Vector3 (0,0,315);
			}
		}

		if (horizontalDirection == 0 && verticalDirection > 0) {
			transform.eulerAngles = new Vector3 (0,0,180);
		}
			
	}

	void FixedUpdate(){
		rigidBody.velocity = new Vector2 (horizontalDirection * speed, verticalDirection * speed);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Enemy" || other.tag == "Bacchus") {
			EnemyScript enSc = other.GetComponent<EnemyScript> ();
			if (!enSc.invulnerable) {
				enSc.currentHealth -= damage;
				enSc.hit = true;
			}
			Destroy (gameObject);
		}

		if (other.tag == "Breakable") {
			other.gameObject.GetComponent<AmphoraScript> ().hit = true;
			Destroy (gameObject);
		}

		if (other.tag == "Room") {
			Destroy (gameObject);
		}
	}
}
