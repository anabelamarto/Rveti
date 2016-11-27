using UnityEngine;
using System.Collections;

public class HeroScript : MonoBehaviour {

	Rigidbody2D rigidBody;
	public float movementSpeed;
	public float verticalMovement;
	public float horizontalMovement;
	public bool canMove;

	public bool canDash;
	public bool dash;
	public float dashSpeed;
	public float dashTime;
	public float dashCooldown;


	void Awake () {
		rigidBody = GetComponent<Rigidbody2D> ();
	}

	void Update () {

		verticalMovement = Input.GetAxisRaw ("Vertical");
		horizontalMovement = Input.GetAxisRaw ("Horizontal");
		if (Input.GetKeyDown (KeyCode.L) && canDash) {
			dash = true;
			canDash = false;
		}

	}

	void FixedUpdate(){
		if (canMove) {
			Move ();
		}
		if (dash) {
			StartCoroutine("DashRoutine");
		}
	}

	void Move(){
		rigidBody.velocity = new Vector2 (horizontalMovement * movementSpeed, verticalMovement * movementSpeed);
	}

	void Dash(){
		rigidBody.AddForce(new Vector2 (horizontalMovement * dashSpeed, verticalMovement * dashSpeed),ForceMode2D.Impulse);
		dash = false;
	}

	IEnumerator DashRoutine(){
		float timer = 0;
		dash = false;
		canMove = false;
		float horizontalDirection = horizontalMovement;
		float verticalDirection = verticalMovement;
		while (dashTime > timer) {
			timer += Time.deltaTime;
			rigidBody.velocity = new Vector2 (horizontalDirection * dashSpeed, verticalDirection * dashSpeed);
			yield return null;
		}
		canMove = true;
		timer = 0;
		while (dashCooldown > timer) {
			timer += Time.deltaTime;
			yield return null;
		}
		canDash = true;
		yield return null;
	}
}
