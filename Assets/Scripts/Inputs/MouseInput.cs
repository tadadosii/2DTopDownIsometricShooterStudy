using UnityEngine;

/// <summary>
/// Used to have static mouse input variables that can be easily accessed by any other class
/// </summary>
public class MouseInput : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public Transform player;

    public static Vector3 mousePixelPos;
    public static Vector3 mouseWorldPos;
    public static Vector2 directionFromPlayerToMouseWorldPos;

    private void Update()
    {
        mousePixelPos = Input.mousePosition;
        mousePixelPos.z = 20f;
        mouseWorldPos = Camera.main.ScreenToWorldPoint(mousePixelPos);
        mouseWorldPos.z = 0f;

        if (player != null)
            directionFromPlayerToMouseWorldPos = mouseWorldPos - player.position;
    }
}
