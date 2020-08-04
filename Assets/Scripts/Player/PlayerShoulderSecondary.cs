using UnityEngine;

/// <summary>
/// This class uses the Crosshair AimDirection and other properties like rate, min offset angle and
/// max offset angle to determine an offset angle that will be added to the secondary shoulder as an extra rotation. 
/// This is an important part of the behaviour that controls the rotation of the shoulders, with the correct 
/// values both shoulders match their rotation in relation to the weapon that's being held by the character.
/// </summary>
public class PlayerShoulderSecondary : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(4, 10)]
    public string notes = "This class uses the Crosshair AimDirection and other properties like rate, min offset angle and " +
        "max offset angle to determine an offset angle that will be added to the secondary shoulder as an extra rotation. " +
        "This is an important part of the behaviour that controls the rotation of the shoulders, with the correct values " +
        "both shoulders match their rotation in relation to the weapon that's being held by the character.";

    [SerializeField] private float rate = 60f;
    [SerializeField] private float minOffsetAngle = 0f;
    [SerializeField] private float maxOffsetAngle = 50f;
    public Transform shoulderMain;

    [Tooltip("Check to let this behaviour be run by the local Update() method and " +
        "Uncheck if you want to call it from any other class by using UpdateLookAt().")]
    [SerializeField] private bool isUpdateCalledLocally = false;

    private float clampAngle;

    private void Update()
    {
        if (!isUpdateCalledLocally)
            return;
        UpdateRotation();
    }

    public void UpdateRotation()
    {
        // I got this hardcoded values after testing them in the Inspector.
        if (PlayerBodyPartsHandler.isRightDirection)
        {
            minOffsetAngle = 0f;
            maxOffsetAngle = 50f;
        }
        else
        {
            minOffsetAngle = -50f;
            maxOffsetAngle = -50f;
        }

        if (TadaInput.IsMouseActive)
            clampAngle = Mathf.Clamp(CrosshairMouse.AimDirection.y * rate, minOffsetAngle, maxOffsetAngle);
        else
            clampAngle = Mathf.Clamp(CrosshairJoystick.AimDirection.y * rate, minOffsetAngle, maxOffsetAngle);

        transform.rotation = shoulderMain.rotation;
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + clampAngle);
    }
}
