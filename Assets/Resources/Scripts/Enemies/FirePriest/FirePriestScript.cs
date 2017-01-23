using UnityEngine;
using System.Collections;
using Utilities;

public class FirePriestScript : EnemyScript {

	private Wander2Unit wanderScript;
	private SpriteRenderer sprRen;

	public HeroScript heroScript;
	public float timeThinkingAboutABetterLife;
	public GameObject fireToLaunch;
	public bool test;
	public float timeBeforeNextFire = 2;
	public bool watchTheWorldBurnAgain = true; //can I fire again?

	// Use this for initialization
	void Start () {
		sprRen = GetComponent<SpriteRenderer> ();
		heroScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<HeroScript>();
		rigidBody = GetComponent<Rigidbody2D> ();
		wanderScript = GetComponent<Wander2Unit> ();
		anim = GetComponent<Animator> ();

		currentDamageMultiplier = minDamageMultiplier;
		currentHealth = maxHealth;
	}
	
	// Update is called once per frame
	void Update () {
		CheckDead ();

		if (hit) {
			hit = false;
			StartCoroutine ("DealWithHit");
		}

		if (heroScript.currentHealth <= 0) {
			hunt = false;
		}else if (!hunt && (heroScript.gameObject.transform.position - transform.position).magnitude < huntRadius && !heroScript.invulnerable && !invulnerable && watchTheWorldBurnAgain) {
			hunt = true;
			wanderScript.enabled = true;
		} else if(hunt && (heroScript.gameObject.transform.position - transform.position).magnitude > huntRadius + tetherBreak && !heroScript.invulnerable){
			hunt = false;
			rigidBody.velocity = new Vector2 (0, 0);
		}

		UtilitiesScript.AxisSpeedsToDirection (ref currentDirection, rigidBody.velocity.x, rigidBody.velocity.y);
		if (currentDirection != UtilitiesScript.Direction.NoDirection) {
			lastDirection = currentDirection;
		}

		if(Input.GetKeyDown(KeyCode.N)){
			test = true;
		}

		if ((test || watchTheWorldBurnAgain) && hunt) {
			test = false;
			watchTheWorldBurnAgain = false;
			LaunchFire ();
		}
	}

	void LateUpdate(){
		if (rigidBody.velocity.x > rigidBody.velocity.y) {
			anim.SetFloat ("LastX", rigidBody.velocity.x);
			anim.SetFloat ("LastY", 0);
		} else {
			anim.SetFloat ("LastX", 0);
			anim.SetFloat ("LastY", rigidBody.velocity.y);
		}

	}

	void LaunchFire(){
		rigidBody.velocity = new Vector2 (0, 0);
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
	}

	IEnumerator WaitingForNextFire(){
		float timer = 0;
		while (timer < timeBeforeNextFire) {
			timer += Time.deltaTime;
			yield return null;
		}

		watchTheWorldBurnAgain = true;
		yield return null;
	}

	IEnumerator DealWithHit(){
		float timer = 0;
		invulnerable = true;
		hunt = false;
		wanderScript.enabled = false;
		while (timeThinkingAboutABetterLife > timer) {
			timer += Time.deltaTime;
			sprRen.enabled = !sprRen.enabled;
			rigidBody.velocity = new Vector2 (0, 0);
			yield return null;
		}
		sprRen.enabled = true;
		invulnerable = false;
		wanderScript.enabled = true;
		yield return null;
	}
}
