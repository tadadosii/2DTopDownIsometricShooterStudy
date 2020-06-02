using UnityEngine;

/// <summary>
/// basic script to draw gizmos and messages on the screen
/// </summary>
public class DebugHandler : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public bool drawGizmos;
    public bool drawGUI;

    private void OnDrawGizmos()
    {
        if (drawGizmos)
        {
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(MouseInput.mouseWorldPos, 0.15f);
        }
    }
    private void OnGUI()
    {
        if (drawGUI)
        {
            GUIStyle style = new GUIStyle();
            style.fontSize = Screen.height / 32;
            style.normal.textColor = Color.yellow;
            style.alignment = TextAnchor.MiddleCenter;
            string s0 = $"PlayerPos->MousePos: {MouseInput.directionFromPlayerToMouseWorldPos.x:0.0)} , {MouseInput.directionFromPlayerToMouseWorldPos.y:0.0}";
            string s1 = "Buttons: Alpha1: FireRate 0.125f | Alpha2: FireRate 0.05f | LMB: FireBasic | RMB: Charge->Fire | Q: Switch bullet";
            GUI.Label(new Rect(Screen.width / 2f, Screen.height - Screen.height / 14f, 0, 0), s0, style);
            GUI.Label(new Rect(Screen.width / 2f, Screen.height - Screen.height / 25f, 0, 0), s1, style);
        }
    }
}
