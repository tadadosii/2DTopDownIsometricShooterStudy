using UnityEngine;

/// <summary>
/// Basic class to draw gizmos on the screen.
/// </summary>
public class GizmosHandler : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(1, 10)]
    public string notes = "Basic class to draw gizmos on the screen.";

    [SerializeField] private bool drawGizmos = true;

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(TadaInput.MouseWorldPos, 0.15f);
            Gizmos.DrawWireSphere(CrosshairJoystick.CrosshairPosition, 0.15f);
        }
    }
}
