using UnityEngine;
using System.Collections;

public class OwlStatueBuildBarrierScript : MonoBehaviour {

	private GameObject barriers;
	public Utilities.UtilitiesScript.Direction direction;
	private float verticalSeparation = 1.27f;
	private float horizontalSeparation = 1.28f;
	public float distance;

	void Awake () {
		int numberOfBarriers;
		if (direction == Utilities.UtilitiesScript.Direction.North) {
			numberOfBarriers = Mathf.RoundToInt(distance/verticalSeparation);
			barriers = (GameObject) Resources.Load ("Prefabs/OwlStatueBarrierVertical");
			BarrierCreation (numberOfBarriers, verticalSeparation, barriers, new Vector2 (0.75f, -0.1f), true);
		}
		if (direction == Utilities.UtilitiesScript.Direction.South) {
			numberOfBarriers =(int) distance / (int)verticalSeparation;
			barriers = (GameObject) Resources.Load ("Prefabs/OwlStatueBarrierVertical");
			BarrierCreation (numberOfBarriers, -verticalSeparation, barriers, new Vector2 (0.75f, -3.27f), true);
		}
		if (direction == Utilities.UtilitiesScript.Direction.East) {
			numberOfBarriers =(int) distance / (int)verticalSeparation;
			barriers = (GameObject) Resources.Load ("Prefabs/OwlStatueBarrierHorizontal");
			BarrierCreation (numberOfBarriers, horizontalSeparation, barriers, new Vector2 (2.36f, -2.24f),false);
		}
		if (direction == Utilities.UtilitiesScript.Direction.West) {
			numberOfBarriers =(int) distance / (int)verticalSeparation;
			barriers = (GameObject) Resources.Load ("Prefabs/OwlStatueBarrierHorizontal");
			BarrierCreation (numberOfBarriers, -horizontalSeparation, barriers, new Vector2 (-1.43f, -2.24f), false);
		}
	}

	void BarrierCreation(int n, float separation, GameObject bar, Vector2 startingPoint, bool vertical){
		for (int i = 0; i < n; i++) {
			GameObject obj = (GameObject) Instantiate (bar, transform.position, Quaternion.identity, transform);
			if (vertical) {
				float s = startingPoint.y + (separation * i);
				obj.transform.localPosition = new Vector2 (startingPoint.x, s);
			} else {
				float x = startingPoint.x + (separation * i);
				obj.transform.localPosition = new Vector2 (x, startingPoint.y);
			}
		}
	}
}
