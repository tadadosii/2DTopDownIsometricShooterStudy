using UnityEngine;

/// <summary>
/// This class derives from the base class Crosshair. It overrides the method UpdateCrosshair 
/// to snap the crosshair position to <see cref="TadaInput.MouseWorldPos"/>.
/// </summary>
public class CrosshairMouse : Crosshair
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(3, 10)]
    public string notes = "This class derives from the base class Crosshair. It overrides the method UpdateCrosshair " +
    "to snap the crosshair position to TadaInput.MouseWorldPos";

    private Transform player;

    /// <summary>
    /// Vector that goes from player position to crosshair world position.
    /// </summary>
    public static Vector3 AimDirection
    {
        get { return _AimDirection; }
        private set { _AimDirection = value; }
    }
    private static Vector3 _AimDirection;

    protected override void Awake()
    {
        base.Awake();
        player = FindObjectOfType<PlayerController>().transform;
    }

    public override void UpdateCrosshair()
    {
        base.UpdateCrosshair();
        crosshair.transform.position = TadaInput.MouseWorldPos;

        _AimDirection = (crosshair.transform.position - player.position).normalized;
        _AimDirection.z = 0f;
    }
}
