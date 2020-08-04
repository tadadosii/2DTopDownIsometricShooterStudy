using UnityEngine;

/// <summary>
/// This class uses TadaInput.VectorPlayerToMouseWPos to move the local position of a gameobject that's
/// used as a look at target by the non-trigger hand.
/// </summary>
public class PlayerHandTargetToLookAt : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(7,10)]
    public string Notes = "This class uses the AimDirection of the Crosshair to move the local position of a " +
        "gameobject that's used as a look at target by the non-trigger hand.\nThis values are hardcoded in " +
        "the serialized properties, feel free to mess around with them to get a feeling of what each of them " +
        "do. To reset the values simply right click on top of the script name and hit Reset.";
    [Space(10)]

    [SerializeField] private float lerpSpeed = 2f;
    [SerializeField] private Vector2 sensitivity = new Vector2(0.1f, 1f);
    [SerializeField] private Vector2 initialOffset = new Vector2(0.25f, 0.18f);
    [SerializeField] private Vector2 minMaxOffsetX = new Vector2 (-1f, 0.3f);
    [SerializeField] private Vector2 minMaxOffsetY = new Vector2(0.18f, 1f);

    private Vector2 aimDirection;

    private void Update()
    {
        float clampX = 0.0f;
        float clampY = 0.0f;

        if (TadaInput.IsMouseActive)
            aimDirection = CrosshairMouse.AimDirection;
        else
            aimDirection = CrosshairJoystick.AimDirection;

        if (aimDirection.y > 0)
            clampX = Mathf.Clamp(initialOffset.x - aimDirection.y * sensitivity.x, minMaxOffsetX.x, minMaxOffsetX.y);
        else if (aimDirection.y < 0)
            clampX = Mathf.Clamp(initialOffset.x + aimDirection.y * sensitivity.x, minMaxOffsetX.x, minMaxOffsetX.y);

        clampY = Mathf.Clamp(initialOffset.y - aimDirection.y * sensitivity.y, minMaxOffsetY.x, minMaxOffsetY.y);

        transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2 (clampX, clampY), Time.deltaTime * lerpSpeed);
    }
}
