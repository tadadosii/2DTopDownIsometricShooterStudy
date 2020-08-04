using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(4, 10)]
    public string notes = "At the moment the camera can smoothly follow a Transform target by using transform.position = Vector2.Lerp() " +
        "and it can offset its position to look towards the crosshair position";

    public float FollowSpeed { get { return _FollowSpeed; } set { _FollowSpeed = value; } }
    [SerializeField] private float _FollowSpeed = 3f;

    [SerializeField] private float _MaxOffset = 3f;

    public Transform target;

    [SerializeField] private bool isOffsetActive = true;

    private Vector2 offset;

    private void FixedUpdate()
    {
        if (target == null)
        {
            Debug.LogWarning(gameObject.name + ": Target missing!");
            return;
        }

        if (PauseController.isGamePaused)
            return;

        if (isOffsetActive)
        {
            // Offset the camera to look at the position where the crosshair is pointing at.
            if (TadaInput.IsMouseActive)
                offset = Vector2.ClampMagnitude(CrosshairMouse.AimDirection, _MaxOffset);
            else
                offset = TadaInput.AimAxisSmoothInput * _MaxOffset;
        }
        else
            offset = Vector2.zero;

        Vector3 targetPosition = new Vector3(target.position.x + offset.x, target.position.y + offset.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, targetPosition, Time.deltaTime * _FollowSpeed);
    }
}
