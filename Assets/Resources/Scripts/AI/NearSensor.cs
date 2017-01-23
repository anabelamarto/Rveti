using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NearSensor : MonoBehaviour {

	public HashSet<Rigidbody2D> targets = new HashSet<Rigidbody2D>();
	public List<string> targetsToIgnore;

	void OnTriggerEnter2D(Collider2D other) {
		if (!targetsToIgnore.Contains(other.tag)) {
			if (other.gameObject.GetComponent<Rigidbody2D> () != null) {
				targets.Add (other.GetComponent<Rigidbody2D> ());
			}
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (!targetsToIgnore.Contains(other.tag)) {
			if (other.gameObject.GetComponent<Rigidbody2D> () != null) {
				targets.Remove (other.GetComponent<Rigidbody2D> ());
			}
		}
	}
}
