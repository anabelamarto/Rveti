using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class HeroScript : CombatCharacterScript {
	
	private UnityAction wineTrapListener;
	private UnityAction soundDazedTrapListener;
	private UnityAction heroActiveListener;
	private UnityAction heroInactiveListener;

	//public Rigidbody2D rigidBody;
	SpriteRenderer spriteRenderer;
	//public Utilities.UtilitiesScript.Direction currentDirection;
	//public Utilities.UtilitiesScript.Direction lastDirection;

	public GameObject mainWeaponSkill;
	public GameObject subWeaponSkill;
	public GameObject abilitySkill;

	private GameObject fireSkill;
	private GameObject shieldSkill;
	private GameObject crossbowSkill;
	private GameObject dashSkill;

	public bool heroActive;
	public KeyCode mainWeaponKey = KeyCode.J;
	public KeyCode subWeaponKey = KeyCode.K;
	public KeyCode abilityKey = KeyCode.L;
	public bool mainWeaponOffCooldown;
	public bool subWeaponOffCooldown;
	public bool abilityOffCooldown;
	public bool mainWeaponColliderActive;
	public HeroSwordAttackScript sword;
	public bool skillInUse;

	public float initialMovementSpeed;
	//public float movementSpeed;
	public float verticalMovement;
	public float horizontalMovement;
	public bool isDashing;
	public bool disableMove;
	//public bool canMove;

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
	public bool isDancing;

	private float hitTimer =0;
	public bool hitBeingProcessed;

	private UnityAction steppedOnWebListener;
	private UnityAction exitedWebListenter;
	public bool steppingOnWeb; 
	public int websCurrentlyBeingSteppedOn;
	public float webSpeedModifier;

	public bool isKneeling;

	private bool shieldEq;
	private bool crossbowEq;
	private bool fireEq;
	private bool dashEq;

	public AudioClip shieldClip;
	public AudioClip crossbowClip;
	public AudioClip fireClip;
	public AudioClip dashClip;

	private AudioSource audSor;

	void Awake () {
		wineTrapListener = new UnityAction (GetDrunk);
		soundDazedTrapListener = new UnityAction (GetSoundDazed);
		heroActiveListener = new UnityAction (HeroActive);
		heroInactiveListener = new UnityAction (HeroInactive);
		steppedOnWebListener = new UnityAction (SteppedOnWeb);
		exitedWebListenter = new UnityAction (ExitedWeb);


		rigidBody = GetComponent<Rigidbody2D> ();
		spriteRenderer = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		audSor = GetComponent<AudioSource> ();


		lastDirection = currentDirection;
		movementSpeed = initialMovementSpeed;
		currentHealth = maxHealth;
		currentDamageMultiplier = minDamageMultiplier;

		sword = transform.FindChild("SwordTest").gameObject.GetComponent<HeroSwordAttackScript>();
		fireSkill = (GameObject)Resources.Load ("Prefabs/ThrownFireTest");
		dashSkill = (GameObject)Resources.Load ("Prefabs/DashTest");
		shieldSkill = (GameObject)Resources.Load ("Prefabs/ShieldTest");
		crossbowSkill = (GameObject)Resources.Load ("Prefabs/CrossbowTest");
		subWeaponSkill = shieldSkill;
		shieldEq = true;
		abilitySkill = dashSkill;
		dashEq = true;
	}

	void OnEnable(){
		EventManagerScript.StartListening ("wineTrap", wineTrapListener);
		EventManagerScript.StartListening ("soundDazeTrap", soundDazedTrapListener);
		EventManagerScript.StartListening ("heroActive", heroActiveListener);
		EventManagerScript.StartListening ("heroInactive", heroInactiveListener);
		EventManagerScript.StartListening ("steppedOnWeb", steppedOnWebListener);
		EventManagerScript.StartListening ("exitedWeb", exitedWebListenter);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("wineTrap", wineTrapListener);
		EventManagerScript.StopListening ("soundDazeTrap", soundDazedTrapListener);
		EventManagerScript.StopListening ("heroActive", heroActiveListener);
		EventManagerScript.StopListening ("heroInactive", heroInactiveListener);
		EventManagerScript.StopListening ("steppedOnWeb", steppedOnWebListener);
		EventManagerScript.StopListening ("exitedWeb", exitedWebListenter);
	}

	void Update () {

		if (currentHealth <= 0) {
			anim.SetTrigger ("Die");
			StartCoroutine ("Die");
		}

		if (isKneeling) {
			heroActive = false;
			ZeroMovement ();

			if (Input.GetKeyDown (KeyCode.I)) {
				if (shieldEq) {
					audSor.clip = crossbowClip;
					subWeaponSkill = crossbowSkill;
					shieldEq = false;
					crossbowEq = true;
				} else {
					audSor.clip = shieldClip;
					subWeaponSkill = shieldSkill;
					shieldEq = true;
					crossbowEq = false;
				}
			}

			if(Input.GetKeyDown(KeyCode.O)){
				if(fireEq){
					audSor.clip = dashClip;
					abilitySkill = dashSkill;
					fireEq = false;
					dashEq = true;
				}else{
					audSor.clip = fireClip;
					abilitySkill = fireSkill;
					fireEq = true;
					dashEq = false;
				}
			}

			if(Input.GetKeyDown(KeyCode.R)){
				EventManagerScript.TriggerEvent("deactivateAltar");
			}

		} else {
			heroActive = true;
		}

		if (heroActive) {

			DealWithHit ();

			if (canMove) {
				verticalMovement = Input.GetAxisRaw ("Vertical");
				horizontalMovement = Input.GetAxisRaw ("Horizontal");
			}


			DealWithDrunk ();
			DealWithSoundDazed ();
			DealWithWebs ();

			if (Input.GetKeyDown (mainWeaponKey) && mainWeaponOffCooldown) {
				//mainWeaponSkill.GetComponent<SkillScript> ().Skill (gameObject);
				sword.strike = true;
				//anim.SetTrigger ("MainWeapon");
			}

			if (Input.GetKeyDown (subWeaponKey) && subWeaponOffCooldown) {
				subWeaponSkill.GetComponent<SkillScript> ().Skill (gameObject);
			}

			if (Input.GetKeyDown (abilityKey) && abilityOffCooldown) {
				abilitySkill.GetComponent<SkillScript> ().Skill (gameObject);
			}
				

		}
	}

	void FixedUpdate(){
		if (!isDashing && !disableMove) {
			Move ();
		}
		DealWithAnimation ();

	}

	void Move(){
		rigidBody.velocity = new Vector2 (horizontalMovement * movementSpeed, verticalMovement * movementSpeed);
		Utilities.UtilitiesScript.AxisSpeedsToDirection (ref currentDirection, rigidBody.velocity.x, rigidBody.velocity.y);
		if (currentDirection != Utilities.UtilitiesScript.Direction.NoDirection) {
			lastDirection = currentDirection;
		}
		if (rigidBody.velocity.magnitude > 0) {
			isMoving = true;
		} else {
			isMoving = false;
		}

		if (isMoving) {
			if (!audSor.isPlaying) {
				audSor.Play ();
			}
		} else {
			audSor.Stop ();
		}
	}

	void DealWithAnimation(){
		spriteRenderer.sortingOrder = (int)transform.position.y;
		anim.SetFloat ("HorizontalSpeed", rigidBody.velocity.x);
		anim.SetFloat ("VerticalSpeed", rigidBody.velocity.y);
		anim.SetFloat ("LastX", Utilities.UtilitiesScript.DirectionToVectorSpeeds (lastDirection).x);
		anim.SetFloat ("LastY", Utilities.UtilitiesScript.DirectionToVectorSpeeds (lastDirection).y);
		anim.SetBool ("IsMoving", isMoving);
	}

	void HeroActive(){
		heroActive = true;
	}

	void HeroInactive(){
		heroActive = false;
	}

	void GetDrunk(){
		drunk = true;
	}

	void DealWithDrunk(){
		if (drunk) {
			spriteRenderer.color = new Color (1f, 0.2f,0.98f);
			if (drunkEffectTime > drunkTimer) {
				verticalMovement *= -1;
				horizontalMovement *= -1;
				drunkTimer += Time.deltaTime;
			} else {
				spriteRenderer.color = new Color (1f,1f,1f);
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
			disableMove = true;
			isDancing = true;
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
				disableMove = false;
				isDancing = false;
				soundDazedTimer = 0;
				firstDaze = true;
			}
		}
	}

	void SteppedOnWeb(){
		websCurrentlyBeingSteppedOn++;
	}

	void ExitedWeb(){
		websCurrentlyBeingSteppedOn--;
		if (websCurrentlyBeingSteppedOn < 0) {
			websCurrentlyBeingSteppedOn = 0;
		}
	}

	void DealWithWebs(){
		if (websCurrentlyBeingSteppedOn > 0 && steppingOnWeb == false) {
			steppingOnWeb = true;
			movementSpeed -= webSpeedModifier;
		} else if(websCurrentlyBeingSteppedOn == 0 && steppingOnWeb == true){
			steppingOnWeb = false;
			movementSpeed += webSpeedModifier;
		}
	}

	void DealWithHit(){
		if (hit && !hitBeingProcessed) {
			hit = false;
			hitBeingProcessed = true;
		}
		if (hitTimer < invulnerableTime && hitBeingProcessed) {
			hitTimer += Time.deltaTime;
			spriteRenderer.enabled = !spriteRenderer.enabled;
			invulnerable = true;
		} else {
			hitBeingProcessed = false;
			invulnerable = false;
			spriteRenderer.enabled = true;
			hitTimer = 0;
		}
	}

	IEnumerator Die(){
		float timer = 0;
		while(timer < 2f){
			ZeroMovement ();
			heroActive = false;
			timer += Time.deltaTime;
			yield return null;
		}
		Application.LoadLevel(Application.loadedLevel);
		yield return null;
	}


	//PUBLIC METHODS
	public void ZeroMovement(){
		horizontalMovement = 0;
		verticalMovement = 0;
	}
}
