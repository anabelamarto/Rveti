using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public bool opened;
	public GameObject connection;
	public Vector2 positionForPlayer;
	private BoxCollider2D boxCollider;
	public SpriteRenderer childSpriteRen;
	public Animator childAnim;
	public RuntimeAnimatorController control;
	public Sprite open;
	public Sprite close;

	private DoorScript connectionScript;

	void Start () {
		Transform child = transform.GetChild (0);
		connectionScript = connection.GetComponent<DoorScript> ();
		boxCollider = GetComponent<BoxCollider2D> ();
		childSpriteRen = child.GetComponent<SpriteRenderer>();
		if (opened) {
			childSpriteRen.sprite = open;
		} else {
			childSpriteRen.sprite = close;
		}
		childAnim = child.gameObject.AddComponent<Animator> ();
		childAnim.runtimeAnimatorController = control;
	}

	void Update(){
		if (!opened) {
			//boxCollider.enabled = false;
			if (boxCollider != null) {
				boxCollider.isTrigger = false;
			}
		} else {
			//boxCollider.enabled = true;
			if (boxCollider != null) {
				boxCollider.isTrigger = true;
			}
		}
	}

	void OnTriggerEnter2D(Collider2D other){
		if (opened) {
			if (other.tag == "Player") {
				other.transform.position = connectionScript.transform.TransformPoint(connectionScript.positionForPlayer);
				connectionScript.transform.parent.parent.GetComponent<RoomScript> ().active = true;
				transform.parent.parent.GetComponent<RoomScript> ().active = false;
				EventManagerScript.TriggerEvent ("activateRoom");
				EventManagerScript.TriggerEvent ("deactivateRoom");
			}
		}
	}
}
