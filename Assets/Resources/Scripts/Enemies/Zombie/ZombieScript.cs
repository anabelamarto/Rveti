using UnityEngine;
using System.Collections;
using Utilities;

public class ZombieScript : EnemyScript {

	private PursueUnit pursueScript;
	private SpriteRenderer sprRen;

	public HeroScript heroScript;

	public bool horizontalAlign;
	public bool verticalAlign;
	public float timeBeforeChase;
	public float timeThinkingAboutABetterLife;
	public float damage;
	private float lastX;
	private float lastY;

	// Use this for initialization
	void Start () {
		sprRen = GetComponent<SpriteRenderer> ();
		anim = GetComponent<Animator> ();
		heroScript = GameObject.FindGameObjectWithTag ("Player").GetComponent<HeroScript>();
		rigidBody = GetComponent<Rigidbody2D> ();
		pursueScript = GetComponent<PursueUnit> ();

		currentDamageMultiplier = minDamageMultiplier;
		currentHealth = maxHealth;
		pursueScript.enabled = false;
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
			pursueScript.enabled = false;
		}else if (!hunt && (heroScript.gameObject.transform.position - transform.position).magnitude < huntRadius && !heroScript.invulnerable && !invulnerable) {
			hunt = true;
			pursueScript.enabled = true;
		} else if(hunt && (heroScript.gameObject.transform.position - transform.position).magnitude > huntRadius + tetherBreak && !heroScript.invulnerable){
			hunt = false;
			pursueScript.enabled = false;
			rigidBody.velocity = new Vector2 (0, 0);
		}

		UtilitiesScript.AxisSpeedsToDirection (ref currentDirection, rigidBody.velocity.x, rigidBody.velocity.y);
		if (currentDirection != UtilitiesScript.Direction.NoDirection) {
			lastDirection = currentDirection;
		}
	}

	void FixedUpdate(){

		if (rigidBody.velocity.magnitude > 0) {
			isMoving = true;
		} else {
			isMoving = false;
		}

		lastX = rigidBody.velocity.x;
		lastY = rigidBody.velocity.y;


		anim.SetFloat ("LastX", lastX);
		anim.SetFloat ("LastY", lastY);
		anim.SetBool ("IsMoving", isMoving);
	}

	IEnumerator DealWithHit(){
		float timer = 0;
		invulnerable = true;
		hunt = false;
		pursueScript.enabled = false;
		while (timeThinkingAboutABetterLife > timer) {
			timer += Time.deltaTime;
			sprRen.enabled = !sprRen.enabled;
			rigidBody.velocity = new Vector2 (0, 0);
			yield return null;
		}
		sprRen.enabled = true;
		invulnerable = false;
		yield return null;
	}

	void OnCollisionStay2D(Collision2D other){
		if (other.gameObject.tag == "Player" && !invulnerable) {
			HeroScript hs = other.gameObject.GetComponent<HeroScript> ();
			if (!hs.invulnerable) {
				hs.hit = true;
				hs.currentHealth -= currentDamageMultiplier * damage;
			}
		}
	}
}
