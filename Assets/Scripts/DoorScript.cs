using UnityEngine;
using System.Collections;

public class DoorScript : MonoBehaviour {

	public bool opened;
	public GameObject connection;
	public Vector2 positionForPlayer;

	private DoorScript connectionScript;

	void Start () {
		connectionScript = connection.GetComponent<DoorScript> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (opened) {
			if (other.tag == "Player") {
				other.transform.position = connectionScript.positionForPlayer;
			}
		}
	}
}
