using UnityEngine;
using System.Collections;

public class BacchusGateScript : MonoBehaviour {

	private Animator anim;
	private BoxCollider2D colli;
	private float timer;
	public float timeUp;
	public bool up;


	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		colli = GetComponent<BoxCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (up) {
			if (timer >= timeUp) {
				up = false;
				anim.SetTrigger ("Down");
				anim.ResetTrigger ("Up");
				colli.enabled = false;
			}
			timer += Time.deltaTime;
		} else {
			timer = 0;
		}
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "Bacchus") {
			foreach (Transform t in transform.parent) {
				anim.SetTrigger ("Down");
				anim.ResetTrigger ("Up");
				colli.enabled = false;
			}
		}
	}

	//returns false indicating it went up and can't go again for now
	public bool GetUp(){
		if(!up){
			anim.SetTrigger ("Up");
			anim.ResetTrigger ("Down");
			up = true;
			colli.enabled = true;
			return false;
		}
		return true;
	}
}
