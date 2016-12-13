using UnityEngine;
using System.Collections;

public class OwlStatueScript : MonoBehaviour {

	private GameObject log;

	public bool inReach;

	// Use this for initialization
	void Start () {
		log = transform.parent.parent.FindChild ("Log").gameObject;
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
		log.SetActive (!log.activeSelf);
	}
}
