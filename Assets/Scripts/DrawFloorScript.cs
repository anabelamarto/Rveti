using UnityEngine;
using UnityEngine.Events;
using System.Collections;

public class DrawFloorScript : MonoBehaviour {

	public GameObject[] tiles;
	public Vector2 size;
	public Vector2 startingPosition;

	private UnityAction floorDrawListener;

//	void Awake(){
//		floorDrawListener = new UnityAction (DrawFloor);
//	}
//
//	void OnEnable(){
//		EventManagerScript.StartListening ("floorDraw", floorDrawListener);
//	}
//
//	void OnDisable(){
//		EventManagerScript.StopListening ("floorDraw", floorDrawListener);
//	}

	void Start(){
		DrawFloor ();
	}

	public void DrawFloor(){
		//draw the floor tiles
		for (int i = 0; i < size.x; i++) {
			for (int j = 0; j < size.y; j++) {
				Instantiate (tiles [0], new Vector3 (startingPosition.x+i, startingPosition.y-j, 0), Quaternion.identity, transform);
			}
		}

		for (int i = 0; i < size.x; i++) {
			Instantiate (tiles [1], new Vector3 (startingPosition.x + i, startingPosition.y, 0), Quaternion.Euler(0,0,90),transform);
			Instantiate (tiles [1], new Vector3 (startingPosition.x + i, startingPosition.y - size.y - 0.25f, 0), Quaternion.Euler(0,0,90),transform);
		}

		for (int i = 0; i < size.y; i++) {
			Instantiate (tiles [1], new Vector3 (startingPosition.x-0.25f, startingPosition.y-i, 0), Quaternion.Euler(0,0,0),transform);
			Instantiate (tiles [1], new Vector3 (startingPosition.x+size.x, startingPosition.y-i, 0), Quaternion.Euler(0,0,0),transform);
		}
	}
}
