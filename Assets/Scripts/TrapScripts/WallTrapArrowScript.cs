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

		boxCollider = gameObject.AddComponent<BoxCollider2D> ();
		boxCollider.isTrigger = true;
		timer = 0;
	}
	
	void FixedUpdate ()
	{
		switch (direction) {
		case Utilities.UtilitiesScript.Direction.Down:
			rigidBody.velocity = new Vector2 (0, -speed);
			break;
		case Utilities.UtilitiesScript.Direction.Up:
			rigidBody.velocity = new Vector2 (0, speed);
			break;
		case Utilities.UtilitiesScript.Direction.Right:
			rigidBody.velocity = new Vector2 (speed, 0);
			break;
		case Utilities.UtilitiesScript.Direction.Left:
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
}
