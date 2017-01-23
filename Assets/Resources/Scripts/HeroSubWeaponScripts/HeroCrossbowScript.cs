using UnityEngine;
using System.Collections;

public class HeroCrossbowScript : SkillScript {

	private HeroScript executorScript;


	public float crossbowCooldown;
	private bool selfDestroy = false;

	public override void Skill (GameObject executor){
		executorScript = executor.GetComponent<HeroScript> ();
		GameObject crossbow = (GameObject)Instantiate (gameObject, executor.transform.position, Quaternion.identity, executor.transform);
		HeroCrossbowScript crossbowScript = crossbow.GetComponent<HeroCrossbowScript> ();
		crossbowScript.executorScript = executorScript;
	}

	// Use this for initialization
	void Start () {
		GameObject arrow = (GameObject) Resources.Load ("Prefabs/CrossbowArrow");
		GameObject firedArrow = (GameObject) Instantiate (arrow, executorScript.transform.position, Quaternion.identity);
		CrossbowArrowScript arrowScript = firedArrow.GetComponent<CrossbowArrowScript> ();
		Utilities.UtilitiesScript.DirectionToAxisSpeeds (executorScript.lastDirection, ref arrowScript.horizontalDirection, ref arrowScript.verticalDirection);
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
		executorScript.subWeaponOffCooldown = false;
		while (timer < crossbowCooldown) {
			timer += Time.deltaTime;
			yield return null;
		}
		executorScript.subWeaponOffCooldown = true;
		selfDestroy = true;
		yield return null;
	}
}
