using UnityEngine;

/// <summary>
/// This class uses MouseInput direction player -> mouse world pos to move the local position of a gameobject that's
/// used as a look at target by the hands positioned in the weapon handguard
/// </summary>
public class HandTargetToLookAt : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public float lerpRate = 2f;
    public Vector2 sensitivity = new Vector2(0.1f, 1f);
    public Vector2 initialOffset = new Vector2(0.25f, 0.18f);
    public Vector2 minMaxOffsetX = new Vector2 (-1f, 0.3f);
    public Vector2 minMaxOffsetY = new Vector2(0.18f, 1f);

    private void Update()
    {
        float clampX = 0.0f;
        if (MouseInput.directionFromPlayerToMouseWorldPos.y > 0)
            clampX = Mathf.Clamp(initialOffset.x - MouseInput.directionFromPlayerToMouseWorldPos.y * sensitivity.x, minMaxOffsetX.x, minMaxOffsetX.y);
        else if (MouseInput.directionFromPlayerToMouseWorldPos.y < 0)
            clampX = Mathf.Clamp(initialOffset.x + MouseInput.directionFromPlayerToMouseWorldPos.y * sensitivity.x, minMaxOffsetX.x, minMaxOffsetX.y);

        float clampY = Mathf.Clamp(initialOffset.y - MouseInput.directionFromPlayerToMouseWorldPos.y * sensitivity.y, minMaxOffsetY.x, minMaxOffsetY.y);
        transform.localPosition = Vector2.Lerp(transform.localPosition, new Vector2 (clampX, clampY), Time.deltaTime * lerpRate);
    }
}
