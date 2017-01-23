using UnityEngine;
using System.Collections;

public class CombatCharacterScript : CharacterScript {

	public float maxHealth;
	public float currentHealth;
	public float movementSpeed;
	public float minDamageMultiplier;
	public float currentDamageMultiplier;
	public float invulnerableTime;
	public float pushedBackTime;
	[Range(0,1)] public float pushBackResistance;
	public bool hit;
	//public GameObject[] attacks;
	public bool canMove;
	public bool canAttack;
	public bool canInteract;
	public Vector2 pushSuffered;
	public float strenghtOfPushSuffered;
	public float damageSuffered;
	public Rigidbody2D rigidBody;
	public Animator anim;
	public bool beingPushed;
	public bool dead;
	public bool isMoving;

	public void CantDoAnything ()
	{
		canMove = false;
		canInteract = false;
		canAttack = false;
	}

	public void CanDoEverything ()
	{
		canMove = true;
		canInteract = true;
		canAttack = true;
	}
}
