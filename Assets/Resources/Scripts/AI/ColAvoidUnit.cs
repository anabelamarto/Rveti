using UnityEngine;
using System.Collections;

public class ColAvoidUnit : MonoBehaviour {

	//public LinePath path;

	private SteeringBasics steeringBasics;
	//private FollowPath followPath;
	private CollisionAvoidance colAvoid;

	private NearSensor colAvoidSensor;

	// Use this for initialization
	void Start()
	{
		//path.calcDistances();

		steeringBasics = GetComponent<SteeringBasics>();
		//followPath = GetComponent<FollowPath>();
		colAvoid = GetComponent<CollisionAvoidance>();

//		colAvoidSensor = transform.Find("ColAvoidSensor").GetComponent<NearSensor>();
//		Debug.Log ("Near Sensor: " + colAvoidSensor);
		colAvoidSensor = transform.FindChild ("NearSensor").GetComponent<NearSensor> ();
//		Debug.Log ("Child Count: " + transform.childCount);
//		Debug.Log ("Near Sensor: " + colAvoidSensor).GetComponent<NearSensor> ();
//		colAvoidSensor = transform.GetChild (0);
		Debug.Log ("Near Sensor: " + colAvoidSensor);
		Debug.Log ("Targets : " + colAvoidSensor.targets);
		Debug.Log ("Targets To Ignore : " + colAvoidSensor.targetsToIgnore);
	}

	// Update is called once per frame
	void Update()
	{
//		path.draw();
//
//		if (isAtEndOfPath())
//		{
//			path.reversePath();
//		}

		Vector3 accel = colAvoid.getSteering(colAvoidSensor.targets);

//		if (accel.magnitude < 0.005f)
//		{
//			accel = followPath.getSteering(path);
//		}

		steeringBasics.steer(accel);
		steeringBasics.lookWhereYoureGoing();
	}

//	public bool isAtEndOfPath()
//	{
//		return Vector3.Distance(path.endNode, transform.position) < followPath.stopRadius;
//	}
}
