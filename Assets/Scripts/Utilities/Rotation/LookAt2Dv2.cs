using UnityEngine;

/// <summary>
/// New v2. The gameobject that has this component attached will instantly rotate to make its x or y axis look 
/// towards the assigned target or towards mouse world position if a exposed enum is selected. The direction can be
/// inverted by checking isFlipAxis. Also there is an option to disable local update if a linked control is 
/// needed. It can also use a smooth rotation by enabling isSmoothRotationEnable.
/// </summary>
public class LookAt2Dv2 : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(4, 10)]
    public string notes = "New v2. The gameobject that has this component attached will instantly rotate to make its x or y axis look " +
        "towards the assigned target or towards mouse world position if a exposed enum is selected. The direction can be inverted by " +
        "checking isFlipAxis. Also there is an option to disable local update if a linked control is needed. It can also use a " +
        "smooth rotation by enabling isSmoothRotationEnable.";

    // TargetTransform: Look at the gameobject Transform from the public variable targetTransform.
    // MouseWorldPosition: Look at the mouse world position stored by the TadaInput class.
    public enum LookAtTarget { TargetTransform, MouseWorldPosition }
    [SerializeField] private LookAtTarget lookAtTarget = LookAtTarget.TargetTransform;

    [Tooltip("If you are using a Transform, select TargetTransform from lookAtTarget dropdown list.")]
    public Transform targetTransform;

    private enum Axis { X, Y }
    [SerializeField] private Axis axis = Axis.Y;

    [Tooltip("Used when isSmoothRotationEnable is true.")]
    [SerializeField] private float turnRate = 10f;

    [Tooltip("Use to set an initial offset angle or use SetOffsetAngle method to do it via code.")]
    [SerializeField] private float offsetLookAtAngle = 0f;

    [Tooltip("e.g. writing 30 will make the axis have a range of -30 to 30 degrees.")]
    [SerializeField] private float maxAngle = 360f;

    [Tooltip("Check to let this behaviour be run by the local Update() method and Uncheck if you want to call it from any other class by using UpdateLookAt().")]
    [SerializeField] private bool isUpdateCalledLocally = false;

    [Tooltip("Check to smoothly rotate towards target rotation using turnRate as variable.")]
    public bool isSmoothRotationEnable = false;

    [Tooltip("Check to flip the axis and use the negative side to look at")]
    public bool isFlipAxis = false;

    [Header("Debug")]
    [SerializeField] private Color debugColor = Color.green;
    [SerializeField] private bool debug = false;

    private Vector3 targetPosition;
    private Vector3 direction;
    private Vector3 upwardAxis; 

    private void Update()
    {
        if (!isUpdateCalledLocally)
            return;
        UpdateLookAt();
    }

    public void UpdateLookAt()
    {
        Vector3 myPosition = transform.position;

        if (lookAtTarget == LookAtTarget.MouseWorldPosition)
            targetPosition = TadaInput.MouseWorldPos;
        else if ((lookAtTarget == LookAtTarget.TargetTransform))
        {
            if (targetTransform == null)
            {
                Debug.LogError(gameObject.name + " target missing!");
                return;
            }
            targetPosition = targetTransform.position;
        }

        // Ensure there is no 3D rotation by aligning Z position
        targetPosition.z = myPosition.z;

        // Vector from this object towards the target position
        direction = (targetPosition - myPosition).normalized;

        switch (axis)
        {
            case Axis.X:

                if (!isFlipAxis)
                {
                    // Rotate direction by 90 degrees around the Z axis
                    upwardAxis = Quaternion.Euler(0, 0, 90 + offsetLookAtAngle) * direction;
                }
                else
                {
                    // Rotate direction by -90 degrees around the Z axis
                    upwardAxis = Quaternion.Euler(0, 0, -90 + offsetLookAtAngle) * direction;
                }
                break;

            case Axis.Y:

                if (!isFlipAxis)
                    upwardAxis = direction;
                else
                    upwardAxis = -direction;
                break;

            default:
                break;
        }

        // Get the rotation that points the Z axis forward, and the X or Y axis 90° away from the target
        // (resulting in the Y or X axis facing the target).
        Quaternion targetRotation = Quaternion.LookRotation(forward: Vector3.forward, upwards: upwardAxis);

        if (debug)
            Debug.DrawLine(transform.position, targetPosition, debugColor);

        if (!isSmoothRotationEnable)
        {
            // Update the rotation if it's inside the maxAngle limits.
            if (Quaternion.Angle(Quaternion.identity, targetRotation) < maxAngle)
                transform.rotation = targetRotation;
            return;
        }

        // Smooth rotation.
        Quaternion rotation = Quaternion.Lerp(transform.rotation, targetRotation, turnRate * Time.deltaTime);

        // Update the rotation if it's inside the maxAngle limits.
        if (Quaternion.Angle(Quaternion.identity, rotation) < maxAngle)
            transform.rotation = rotation;
    }

    public void SwitchToTarget(LookAtTarget target)
    {
        lookAtTarget = target;
    }

    public void SetOffsetAngle(float value)
    {
        offsetLookAtAngle = value;
    }
}
