﻿using UnityEngine;
using System.Collections;

[RequireComponent(typeof(SteeringBasics))]
public class WallAvoidance : MonoBehaviour {

	/* How far ahead the ray should extend */
	public float mainWhiskerLen = 1.25f;

	/* The distance away from the collision that we wish go */
	public float wallAvoidDistance = 0.5f;

	public float sideWhiskerLen = 0.701f;

	public float sideWhiskerAngle = 45f;

	public float maxAcceleration = 40f;

	private Rigidbody2D rb;
	private SteeringBasics steeringBasics;

	// Use this for initialization
	void Start () {
		rb = GetComponent<Rigidbody2D>();
		steeringBasics = GetComponent<SteeringBasics>();
	}

	public Vector2 getSteering()
	{
		return getSteering(rb.velocity);
	}

	public Vector2 getSteering(Vector2 facingDir)
	{
		Vector2 acceleration = Vector2.zero;

		/* Creates the ray direction vector */
		Vector2[] rayDirs = new Vector2[3];
		rayDirs[0] = facingDir.normalized;

		float orientation = Mathf.Atan2(rb.velocity.y, rb.velocity.x);

		rayDirs[1] = orientationToVector(orientation + sideWhiskerAngle * Mathf.Deg2Rad);
		rayDirs[2] = orientationToVector(orientation - sideWhiskerAngle * Mathf.Deg2Rad);

		RaycastHit2D hit;

		/* If no collision do nothing */
		if (!findObstacle(rayDirs, out hit))
		{
			Debug.Log ("No obstacle found");
			return acceleration;
		}
		Debug.Log ("There's something at: " + hit.point);
		Debug.Log ("Hit normal: " + hit.normal);
		Debug.Log ("That something is: " + hit.collider.name);

		/* Create a target away from the wall to seek */
		Vector2 whereToDodge = new Vector2 (hit.normal.y, hit.normal.x);
		Vector2 targetPostition = hit.point + whereToDodge * wallAvoidDistance;
		Debug.Log ("I'mma go here to dodge: " + targetPostition);

		/* If velocity and the collision normal are parallel then move the target a bit to
         the left or right of the normal */
		Vector3 cross = Vector3.Cross(rb.velocity, hit.normal);
		Debug.Log ("Cross Magnitude: " + cross.magnitude);
		if (cross.magnitude < 0.005f)
		{
			targetPostition = targetPostition + new Vector2(-hit.normal.y, hit.normal.x);
			Debug.Log ("Repositioning");
		}

		return steeringBasics.seek((Vector2)targetPostition, maxAcceleration);
	}

	/* Returns the orientation as a unit vector */
	private Vector2 orientationToVector(float orientation)
	{
		return new Vector2(Mathf.Cos(orientation), Mathf.Sin(orientation));
	}

	private bool findObstacle(Vector2[] rayDirs, out RaycastHit2D firstHit)
	{
		firstHit = new RaycastHit2D();
		bool foundObs = false;

		for (int i = 0; i < rayDirs.Length; i++)
		{
			float rayDist = (i == 0) ? mainWhiskerLen : sideWhiskerLen;


			if (Physics2D.Raycast(transform.position, rayDirs[i], rayDist))
			{
				foundObs = true;
				firstHit = Physics2D.Raycast(transform.position, rayDirs[i], rayDist);
				break;
			}

			//Debug.DrawLine(transform.position, transform.position + rayDirs[i] * rayDist);
		}

		return foundObs;
	}
}
