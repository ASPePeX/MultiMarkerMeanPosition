using UnityEngine;
using Vuforia;

public class CameraRefocus : MonoBehaviour
{
    private float focusTimer;
    private float touchFocusTimer;

    void Update()
    {
        if (Input.touchCount > 0 && touchFocusTimer <= 0)
        {
            touchFocusTimer = Config.CameraRefocusOnTouchCooldown;
            Focus();
        }

        if (touchFocusTimer > 0)
        {
            touchFocusTimer -= Time.deltaTime;
        }

        if (focusTimer <= 0)
        {
            focusTimer = Config.CameraAutoRefocusCooldown;
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
