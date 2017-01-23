using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class HeroSwordAttackScript : SkillScript {

	private HeroScript executorScript;
	public bool strike = false;
	public bool animationDone = false;
	public float damage;
	public float timer = 0;
	public float timeSinceLastAttack = 0.7f;
	private AudioSource audSor;
	public List<AudioClip> clips = new List<AudioClip>();



	public override void Skill(GameObject executor){
	}

	void Awake(){
		executorScript = transform.parent.gameObject.GetComponent<HeroScript>();
		audSor = GetComponent<AudioSource> ();
	}

	void Update(){
		if (strike) {
			strike = false;
			StartCoroutine ("SwordStrike");
		}
		timer += Time.deltaTime;
		if (timer > timeSinceLastAttack) {
			executorScript.mainWeaponOffCooldown = true;
		}
		if (timer > 3) {
			timer = 0;
		}
	}

	IEnumerator SwordStrike(){
		executorScript.mainWeaponOffCooldown = false;
		executorScript.anim.SetTrigger ("Sword");
		audSor.clip = clips [Random.Range (0, clips.Count - 1)];
		audSor.Play ();
		timer = 0;
		while (!animationDone) {
			yield return null;
		}
		animationDone = false;
		executorScript.anim.ResetTrigger ("Sword");
		executorScript.mainWeaponOffCooldown = true;
		yield return null;
	}

	void OnTriggerEnter2D (Collider2D other){
//		if (other.tag == "Enemy") {
//			EnemyScript enSc = other.gameObject.GetComponent<EnemyScript> ();
//			enSc.currentHealth -= damage;
//			enSc.hit = true;
//		}
	}

	void OnCollisionEnter2D(Collision2D other){
		//Should had used layers here, too late to change now
		if (other.gameObject.tag == "Enemy" || other.gameObject.tag == "Bacchus") {
			EnemyScript enSc = other.gameObject.GetComponent<EnemyScript> ();
			if (!enSc.invulnerable) {
				enSc.currentHealth -= damage;
				enSc.hit = true;
			}
		}

		if (other.gameObject.tag == "Breakable") {
			other.gameObject.GetComponent<AmphoraScript> ().hit = true;
		}
	}
}
