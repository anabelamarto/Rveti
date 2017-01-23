using UnityEngine;
using System.Collections;

public class AltarFadeLight : MonoBehaviour {

	private SpriteRenderer spriteRenderer;
	private SpriteRenderer parentSpriteRenderer;
	private CircleCollider2D circleCol;
	private float maxDistance;

	// Use this for initialization
	void Start () {
		spriteRenderer = GetComponent<SpriteRenderer> ();
		parentSpriteRenderer = transform.parent.GetComponent<SpriteRenderer> ();
		circleCol = GetComponent<CircleCollider2D> ();
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			maxDistance = (other.bounds.center - circleCol.bounds.center).sqrMagnitude / circleCol.radius;
		}
	}

	void OnTriggerStay2D(Collider2D other){
		if (other.tag == "Player") {
			float approach = (other.bounds.center - circleCol.bounds.center).sqrMagnitude / circleCol.radius;
			float percent = approach / maxDistance;
			float alpha = 1-percent;
			Color parentC = new Color (1, 1, 1, percent);
			Color c = new Color (1, 1, 1, alpha);
			parentSpriteRenderer.color = parentC;
			spriteRenderer.color = c;
		}
	}

	void OnTriggerExit2D(Collider2D other){
		if (other.tag == "Player") {
			Color parentC = new Color (255, 255, 255, 255);
			Color c = new Color (255, 255, 255, 0);
			parentSpriteRenderer.color = parentC;
			spriteRenderer.color = c;
		}
	}
}
