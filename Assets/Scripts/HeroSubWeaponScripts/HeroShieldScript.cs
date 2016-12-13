using UnityEngine;
using System.Collections;

public class HeroShieldScript : SkillScript {

	private HeroScript executorScript;
	public float shieldTime;
	public float shieldCooldown;
	public float shieldRadius;
	private bool selfDestroy = false;

	public override void Skill (GameObject executor)
	{
		executorScript = executor.GetComponent<HeroScript> ();
		GameObject shield = (GameObject)Instantiate (gameObject, executor.transform.position, Quaternion.identity, executor.transform);
		HeroShieldScript shieldScript = shield.GetComponent<HeroShieldScript> ();
		shieldScript.executorScript = executorScript;
	}

	void Start(){
		StartCoroutine ("Shield");
	}

	void Update(){
		if (selfDestroy) {
			StopAllCoroutines ();
			Destroy (gameObject);
		}
	}

	IEnumerator Shield(){
		float timer = 0;
		executorScript.skillInUse = true;
		executorScript.canMove = false;
		executorScript.subWeaponOffCooldown = false;
		CircleCollider2D circleCollider2D = gameObject.AddComponent<CircleCollider2D> ();
		circleCollider2D.radius = shieldRadius;
		while (shieldTime > timer) {
			timer += Time.deltaTime;
			yield return null;
		}
		Destroy (circleCollider2D);
		executorScript.canMove = true;
		executorScript.skillInUse = false;
		timer = 0;
		while (shieldCooldown > timer) {
			timer += Time.deltaTime;
			yield return null;
		}
		executorScript.subWeaponOffCooldown = true;
		selfDestroy = true;
		yield return null;
	}
}
