using UnityEngine;
using System.Collections;
using Utilities;
using System.Collections.Generic;

public class BacchusScript : EnemyScript {

	private FollowPathUnit followScript;
	private SpriteRenderer sprRen;

	public HeroScript heroScript;
	public float timeThinkingAboutABetterLife;
	public GameObject fireToLaunch;
	public bool test;
	public float timeBeforeNextFire = 2;
	public bool canILaunch = true;
	public bool crashedAgainstTheGates;
	public float damage;
	public bool launch;
	private AudioSource audSor;

	public AudioClip crash;
	public AudioClip moving;
	public List<AudioClip> throws = new List<AudioClip> ();

	// Use this for initialization
	void Start () {
		sprRen = GetComponent<SpriteRenderer> ();
		heroScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<HeroScript>();
		rigidBody = GetComponent<Rigidbody2D> ();
		followScript = GetComponent<FollowPathUnit> ();
		anim = GetComponent<Animator> ();
		audSor = GetComponent<AudioSource> ();

		currentDamageMultiplier = minDamageMultiplier;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		CheckDead ();

		if (hit) {
			hit = false;
			//StartCoroutine ("DealWithHit");
		}

		if (heroScript.currentHealth <= 0) {
			hunt = false;
		}else if (!hunt && (heroScript.gameObject.transform.position - transform.position).magnitude < huntRadius && canILaunch) {
			hunt = true;
		} else if(hunt && (heroScript.gameObject.transform.position - transform.position).magnitude > huntRadius){
			hunt = false;
			rigidBody.velocity = new Vector2 (0, 0);
		}

		if (crashedAgainstTheGates) {
			StartCoroutine ("CrashedAgainstTheGates");
			anim.SetTrigger ("Crash");
			anim.ResetTrigger ("Uncrash");
		}

		UtilitiesScript.AxisSpeedsToDirection (ref currentDirection, rigidBody.velocity.x, rigidBody.velocity.y);
		if (currentDirection != UtilitiesScript.Direction.NoDirection) {
			lastDirection = currentDirection;
		}

		if(Input.GetKeyDown(KeyCode.N)){
			test = true;
		}

		if ((test || canILaunch) && hunt) {
			test = false;
			canILaunch = false;

			StartCoroutine ("LaunchFire");
		}
	}

	void LateUpdate(){
//		if (rigidBody.velocity.x > rigidBody.velocity.y) {
//			if (rigidBody.velocity.x > 0) {
//				anim.SetFloat ("LastX", 1);
//			} else {
//				anim.SetFloat ("LastX", -1);
//			}
//			anim.SetFloat ("LastY", 0);
//		} else {
//			anim.SetFloat ("LastX", 0);
//			if (rigidBody.velocity.y > 0) {
//				anim.SetFloat ("LastY", 1);
//			} else {
//				anim.SetFloat ("LastY", -1);
//			}
//		}

		anim.SetFloat ("LastX", rigidBody.velocity.x);
		anim.SetFloat ("LastY", rigidBody.velocity.y);
	}

	public override void CheckDead(){
		if (currentHealth <= 0) {
			dead = true;
			GameObject smoke =(GameObject) Instantiate ((GameObject)Resources.Load ("Prefabs/Smoke"), transform);
			smoke.transform.localPosition = Vector2.zero;
			Dead ();
		}
	}

	public override void Dead(){
		if (dead) {
			StopAllCoroutines ();
			transform.parent.parent.GetComponent<RoomScript> ().EnemyDied (id);
			gameObject.SetActive (false);
		}
	}

	public void SetLaunch(){
		launch = true;
	}

	IEnumerator LaunchFire(){
		float timer = 0;
		anim.SetTrigger ("Attack");
		while (!launch) {
			timer += Time.deltaTime;
			yield return null;
		}
		audSor.clip = throws[Random.Range(0, throws.Count-1)];
		audSor.loop = false;
		audSor.Play ();
		launch = false;
		GameObject fire = (GameObject)Instantiate (fireToLaunch, new Vector2 (0, 0), Quaternion.identity, transform);
		FireThrowScript fireScript = fire.GetComponent<FireThrowScript> ();
		fire.transform.localPosition = new Vector2 (UtilitiesScript.DirectionToVectorSpeeds (heroScript.lastDirection).x, UtilitiesScript.DirectionToVectorSpeeds (heroScript.lastDirection).y);
		fire.transform.parent = null;
		fireScript.crashPosition = new Vector2 (heroScript.transform.position.x + UtilitiesScript.DirectionToVectorSpeeds (heroScript.lastDirection).x, heroScript.transform.position.y + UtilitiesScript.DirectionToVectorSpeeds (heroScript.lastDirection).y);
		float range = Vector2.Distance (fireScript.crashPosition, transform.position);
		float airborneTime = range / 10;
		Vector2 aux = (Vector2)(heroScript.transform.position - transform.position);
		aux.Normalize ();
		fireScript.horizontalDirection = (int) aux.x;
		fireScript.verticalDirection = (int) aux.y;
		fireScript.range = range;
		fireScript.airborneTime = airborneTime;
		StartCoroutine ("WaitingForNextFire");
		yield return null;
	}

	IEnumerator WaitingForNextFire(){
		float timer = 0;
		while (timer < timeBeforeNextFire) {
			timer += Time.deltaTime;
			yield return null;
		}

		canILaunch = true;
		yield return null;
		audSor.clip = moving;
		audSor.loop = true;
		audSor.Play ();
	}

	IEnumerator CrashedAgainstTheGates(){
		audSor.clip = crash;
		audSor.loop = false;
		audSor.Play ();
		float timer = 0;
		crashedAgainstTheGates = false;
		invulnerable = false;
		while (timer < timeThinkingAboutABetterLife) {
			hunt = false;
			canILaunch = false;
			timer += Time.deltaTime;
			followScript.enabled = false;
			rigidBody.velocity = new Vector2 (0, 0);
			if (timer > 2.2) {
				sprRen.color = new Color (1, 0, 0);
				sprRen.enabled = !sprRen.enabled;
			}
			yield return null;
		}
		sprRen.enabled = true;
		sprRen.color = new Color (1, 1, 1);
		followScript.enabled = true;
		invulnerable = true;
		anim.SetTrigger ("Uncrash");
		anim.ResetTrigger ("Crash");
		canILaunch = true;
		audSor.clip = moving;
		audSor.loop = true;
		audSor.Play ();
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "BacchusGate") {
			crashedAgainstTheGates = true;
		}

		if (other.gameObject.tag == "Player" && invulnerable) {
			HeroScript hs = other.gameObject.GetComponent<HeroScript> ();
			if (!hs.invulnerable) {
				hs.hit = true;
				hs.currentHealth -= damage;
			}
		}
	}
}
