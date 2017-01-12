using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.VR.WSA;

public class TrackableManager : MonoBehaviour
{
    private HashSet<GameObject> register;
    private LineRenderer lr;
    private GameObject center;
    private Vector3 centerpos;

	// Use this for initialization
	void Start () {
		register = new HashSet<GameObject>();
	    lr = GameObject.Find("Lines").GetComponent<LineRenderer>();
	    center = GameObject.Find("Sphere");
	    centerpos = center.transform.position;
	}

	// Update is called once per frame
	void Update () {

	    // Line stuff
	    if (register.Count > 1)
	    {
	        lr.enabled = true;
	        lr.numPositions = register.Count +1;

	        Vector3[] positions = new Vector3[register.Count +1];
	        int cnt = 0;

	        float meanX = 0;
	        float meanY = 0;
	        float meanZ = 0;

	        foreach (var go in register)
	        {
	            positions[cnt] = go.transform.position;
	            meanX += positions[cnt].x;
	            meanY += positions[cnt].y;
	            meanZ += positions[cnt].z;

	            cnt++;
	        }

	        positions[cnt] = positions[0];

	        centerpos.x = meanX / register.Count;
	        centerpos.y = meanY / register.Count;
	        centerpos.z = meanZ / register.Count;

	        center.transform.position = centerpos;

	        lr.SetPositions(positions);
	    }
	    else
	    {
	        lr.enabled = false;
	    }
	}

    public void AddTrackable(GameObject go)
    {
        if (!register.Contains(go))
        {
            register.Add(go);
        }
        else
        {
            Debug.LogWarning("Duplicate object!");
        }
    }

    public void RemoveTrackable(GameObject go)
    {
        if (register.Contains(go))
        {
            register.Remove(go);
        }
    }
}
