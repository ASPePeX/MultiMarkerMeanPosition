using System.Collections.Generic;
using UnityEngine;

public class TrackableManager : MonoBehaviour
{
    private HashSet<GameObject> register;
    private LineRenderer lr;
    private GameObject center;
    private Vector3 centerpos;

    public HashSet<GameObject> Register
    {
        get { return register; }
    }

    // Use this for initialization
	void Start () {
		register = new HashSet<GameObject>();
	    lr = GameObject.Find("Lines").GetComponent<LineRenderer>();
	    center = GameObject.Find("SphereDolly");
	    centerpos = center.transform.position;
	}

	// Update is called once per frame
	void Update () {
	    if (register.Count > 0)
	    {
	        List<Vector3> positions = new List<Vector3>(register.Count+1);

	        float meanX = 0;
	        float meanY = 0;
	        float meanZ = 0;

	        foreach (var go in register)
	        {
	            Vector3 pos = go.transform.position + go.transform.rotation * go.GetComponent<TrackerData>().PositionOffset;

                positions.Add(pos);
	            meanX += pos.x;
	            meanY += pos.y;
	            meanZ += pos.z;
	        }

	        if (register.Count > 2)
	            positions.Add(positions[0]);

	        centerpos.x = meanX / register.Count;
	        centerpos.y = meanY / register.Count;
	        centerpos.z = meanZ / register.Count;

	        center.transform.position = centerpos;

	        if (register.Count > 1)
	        {
	            lr.enabled = true;
	            lr.numPositions = positions.Count;
	            lr.SetPositions(positions.ToArray());
	        }
	        else
	        {
	            lr.enabled = false;
	        }

	        center.SetActive(true);
	    }
	    else
	    {
	        center.SetActive(false);
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
        if (go != null && register.Contains(go))
        {
            register.Remove(go);
        }
    }
}
