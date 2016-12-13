using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HeroScript : MonoBehaviour {

	private UnityAction wineTrapListener;
	private UnityAction soundDazedTrapListener;

	public Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;
	public GameObject subWeaponSkill;
	public GameObject dashTest;

	public KeyCode mainWeaponKey = KeyCode.J;
	public KeyCode subWeaponKey = KeyCode.K;
	public KeyCode abilityKey = KeyCode.L;
	public bool mainWeaponOffCooldown;
	public bool subWeaponOffCooldown;
	public bool abilityOffCooldown;
	public bool skillInUse;

	public float movementSpeed;
	public float verticalMovement;
	public float horizontalMovement;
	public bool canMove;

	public bool drunk;
	public float drunkEffectTime;
	public float drunkTimer;

	public bool soundDazed;
	public float soundDazedEffectTime;
	public float soundDazedTimer;
	public float soundDazedDirectionChangeTime;
	public float soundDazedDirectionChangeTimer;
	public Vector2 soundDazedDirection;
	public bool firstDaze = true;

	public bool steppingOnWeb; 


	void Awake () {
		wineTrapListener = new UnityAction (GetDrunk);
		soundDazedTrapListener = new UnityAction (GetSoundDazed);
		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
	}

	void OnEnable(){
		EventManagerScript.StartListening ("wineTrap", wineTrapListener);
		EventManagerScript.StartListening ("soundDazeTrap", soundDazedTrapListener);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("wineTrap", wineTrapListener);
		EventManagerScript.StopListening ("soundDazeTrap", soundDazedTrapListener);
	}

	void Update () {

		verticalMovement = Input.GetAxisRaw ("Vertical");
		horizontalMovement = Input.GetAxisRaw ("Horizontal");


		DealWithDrunk ();
		DealWithSoundDazed ();

		if (Input.GetKeyDown (mainWeaponKey) && mainWeaponOffCooldown) {
			
		}

		if (Input.GetKeyDown (subWeaponKey) && subWeaponOffCooldown) {
			subWeaponSkill.GetComponent<SkillScript> ().Skill (gameObject);
		}

		if (Input.GetKeyDown (abilityKey) && abilityOffCooldown) {
			dashTest.GetComponent<SkillScript> ().Skill (gameObject);
			//dash = true;
			//canDash = false;
		}

	}

	void FixedUpdate(){
		if (canMove) {
			Move ();
		}

		spriteRenderer.sortingOrder = (int)transform.position.y;
	}

	void Move(){
		rigidBody.velocity = new Vector2 (horizontalMovement * movementSpeed, verticalMovement * movementSpeed);
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
			if (firstDaze) {
				soundDazedDirection = Random.insideUnitCircle;
				soundDazedDirection = new Vector2 (Mathf.Round (soundDazedDirection.x) * movementSpeed, Mathf.Round (soundDazedDirection.y) * movementSpeed);
				firstDaze = false;
			}
			if (soundDazedEffectTime > soundDazedTimer) {
				if (soundDazedDirectionChangeTime < soundDazedDirectionChangeTimer) {
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
				firstDaze = true;
			}
		}
	}


}
