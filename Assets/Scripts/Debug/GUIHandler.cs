using UnityEngine;

/// <summary>
/// Basic class to draw messages on the screen.
/// </summary>
public class GUIHandler : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(1, 10)]
    public string notes = "Basic class to draw messages on the screen.";

    [SerializeField] private bool drawGUI = true;

    private void OnGUI()
    {
        if (drawGUI)
        {
            GUIStyle style = new GUIStyle
            {
                fontSize = Screen.height / 40,
                alignment = TextAnchor.MiddleCenter
            };

            // Not exactly sure of how can I pass this inside the GUIStyle information or if it's actually possible to do it...
            style.normal.textColor = Color.yellow;
            
            string s0 = $"PlayerPos->MousePos: {CrosshairMouse.AimDirection.x:0.0} , {CrosshairMouse.AimDirection.y:0.0}";
            string s1 = $"PlayerPos->CrosshairJoyDir: {CrosshairJoystick.AimDirection.x:0.0} , {CrosshairJoystick.AimDirection.y:0.0}";
            string s2 = "WASD: Move | Mouse: Aim | Space: Dash | 1: Weapon0 | 2: Weapon2 | LMB: Act1 | RMB: Act2 | Q/E: SwitchWpn | C/V: SwitchFireRate | Esc: Pause";
            string s3 = "LStick: Move | RStick: Aim | RB: Dash | RTrigger: Action1 | LTrigger: Action2 | A/B: SwitchWeapon | LStickButton: SwitchFireRate | Start: Pause";
            string s4 = "";

            if (TadaInput.IsMouseActive)
                s4 = s2;
            else
                s4 = s3;

            GUI.Label(new Rect(Screen.width / 2f, Screen.height - Screen.height / 10f, 0, 0), s0, style);
            GUI.Label(new Rect(Screen.width / 2f, Screen.height - Screen.height / 15f, 0, 0), s1, style);
            GUI.Label(new Rect(Screen.width / 2f, Screen.height - Screen.height / 30f, 0, 0), s4, style);
        }
    }
}
