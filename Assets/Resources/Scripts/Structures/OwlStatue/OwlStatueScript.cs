using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class OwlStatueScript : MonoBehaviour {

	private GameObject barrier;
	private List<OwlStatueBarrierScript> scripts = new List<OwlStatueBarrierScript> ();


	public bool inReach;

	void Start(){
		barrier = transform.parent.parent.FindChild ("Barrier").gameObject;
		foreach (Transform t in barrier.transform) {
			scripts.Add(t.gameObject.GetComponent<OwlStatueBarrierScript> ());
		}
	}

	void Update () {
		if (inReach) {
			if(Input.GetKeyDown(KeyCode.E)){
				ChangeLog ();
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		inReach = true;
	}

	void OnTriggerExit2D(Collider2D other){
		inReach = false;
	}

	void ChangeLog(){
		foreach (OwlStatueBarrierScript s in scripts) {
			s.ChangeStat ();
		}
	}
}
