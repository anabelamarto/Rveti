using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class HPBarScript : MonoBehaviour {

	Text values;
	RectTransform fill;
	Image sub;
	Image ability;
	private HeroScript hs;

	void Awake(){
		hs = GameObject.Find ("Hero").GetComponent<HeroScript>();
	}

	// Use this for initialization
	void Start () {
		values = transform.FindChild ("Values").gameObject.GetComponent<Text> ();
		fill = transform.FindChild ("Fill").gameObject.transform as RectTransform;
		sub = transform.FindChild ("SubCool").gameObject.GetComponent<Image> ();
		ability = transform.FindChild ("AbilityCool").gameObject.GetComponent<Image> ();
	}

	// Update is called once per frame
	void Update () {
		values.text = hs.currentHealth + "/" + hs.maxHealth;
		float percent = (hs.currentHealth * 100) / hs.maxHealth;
		fill.sizeDelta = new Vector2 (percent, fill.sizeDelta.y);
		if (hs.subWeaponOffCooldown) {
			sub.color = new Color (1f, 1f, 1f);
		} else {
			sub.color = new Color (0f, 0f, 0f);
		}

		if (hs.abilityOffCooldown) {
			ability.color = new Color (1f, 1f, 1f);
		} else {
			ability.color = new Color (0f, 0f, 0f);
		}
	}
}
