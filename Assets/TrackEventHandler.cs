using UnityEngine;
using Vuforia;

public class TrackEventHandler : MonoBehaviour, ITrackableEventHandler {

    private TrackableBehaviour _mTrackableBehaviour;
    private TrackableManager _trackableManager;

    private bool ManagerReady;

    void Start()
    {
        _trackableManager = GameObject.Find("GameController").GetComponent<TrackableManager>();

        _mTrackableBehaviour = GetComponent<TrackableBehaviour>();
        if (_mTrackableBehaviour)
        {
            _mTrackableBehaviour.RegisterTrackableEventHandler(this);
        }

        ManagerReady = true;
    }

    public void OnTrackableStateChanged(TrackableBehaviour.Status previousStatus, TrackableBehaviour.Status newStatus)
    {
        if (ManagerReady)
        {
            if (newStatus == TrackableBehaviour.Status.DETECTED ||
                newStatus == TrackableBehaviour.Status.TRACKED ||
                newStatus == TrackableBehaviour.Status.EXTENDED_TRACKED)
            {
                OnTrackingFound();
            }
            else
            {
                OnTrackingLost();
            }
        }
    }

    private void OnTrackingFound()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(true);
        }

        _trackableManager.AddTrackable(this.gameObject);

        Debug.Log("Trackable " + _mTrackableBehaviour.TrackableName + " found - this shit is custom yo!");
    }

    private void OnTrackingLost()
    {
        for (int i = 0; i < transform.childCount; i++)
        {
            transform.GetChild(i).gameObject.SetActive(false);
        }

        _trackableManager.RemoveTrackable(this.gameObject);

        Debug.Log("Trackable " + _mTrackableBehaviour.TrackableName + " lost");
    }
}
