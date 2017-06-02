using UnityEngine;
using UnityEngine.UI;
using Vuforia;

public class ObjectFace : MonoBehaviour
{
    [SerializeField]
    private Transform camPos;
    private Transform objPos;
    private Transform tracer;

    [SerializeField]
    private Text text;

	// Use this for initialization
	void Start ()
	{
	    camPos = Camera.main.transform;
	    objPos = GameObject.FindGameObjectWithTag("MainTarget").transform;

	    tracer = Instantiate(new GameObject(), objPos).transform;
	    tracer.parent = objPos;
	    tracer.Translate(0.5f, 0, 1f/3f);
	}
	
	// Update is called once per frame
	void Update () {
		tracer.transform.LookAt(camPos);

	    Vector3 rotation = tracer.rotation.eulerAngles;
	    string side;

	    if (rotation.y > 315 || rotation.y < 45)
	    {
	        side = "left";
	    }
        else if (rotation.y > 45 && rotation.y < 135)
	    {
	        side = "back";
	    }
        else if (rotation.y > 135 && rotation.y < 225)
	    {
	        side = "right";
	    }
        else if (rotation.y > 225 && rotation.y < 315)
	    {
	        side = "front";
	    }
	    else
	    {
	        side = "bla";
	    }

	    if (rotation.x < 310)
	    {
	        side = side + " top";
	    }

        text.text = rotation.ToString() + "\n" + side;
	}
}
