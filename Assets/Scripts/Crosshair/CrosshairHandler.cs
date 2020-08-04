using UnityEngine;

/// <summary>
/// This class has conditions to determine which of the crosshairs should be enabled and updating. 
/// </summary>
public class CrosshairHandler : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(3, 10)]
    public string notes = "This class has conditions to determine which of the crosshairs should be enabled and updating.";

    private CrosshairMouse mouseCrosshair;
    private CrosshairJoystick joystickCrosshair;

    private bool isReady;

    private void Awake()
    {
        mouseCrosshair = FindObjectOfType<CrosshairMouse>();
        joystickCrosshair = FindObjectOfType<CrosshairJoystick>();
    }

    private void Update()
    {
        CheckIfReady();

        if (!TadaInput.IsMouseActive)
        {
            if (mouseCrosshair.IsActive)
            {
                mouseCrosshair.IsActive = false;
                joystickCrosshair.IsActive = true;
            }

            joystickCrosshair.UpdateCrosshair();
        }
        else
        {
            if (!mouseCrosshair.IsActive)
            {
                mouseCrosshair.IsActive = true;
                joystickCrosshair.IsActive = false;
            }

            mouseCrosshair.UpdateCrosshair();
        }
    }

    private void CheckIfReady()
    {
        if (!isReady)
        {
            if (mouseCrosshair == null || joystickCrosshair == null)
            {
                Debug.LogError(gameObject.name + ": mouse or/and joystick crosshair missing!");
                return;
            }

            isReady = true;
        }
    }
}
