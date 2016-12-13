using UnityEngine;
using System.Collections;

public class HeroDashScript : SkillScript {

	private HeroScript executorScript;

	public float dashSpeed;
	public float dashTime;
	public float dashCooldown;
	private bool selfDestroy = false;

	public override void Skill (GameObject executor){
		executorScript = executor.GetComponent<HeroScript> ();
		GameObject dash = (GameObject)Instantiate (gameObject, executor.transform.position, Quaternion.identity, executor.transform);
		HeroDashScript dashScript = dash.GetComponent<HeroDashScript> ();
		dashScript.executorScript = executorScript;
	}

	void Start(){
		StartCoroutine ("Dash");
	}

	void Update(){
		if (selfDestroy) {
			StopAllCoroutines ();
			Destroy (gameObject);
		}
	}

	IEnumerator Dash(){
		float timer = 0;
		executorScript.skillInUse = true;
		executorScript.canMove = false;
		executorScript.abilityOffCooldown = false;
		float horizontalDirection = executorScript.horizontalMovement;
		float verticalDirection = executorScript.verticalMovement;
		while (dashTime > timer) {
			timer += Time.deltaTime;
			executorScript.rigidBody.velocity = new Vector2 (horizontalDirection * dashSpeed, verticalDirection * dashSpeed);
			yield return null;
		}
		executorScript.canMove = true;
		executorScript.skillInUse = false;
		timer = 0;
		while (dashCooldown > timer) {
			timer += Time.deltaTime;
			yield return null;
		}
		executorScript.abilityOffCooldown = true;
		selfDestroy = true;
		yield return null;
	}
}
