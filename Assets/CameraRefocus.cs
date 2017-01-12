using UnityEngine;
using Vuforia;

public class CameraRefocus : MonoBehaviour
{
    private readonly float focusUpdateEvery = Config.CameraRefocusTimer;
    private float focusTimer;

    void Update()
    {
        if (focusTimer <= 0)
        {
            focusTimer = focusUpdateEvery;
            Focus();
        }
        else
        {
            focusTimer -= Time.deltaTime;
        }
    }

    private void Focus()
    {
        CameraDevice.Instance.SetFocusMode(CameraDevice.FocusMode.FOCUS_MODE_TRIGGERAUTO);
    }
}
