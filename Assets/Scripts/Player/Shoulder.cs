using UnityEngine;

/// <summary>
/// This class uses MouseInput direction player -> mouse world pos and other variables like rate, min offset angle and
/// max offset angle to determine an offset angle that will be added to the shoulder as an extra rotation. This is an
/// important part of the behaviour that controls the rotation of the shoulders, with the correct values both shoulders match
/// their rotation in relation to the weapon that's being held by the character.
/// </summary>
public class Shoulder : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------

    public float rate;
    public float minOffsetAngle;
    public float maxOffsetAngle;
    public Transform shoulderRight;
    public bool localUpdate;

    private void Update()
    {
        if (!localUpdate)
            return;
        UpdateRotation();
    }

    public void UpdateRotation()
    {
        float clampAngle = Mathf.Clamp(MouseInput.directionFromPlayerToMouseWorldPos.y * rate, minOffsetAngle, maxOffsetAngle);
        transform.rotation = shoulderRight.rotation;
        transform.rotation = Quaternion.Euler(0, 0, transform.eulerAngles.z + clampAngle);
    }
}
