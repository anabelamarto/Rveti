using UnityEngine;
using System.Collections;

public class HeroFireThrowScript : SkillScript {

	private HeroScript executorScript;


	public float fireThrowCooldown;
	private bool selfDestroy = false;

	public override void Skill (GameObject executor){
		executorScript = executor.GetComponent<HeroScript> ();
		GameObject fireThrow = (GameObject)Instantiate (gameObject, executor.transform.position, Quaternion.identity, executor.transform);
		HeroFireThrowScript fireThrowScript = fireThrow.GetComponent<HeroFireThrowScript> ();
		fireThrowScript.executorScript = executorScript;
	}

	// Use this for initialization
	void Start () {
		GameObject fire = (GameObject)Resources.Load ("Prefabs/ThrownFireSkill");
		GameObject summonedFire = (GameObject) Instantiate (fire, executorScript.transform.position, Quaternion.identity);
		executorScript.anim.SetTrigger ("Fireball");
		FireThrowScript fireScript = summonedFire.GetComponent<FireThrowScript> ();
		Vector2 directions = Utilities.UtilitiesScript.DirectionToVectorSpeeds (executorScript.lastDirection);
		fireScript.horizontalDirection = (int)directions.x;
		fireScript.verticalDirection = (int)directions.y;
		StartCoroutine ("Cooldown");
	}
	
	// Update is called once per frame
	void Update () {
		if (selfDestroy) {
			StopAllCoroutines ();
			Destroy (gameObject);
		}
	}

	IEnumerator Cooldown(){
		float timer = 0;
		executorScript.abilityOffCooldown = false;
		while (timer < fireThrowCooldown) {
			timer += Time.deltaTime;
			yield return null;
		}
		executorScript.anim.ResetTrigger ("Fireball");
		executorScript.abilityOffCooldown = true;
		selfDestroy = true;
		yield return null;
	}
}
