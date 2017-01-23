using UnityEngine;
using System.Collections;

public class OwlStatueBarrierScript : MonoBehaviour {

	BoxCollider2D colli;
	Animator anim;
	private bool up = true;

	// Use this for initialization
	void Awake () {
		colli = GetComponent<BoxCollider2D> ();
		anim = GetComponent<Animator> ();
	}

	public void ChangeStat(){
		if (up) {
			
			up = false;
			anim.ResetTrigger ("Up");
			anim.SetTrigger ("Down");
			colli.enabled = false;
		} else {
			up = true;
			anim.ResetTrigger ("Down");
			anim.SetTrigger ("Up");
			colli.enabled = true;
		}
	}
}
