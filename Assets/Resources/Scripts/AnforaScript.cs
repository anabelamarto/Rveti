using UnityEngine;
using System.Collections;

public class AnforaScript : MonoBehaviour {

	public GameObject itemInside;

	void OnCollisionEnter2D(Collision2D other){
		//ifSkillTag?
		Instantiate(itemInside, transform.position, Quaternion.identity);
	}
}
