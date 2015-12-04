using UnityEngine;
using System.Collections;
using System.IO;
using System.Xml;
using System.Net;

public class RetrieveAllBus : MonoBehaviour {
	public string url = "http://bustime.mta.info/api/siri/vehicle-monitoring.xml?key=9030507a-62c2-4168-825d-ddf477ec1b22&OperatorRef=MTA+NYCT&LineRef=B54";
	string filePath;
	string xmlContent;
	// x = longitude ; y = latitude
	float[] longitude;
	float[] latitude;
	// Use this for initialization
	int vehicleCount;
	public Hashtable vehicleTableBROOKLYN;
	public Hashtable longitudeTableBROOKLYN;
	public Hashtable latitudeTableBROOKLYN;
	public Hashtable nextstopTableBROOKLYN;
	public Hashtable vehicleTableRIDGEWOOD;
	public Hashtable longitudeTableRIDGEWOOD;
	public Hashtable latitudeTableRIDGEWOOD;
	public Hashtable nextstopTableRIDGEWOOD;
	public static string fileLoaded = "NOT LOADED";
	Hashtable stops = new Hashtable();
	Hashtable stopsRIDGEWOOD = new Hashtable();
	// Get specific XML data and store them into the DataTable
	public string file;
	IEnumerator getFile()
	{
		WWW www = new WWW(url);
		yield return www;
		file = www.text;
	}
	public void ReadXML()
	{
		fileLoaded = "FILE SUCCESSFULLY RETRIEVED";
		XmlDocument doc = new XmlDocument ();
		doc.Load (url);
		Debug.Log (doc);
		XmlNodeList vehicleList = doc.GetElementsByTagName("VehicleRef");
		XmlNodeList directionList = doc.GetElementsByTagName("DestinationName");
		XmlNodeList longitudeList = doc.GetElementsByTagName("Longitude");
		XmlNodeList latitudeList = doc.GetElementsByTagName("Latitude");
		XmlNodeList nextstopList = doc.GetElementsByTagName("StopPointName");
		// Store number of vehicles
		vehicleCount = vehicleList.Count;
		// Create Hashtables to store elements
		vehicleTableBROOKLYN = new Hashtable ();
		longitudeTableBROOKLYN = new Hashtable ();
		latitudeTableBROOKLYN = new Hashtable ();
		nextstopTableBROOKLYN = new Hashtable ();
		vehicleTableRIDGEWOOD = new Hashtable ();
		longitudeTableRIDGEWOOD = new Hashtable ();
		latitudeTableRIDGEWOOD = new Hashtable ();
		nextstopTableRIDGEWOOD = new Hashtable ();
		// Store elements with vehicleList as Hashtable keys
		int westCounter=0;
		int eastCounter=0;
		for (int i=0; i < vehicleList.Count; i++)
		{   
			//Vehicle Ref = MTA NYCT_4525
			// 	Vehicle Direction = [Jay St. To St. Nicholas: "RIDGEWOOD TERM via MYRTLE"][St. Nicholas to Jay St.: "DNTWN BKLYN JAY ST via MYRTLE"]
			if(directionList[i].InnerXml == "DNTWN BKLYN JAY ST via MYRTLE")
			{	
				vehicleTableBROOKLYN.Add(westCounter, Substring (vehicleList[i].InnerXml, 9, 13));
				longitudeTableBROOKLYN.Add (westCounter, longitudeList[i].InnerXml);
				latitudeTableBROOKLYN.Add (westCounter, latitudeList[i].InnerXml);
				nextstopTableBROOKLYN.Add (westCounter, nextstopList[i].InnerXml);
				westCounter +=1;

			}
			else if(directionList[i].InnerXml == "RIDGEWOOD TERM via MYRTLE")
			{
				vehicleTableRIDGEWOOD.Add(eastCounter, Substring (vehicleList[i].InnerXml, 9, 13));
				longitudeTableRIDGEWOOD.Add (eastCounter, longitudeList[i].InnerXml);
				latitudeTableRIDGEWOOD.Add (eastCounter, latitudeList[i].InnerXml);
				nextstopTableRIDGEWOOD.Add (eastCounter, nextstopList[i].InnerXml);
				eastCounter +=1;
			}
		}
		Debug.Log (vehicleList.Count);
	}

	string Substring(string word, int start, int end)
	{	
		string newWord = "";
		for (int i=start; i < end; i++) {
			newWord += word[i];
		}
		return newWord;
	}

	public void getAllStops()
	{
		// Default: WESTBOUND
		stops[0]="PALMETTO ST/ST NICHOLAS AV";
		stops[1]="MYRTLE AV/GATES AV";
		stops[2]="MYRTLE AV/IRVING AV";
		stops[3]="MYRTLE AV/KNICKERBOCKER AV";
		stops[4]="MYRTLE AV/HARMAN ST";
		stops[5]="MYRTLE AV/WILSON AV";
		stops[6]="MYRTLE AV/DE KALB AV";
		stops[7]="MYRTLE AV/HART ST";
		stops[8]="MYRTLE AV/SUYDAM ST";
		stops[9]="MYRTLE AV/CHARLES PL";
		stops[10]="MYRTLE AV/BUSHWICK AV";
		stops[11]="MYRTLE AV/BROADWAY";
		stops[12]="MYRTLE AV/LEWIS AV";
		stops[13]="MYRTLE AV/MARCUS GARVEY BL";
		stops[14]="MYRTLE AV/THROOP AV";
		stops[15]="MYRTLE AV/TOMPKINS AV";
		stops[16]="MYRTLE AV/MARCY AV";
		stops[17]="MYRTLE AV/NOSTRAND AV";
		stops[18]="MYRTLE AV/WALWORTH ST";
		stops[19]="MYRTLE AV/BEDFORD AV";
		stops[20]="MYRTLE AV/FRANKLIN AV";
		stops [21] = "MYRTLE AV/CLASSON AV";
		stops[22]="MYRTLE AV/GRAND AV";
		stops[23]="MYRTLE AV/WASHINGTON AV";
		stops[24] = "MYRTLE AV/CLINTON AV";
		stops[25]="MYRTLE AV/VANDERBILT AV";
		stops[26]="MYRTLE AV/CARLTON AV";
		stops[27]="MYRTLE AV/N PORTLAND AV";
		stops[28]="MYRTLE AV/ST EDWARDS ST";
		stops[29]="MYRTLE AV/NAVY ST";
		stops[30]="MYRTLE AV/PRINCE ST";
		stops[31]="METROTECH UNDERPASS/LAWRENCE ST";
		stops[32]="JAY ST/MYRTLE PLZ";
		int ridgewoodCounter = 0;
		// Reverse Sort
		for(int i=stops.Count; i>0; i--)
		{
			stopsRIDGEWOOD[ridgewoodCounter] = stops[i];
			ridgewoodCounter += 1;
		}

	}


}
