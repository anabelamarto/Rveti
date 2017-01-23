using UnityEngine;
using System.Collections;

public class WallTrapArrowScript : ProjectileScript {

	private SpriteRenderer sprRend;
	private Rigidbody2D rigidBody;
	private BoxCollider2D boxCollider;
	public Utilities.UtilitiesScript.Direction direction;
	public float timeToLive;
	public float timer;

	// Use this for initialization
	void Start () {
		rigidBody = GetComponent<Rigidbody2D> ();
		sprRend = GetComponent<SpriteRenderer> ();
		boxCollider = gameObject.AddComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;
		timer = 0;
		switch (direction) {
		case Utilities.UtilitiesScript.Direction.North:
			sprRend.flipY = true;
			break;
		case Utilities.UtilitiesScript.Direction.East:
			transform.eulerAngles = new Vector3 (0, 0, 90);
			transform.localPosition = new Vector3 (0, -0.5f, 0);
			break;
		case Utilities.UtilitiesScript.Direction.West:
			transform.eulerAngles = new Vector3 (0, 0, -90);
			transform.localPosition = new Vector3 (0, -0.24f, 0);
			break;
		default:
			break;
		}
	}
	
	void FixedUpdate ()
	{
		switch (direction) {
		case Utilities.UtilitiesScript.Direction.South:
			rigidBody.velocity = new Vector2 (0, -speed);
			break;
		case Utilities.UtilitiesScript.Direction.North:
			rigidBody.velocity = new Vector2 (0, speed);
			break;
		case Utilities.UtilitiesScript.Direction.East:
			rigidBody.velocity = new Vector2 (speed, 0);
			break;
		case Utilities.UtilitiesScript.Direction.West:
			rigidBody.velocity = new Vector2 (-speed, 0);
			break;
		default:
			break;
		}

		if (timer > timeToLive) {
			Destroy (gameObject);
		}

		timer += Time.deltaTime;
	}

	void OnDisable(){
		Destroy (gameObject);
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			HeroScript hs = other.GetComponent<HeroScript> ();
			if (!hs.invulnerable) {
				hs.currentHealth -= damage;
				hs.hit = true;
			}
		}
	}
}
