using System;
using System.IO;
using System.Text;
using System.Text.RegularExpressions;
using UnityEngine;

public class WritCalibration : MonoBehaviour
{

    private TrackableManager _trackableManager;

	// Use this for initialization
	void Start () {
        _trackableManager = GameObject.Find("GameController").GetComponent<TrackableManager>();
    }
	
	// Update is called once per frame
	void Update () {
	    if (Input.touchCount > 0)
	    {
	        SaveData();
	    }
	}

    private void SaveData()
    {
        File.AppendAllText(SaveFilePath, Data);
    }

    private string Data
    {
        get
        {
            StringBuilder bla = new StringBuilder();
            DateTime now = DateTime.Now;

            bla.Append(now + "\n");

            foreach (var o in _trackableManager.Register)
            {
                string posor = o.transform.position.ToString("F8") + "," + o.transform.rotation.eulerAngles.ToString("F8");

                posor = Regex.Replace(posor, @"[\(\)\s]", "", RegexOptions.None);

                bla.Append("\"" + o.name + "\"," + posor + "\n");
            }
            return bla.ToString();
        }
    }

    public string SaveFilePath
    {
        get { return Application.persistentDataPath + "/calibration.txt"; }
    }
}
