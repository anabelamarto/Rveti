using UnityEngine;
using System.Collections;

public class HPVialScript : MonoBehaviour {

	public float hp = 50;
	private AudioSource audSor;

	void Awake(){
		audSor = GetComponent<AudioSource> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.tag == "Player") {
			Heal (other.gameObject);
		}
	}

	void Heal(GameObject hero){
		HeroScript hs = hero.GetComponent<HeroScript> ();
		float currentHP = hs.currentHealth;
		float maxHp = hs.maxHealth;
		if (currentHP+hp <= maxHp ) {
			if (currentHP + hp > maxHp) {
				hs.currentHealth = hs.maxHealth;
			} else {
				hs.currentHealth = currentHP + hp;
			}
			audSor.Play ();
			Destroy (gameObject);
		}
	}
}
