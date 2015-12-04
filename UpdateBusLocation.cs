using UnityEngine;
using System.Collections;
public class UpdateBusLocation : MonoBehaviour {
	private float height = 154;
	public MapNav gps;
	private bool gpsFix;
	private float initX;
	private float initZ;
	private float fixLatitude;
	private float fixLongitude;
 	public BusUpdater bupdater;
	public float orientation;
	private float scaleX = 1;
	private float scaleY = 1;
	private float scaleZ = 1;
	private Vector3 nextPos;
	// Sample test coordinate
	Vector3 nextStop = new Vector3 (8.4f, 1.54f, -0.35f);
	public static float usLat;
	public static float usLon;

	IEnumerator Start()
	{	
		while (!gps.gpsFix)
		{
			gpsFix = gps.gpsFix;
			yield return null;
		}

	}

	public Vector3 getNewPosition(float busLat, float busLong)
	{	gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
		Debug.Log ("getNewPosition is executed");
		initX = gps.iniRef.x;
		initZ = gps.iniRef.z;
		if (gps.simGPS) {

			fixLatitude = gps.fixLat;
			fixLongitude = gps.fixLon;
		} else {
			fixLatitude = gps.userLat;
			fixLongitude = gps.userLon;

			Debug.Log (gps.userLat);
			Debug.Log(gps.userLon);
		}

		//Translate the geographical coordinate system used by gps mobile devices(WGS84), into Unity's Vector2 Cartesian coordinates(x,z) and set height(1:100 scale).
		nextPos = new Vector3(((busLong * 20037508.34f) / 18000) - initX, height/100, ((Mathf.Log(Mathf.Tan((90 + busLat) * Mathf.PI / 360)) / (Mathf.PI / 180)) * 1113.19490777778f) - initZ);

		return nextPos;
		//Debug.Log ("NEXT POSITION: " + nextPos);

	}


}
