using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HeroScript : MonoBehaviour {

	private UnityAction wineTrapListener;
	private UnityAction soundDazedTrapListener;

	Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;

	public KeyCode mainWeapon = KeyCode.J;
	public KeyCode subWeapon = KeyCode.K;
	public KeyCode ability = KeyCode.L;
	public bool mainWeaponOffCooldown;
	public bool subWeaponOffCooldown;
	public bool abilityOffCooldown;

	public float movementSpeed;
	public float verticalMovement;
	public float horizontalMovement;
	public bool canMove;

	public bool canDash;
	public bool dash;
	public float dashSpeed;
	public float dashTime;
	public float dashCooldown;

	public bool drunk;
	public float drunkEffectTime;
	public float drunkTimer;

	public bool soundDazed;
	public float soundDazedEffectTime;
	public float soundDazedTimer;
	public float soundDazedDirectionChangeTime;
	public float soundDazedDirectionChangeTimer;
	public Vector2 soundDazedDirection;


	void Awake () {
		wineTrapListener = new UnityAction (GetDrunk);
		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void OnEnable(){
		EventManagerScript.StartListening ("wineTrap", wineTrapListener);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("wineTrap", wineTrapListener);
	}

	void Update () {

		verticalMovement = Input.GetAxisRaw ("Vertical");
		horizontalMovement = Input.GetAxisRaw ("Horizontal");


		DealWithDrunk ();
		DealWithSoundDazed ();

		if (Input.GetKeyDown (mainWeapon) && mainWeaponOffCooldown) {
			
		}

		if (Input.GetKeyDown (subWeapon) && subWeaponOffCooldown) {
			
		}

		if (Input.GetKeyDown (ability) && abilityOffCooldown) {
			
			//dash = true;
			//canDash = false;
		}

	}

	void FixedUpdate(){
		if (canMove) {
			Move ();
		}
		if (dash) {
			StartCoroutine("DashRoutine");
		}

		spriteRenderer.sortingOrder = (int)transform.position.y;
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

	void GetDrunk(){
		drunk = true;
	}

	void DealWithDrunk(){
		if (drunk) {
			if (drunkEffectTime > drunkTimer) {
				verticalMovement *= -1;
				horizontalMovement *= -1;
				drunkTimer += Time.deltaTime;
			} else {
				drunk = false;
				drunkTimer = 0;
			}
		}
	}

	void GetSoundDazed(){
		soundDazed = true;
	}

	void DealWithSoundDazed(){
		if (soundDazed) {
			canMove = false;
			if (soundDazedEffectTime > soundDazedTimer) {
				if (soundDazedDirectionChangeTime > soundDazedDirectionChangeTimer) {
					
				} else {
					soundDazedDirection = Random.insideUnitCircle;
					soundDazedDirection = new Vector2 (Mathf.Round (soundDazedDirection.x) * movementSpeed, Mathf.Round (soundDazedDirection.y) * movementSpeed);
					soundDazedDirectionChangeTimer = 0;
				}
				rigidBody.velocity = soundDazedDirection;
				soundDazedDirectionChangeTimer += Time.deltaTime;
				soundDazedTimer += Time.deltaTime;
			} else {
				soundDazed = false;
				canMove = true;
				soundDazedTimer = 0;
			}
		}
	}
}
