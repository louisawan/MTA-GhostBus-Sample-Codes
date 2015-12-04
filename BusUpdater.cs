using UnityEngine;
using System.Collections;

public class BusUpdater : MonoBehaviour {
	private MapNav gps;
	public GameObject prefab;
	public GameObject prefab2DWest;
	public GameObject prefab2DEast;
	public static int testBuses;
	UpdateBusLocation locationUpdater = new UpdateBusLocation();
	RetrieveAllBus busRetriever = new RetrieveAllBus();
	Vector3 newPos;
	Hashtable westboundVehicleTable = new Hashtable ();
	Hashtable westboundLongitudeTable = new Hashtable ();
	Hashtable westboundLatitudeTable = new Hashtable ();
	Hashtable westboundNextstopTable = new Hashtable ();
	Hashtable eastboundVehicleTable = new Hashtable ();
	Hashtable eastboundLongitudeTable = new Hashtable ();
	Hashtable eastboundLatitudeTable = new Hashtable ();
	Hashtable eastboundNextstopTable = new Hashtable ();
	GameObject westboundBus;
	GameObject eastboundBus;
	GameObject[] allBuses;
	GameObject[] all2DBusesEast;
	GameObject[] all2DBusesWest;
	Transform label;
	bool isDestroyed;
	// Use this for initialization
	IEnumerator Start () {
		gps = GameObject.FindGameObjectWithTag("GameController").GetComponent<MapNav>();
		while (!gps.gpsFix) {
			yield return null;
		}
		CallBuses ();
		StartCoroutine(DestroyAfterDelay (10.0f));

	}
	public void CallBuses()
	{	
		isDestroyed = false;
		busRetriever.ReadXML();
		westboundVehicleTable = busRetriever.vehicleTableBROOKLYN;
		westboundLongitudeTable = busRetriever.longitudeTableBROOKLYN;
		westboundLatitudeTable = busRetriever.latitudeTableBROOKLYN;
		westboundNextstopTable = busRetriever.nextstopTableBROOKLYN;
		eastboundVehicleTable = busRetriever.vehicleTableRIDGEWOOD;
		eastboundLongitudeTable = busRetriever.longitudeTableRIDGEWOOD;
		eastboundLatitudeTable = busRetriever.latitudeTableRIDGEWOOD;
		eastboundNextstopTable = busRetriever.nextstopTableRIDGEWOOD;
		for (int i=0; i<westboundVehicleTable.Count; i++) {
			float longitude = float.Parse (westboundLongitudeTable [i].ToString ());
			float latitude = float.Parse (westboundLatitudeTable [i].ToString ());
			Debug.Log (gps.triDScene);
			if(gps.triDScene)
			{westboundBus = Instantiate (prefab, locationUpdater.getNewPosition (latitude, longitude), Quaternion.Euler (0, 270, 0)) as GameObject;
			label = westboundBus.transform.Find ("BusLabel");
			label.GetComponent<TextMesh> ().text = westboundNextstopTable [i].ToString ();
			}
			else if (!gps.triDScene)
			{
				westboundBus = Instantiate (prefab2DWest, locationUpdater.getNewPosition (latitude, longitude), Quaternion.Euler (90, 0, 0)) as GameObject;
				label = westboundBus.transform.Find ("2DBusLabel");
				label.GetComponent<TextMesh> ().text = westboundNextstopTable [i].ToString ();
			}
		}
		for (int i=0; i<eastboundVehicleTable.Count; i++) {
			float longitude = float.Parse (eastboundLongitudeTable [i].ToString ());
			float latitude = float.Parse (eastboundLatitudeTable [i].ToString ());
			if(gps.triDScene)
			{
			eastboundBus = Instantiate (prefab, locationUpdater.getNewPosition (latitude, longitude), Quaternion.Euler (0, 90, 0)) as GameObject;
			label = eastboundBus.transform.Find ("BusLabel");
			label.GetComponent<TextMesh> ().text = eastboundNextstopTable [i].ToString ();
			}
			else if(!gps.triDScene)
			{
				eastboundBus = Instantiate (prefab2DEast, locationUpdater.getNewPosition (latitude, longitude), Quaternion.Euler (90, 0, 0)) as GameObject;
				label = eastboundBus.transform.Find ("2DBusLabel");
				label.GetComponent<TextMesh> ().text = eastboundNextstopTable [i].ToString ();
			}
		}
		testBuses = westboundVehicleTable.Count;
		Debug.Log ("WESTBOUND BUSES: " + testBuses);
		StartCoroutine(DestroyAfterDelay (10.0f));
	}
	// Update is called once per frame
	void Update () {
		if (isDestroyed) {
			Debug.Log ("Make next call");
			CallBuses();

		}
		
	}
	IEnumerator DestroyAfterDelay(float delay)
	{
		yield return new WaitForSeconds(delay);
		allBuses = GameObject.FindGameObjectsWithTag ("B54");
		all2DBusesEast = GameObject.FindGameObjectsWithTag ("B54EAST");
		all2DBusesWest = GameObject.FindGameObjectsWithTag ("B54WEST");
		for (int i=0; i<allBuses.Length; i++) {
			Destroy (allBuses[i]);
		}
		for (int i=0; i<all2DBusesEast.Length; i++) {
			Destroy (all2DBusesEast[i]);
		}
		for (int i=0; i<all2DBusesWest.Length; i++) {
			Destroy (all2DBusesWest[i]);
		}
		isDestroyed = true;

	}
}
