using UnityEngine;
using UnityEngine.Events;
using System.Collections;
using System.Collections.Generic;

public class RoomScript : MonoBehaviour {

	public float xMin;
	public float yMin;
	public float xMax;
	public float yMax;
	public float width;
	public float height;
	public bool active;
	public int enemiesAlive;
	public bool visited = false;
	public bool trapsRemainActive;
	public bool lockdoors;
	public List<GameObject> enemies;
	public List<Vector2> enemyPositions;
	private List<int> activeEnemiesID = new List<int>();

	private UnityAction activateRoomListener;
	private UnityAction deactivateRoomListener;

	// Use this for initialization
	void Awake () {
		activateRoomListener = new UnityAction (ActivateChildren);
		deactivateRoomListener = new UnityAction (DeactivateChildren);
		CalculateRoomSize ();
		if (active) {
			GameObject.Find ("Main Camera").GetComponent<CameraScript> ().setLimits(xMin, xMax, yMin, yMax, width);
		}
	}
	
	// Update is called once per frame
	void Update () {
		if (enemiesAlive <= 0 && active) {
			Transform doors = transform.FindChild ("Doors");
			foreach (Transform child in doors) {
				DoorScript ds = child.gameObject.GetComponent<DoorScript> ();
				ds.opened = true;
				ds.childAnim.ResetTrigger ("Close");
				ds.childAnim.SetTrigger ("Open");
			}
		}
	}

	void CalculateRoomSize(){
		xMin = transform.position.x;
		xMax = transform.position.x + width;
		yMax = transform.position.y;
		yMin = transform.position.y - height;
	}

	public void EnemyDied(int identification){
		activeEnemiesID.Remove (identification);
		enemiesAlive = activeEnemiesID.Count;
		if (enemiesAlive == 0) {
			Transform doors = transform.FindChild ("Doors");
			foreach (Transform child in doors) {
				DoorScript ds = child.gameObject.GetComponent<DoorScript> ();
				ds.opened = true;
			}
		}
	}

	void ResetRoom(){
		for (int i = 0; i < activeEnemiesID.Count; i++) {
			int n = activeEnemiesID [i];
			Transform listOfEnemies = transform.FindChild ("Enemies");
			foreach (Transform enemy in listOfEnemies) {
				if (enemy.GetComponent<EnemyScript> ().id == n) {
					enemy.localPosition = enemyPositions [n];
				}
			}
		}
	}

	void OnEnable(){
		EventManagerScript.StartListening ("activateRoom", activateRoomListener);
		EventManagerScript.StartListening ("deactivateRoom", deactivateRoomListener);
	}

	void OnDisable(){
		EventManagerScript.StopListening ("activateRoom", activateRoomListener);
		EventManagerScript.StopListening ("deactivateRoom", deactivateRoomListener);
	}

	void DeactivateChildren(){
		if (!active) {
			foreach (Transform child in transform) {
				child.gameObject.SetActive (false);
			}
		}
	}

	void ActivateChildren(){
		if (active) {
			if (!visited) {
				visited = true;
				enemiesAlive = enemies.Count;
				Transform enemiesParent = transform.FindChild ("Enemies");
				for (int i = 0; i < enemiesAlive; i++) {
					GameObject clone = (GameObject)Instantiate (enemies [i], enemyPositions [i], Quaternion.identity, enemiesParent);
					clone.transform.localPosition = enemyPositions [i];
					EnemyScript es = clone.GetComponent<EnemyScript> ();
					es.id = i;
					activeEnemiesID.Add (i);
				}
			} else {
				ResetRoom ();
			}

			foreach (Transform child in transform) {
				child.gameObject.SetActive (true);
			}

			if (lockdoors && enemiesAlive != 0) {
				EventManagerScript.TriggerEvent ("fightTrack");
				Transform doors = transform.FindChild ("Doors");
				foreach (Transform child in doors) {
					DoorScript ds = child.gameObject.GetComponent<DoorScript> ();
					ds.opened = false;
					ds.childAnim.ResetTrigger ("Open");
					ds.childAnim.SetTrigger ("Close");
				}
			} else {
				EventManagerScript.TriggerEvent ("noFightTrack");
				Transform doors = transform.FindChild ("Doors");
				foreach (Transform child in doors) {
					DoorScript ds = child.gameObject.GetComponent<DoorScript> ();
					ds.opened = true;
					ds.childAnim.ResetTrigger ("Close");
					ds.childAnim.SetTrigger ("Open");
				}
			}

			GameObject.Find ("Main Camera").GetComponent<CameraScript> ().setLimits(xMin, xMax, yMin, yMax, width);

		}
	}
}
