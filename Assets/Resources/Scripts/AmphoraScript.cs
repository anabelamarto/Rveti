using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AmphoraScript : MonoBehaviour {

	private Animator anim;
	public GameObject objectInside;
	public bool hit;
	private bool broken;
	private bool breaking;
	private AudioSource audSor;

	public List<AudioClip> audClips = new List<AudioClip>();

	// Use this for initialization
	void Start () {
		anim = GetComponent<Animator> ();
		audSor = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (hit && !broken) {
			if (!breaking) {
				breaking = true;
				GetComponent<BoxCollider2D> ().enabled = false;
				anim.SetTrigger ("Break");
				audSor.clip = audClips [Random.Range (0, audClips.Count - 1)];
				audSor.Play ();
				StartCoroutine ("Break");
			}
		}
	}

	void SetBroken(){
		broken = true;
	}

	IEnumerator Break(){
		while (!broken) {
			yield return null;
		}
		if (objectInside != null) {
			Instantiate (objectInside, transform.position, Quaternion.identity);
		}
		yield return null;
	}

	void OnCollisionEnter2D(Collision2D other){
		if (other.gameObject.tag == "PlayerSkill") {
			hit = true;
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "PlayerSkill") {
			hit = true;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "PlayerSkill") {
			hit = true;
		}
	}
}
