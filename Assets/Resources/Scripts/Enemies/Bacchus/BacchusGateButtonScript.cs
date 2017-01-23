using UnityEngine;
using System.Collections;

public class BacchusGateButtonScript : MonoBehaviour {

	public Transform gateParent;
	private float timer;
	public float canPressAgainTimer;
	public bool canPress;
	public bool inReach;
	private SpriteRenderer sprRen;
	public Sprite upSprite;
	public Sprite downSprite;

	// Use this for initialization
	void Start () {
		sprRen = GetComponent<SpriteRenderer> ();
		if (canPress) {
			sprRen.sprite = upSprite;
		} else {
			sprRen.sprite = downSprite;
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (canPress && inReach) {
			if (Input.GetKeyDown (KeyCode.E)) {
				foreach (Transform t in gateParent) {
					canPress = t.gameObject.GetComponent<BacchusGateScript> ().GetUp ();
				}
				if (!canPress) {
					timer = 0;
					sprRen.sprite = downSprite;
				}

			}
		} else {
			if (timer >= canPressAgainTimer) {
				canPress = true;
				timer = 0;
				sprRen.sprite = upSprite;
			} else {
				timer += Time.deltaTime;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			inReach = true;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			inReach = false;
		}
	}

}
