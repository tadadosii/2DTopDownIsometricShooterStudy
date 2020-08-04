using UnityEngine;

public class PlayerShoulderMain : MonoBehaviour
{
    [TextArea(2, 4)]
    public string notes = "This class only exists to act as a realiable way to let other " +
        "classes find it (e.g. CrosshairJoystick).";
}
