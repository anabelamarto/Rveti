using UnityEngine;
using System.Collections;

public class EnemyScript : CombatCharacterScript {

	public bool hunt;
	public float huntRadius;
	public float tetherBreak;
	public int id = -1;
	//public bool dead;

	public virtual void CheckDead(){
		if (currentHealth <= 0) {
			dead = true;
			Dead ();
		}
	}

	public virtual void Dead(){
		if (dead) {
			StopAllCoroutines ();
			gameObject.SetActive (false);
			transform.parent.parent.GetComponent<RoomScript> ().EnemyDied (id);
		}
	}
}