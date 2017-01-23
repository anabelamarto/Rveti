using UnityEngine;
using System.Collections;

public class FireThrowScript : MonoBehaviour {

	public float horizontalDirection;
	public float verticalDirection;
	public float range;
	public float airborneTime;
	public float maxTimeAlive;
	public float colliderRadius;
	public Vector2 colliderOffset;
	public float damage;
	public Animator anim;
	public bool destroyable;
	private CircleCollider2D collider;
	private SpriteRenderer sprRen;
	private AudioSource audSor;

	public Vector2 crashPosition;
	private bool selfDestroy;
	public bool receiveCrashPosition;

	// Use this for initialization
	void Start () {
		destroyable = true;
		collider = gameObject.AddComponent<CircleCollider2D> ();
		audSor = GetComponent<AudioSource> ();
		collider.isTrigger = true;
		if (!receiveCrashPosition) {
			crashPosition = new Vector2 (transform.position.x + horizontalDirection * range, transform.position.y + verticalDirection * range);
		}
		if (tag == "BacchusWine") {
			anim = GetComponent<Animator> ();
			sprRen = GetComponent<SpriteRenderer> ();
		}
		StartCoroutine("Throw");
	}

	// Update is called once per frame
	void Update () {
		if (selfDestroy) {
			StopAllCoroutines ();
			Destroy (gameObject);
		}
	}

	IEnumerator Throw(){
		float timer = 0;
		Vector2 positionIncrement = (crashPosition - (Vector2)transform.position)/airborneTime;
		while(timer < airborneTime){
			transform.position = (Vector2) transform.position + (positionIncrement * Time.deltaTime);
			timer += Time.deltaTime;
			yield return null;
		}
		transform.localScale = new Vector3 (1, 1, 1);
		destroyable = false;
		collider.radius = colliderRadius;
		collider.offset = colliderOffset;
		if (tag == "BacchusWine") {
			anim.SetTrigger ("Smoke");
			audSor.Play ();	
			sprRen.color = new Color (1f, 0f, 0.3f);
		}
		timer = 0;
		while (timer < maxTimeAlive) {
			timer += Time.deltaTime;
			yield return null;
		}
		selfDestroy = true;
		yield return null;
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player" && tag != "PlayerSkill" && !destroyable) {
			HeroScript hs = other.GetComponent<HeroScript> ();
			if (!hs.invulnerable) {
				hs.currentHealth -= damage;
				hs.hit = true;
			}
			if (tag == "BacchusWine") {
				hs.drunk = true;
				//EventManagerScript.TriggerEvent ("wineTrap");
			}
		}

		if ((other.tag == "Enemy" || other.tag == "Bacchus") && tag == "PlayerSkill" && !destroyable) {
			EnemyScript enSc = other.GetComponent<EnemyScript> ();
			if (!enSc.invulnerable) {
				enSc.currentHealth -= damage;
				enSc.hit = true;
			}
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if(other.tag == "Breakable" && tag=="PlayerSkill"){
			other.GetComponent<AmphoraScript> ().hit = true;
		}
	}
}
