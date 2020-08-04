using UnityEngine;

/// <summary>
/// Controls the behaviour of the player's body based on MouseInput.vectorFromPlayerToMouseWorldPos.
/// </summary>
public class PlayerBodyPartsHandler : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(2, 10)]
    public string notes = "This class controls the behaviour of the player's body based on MouseInput.VectorPlayerToMouseWPos " +
        "and CrosshairJoystick.VectorPlayerToCrosshairWPos";

    [Header("Body parts")]
    public SpriteRenderer[] handRenderers;
    public SpriteRenderer[] headRenderers;
    [Space(5)]

    // Used to invert the legs based on the moving direction
    public GameObject hips;

    // Used to invert the upper body based on the moving direction
    public GameObject upperBody;

    private LookAt2Dv2Updater lookAtUpdater;
    private LookAt2Dv2Handler lookAtHandler;
    private PlayerShoulderSecondary shoulderSecondary;

    // To grant access to other classes if the need to know the current direction 
    // of the body (e.g. Projectile class to set travel direction)
    public static bool isRightDirection;

    private bool isHandAndHeadOnFront;
    private bool isLookAtMouse;

    private enum Direction { Left, Right }

    private void Awake()
    {
        // always start with right direction
        isRightDirection = true;
        TryGetComponent(out lookAtUpdater);
        TryGetComponent(out lookAtHandler);
        shoulderSecondary = FindObjectOfType<PlayerShoulderSecondary>();
    }

    private void Update()
    {
        CheckIfMissingClasses();

        if (PauseController.isGamePaused)
            return;

        // Update all LookAt2Dv2 classes
        lookAtUpdater.UpdateLookAtClasses();

        // Update secondary shoulder rotation
        shoulderSecondary.UpdateRotation();

        UpdateLookAtTarget();
        UpdateRenderersLayerOrder();
        UpdateBodyPartsDirection();
    }

    private void UpdateLookAtTarget()
    {
        if (TadaInput.IsMouseActive && !isLookAtMouse)
        {
            isLookAtMouse = true;
            lookAtHandler.SwitchToTarget(LookAt2Dv2.LookAtTarget.MouseWorldPosition);
        }
        if (!TadaInput.IsMouseActive && isLookAtMouse)
        {
            isLookAtMouse = false;
            lookAtHandler.SwitchToTarget(LookAt2Dv2.LookAtTarget.TargetTransform);
        }           
    }

    private void UpdateRenderersLayerOrder()
    {
        // I'm using hardcoded values to set the sortingOrder of the spriteRenderers. I ended up with this values after assembling the player's body 
        // and realizing how the parts should overlap one another. Maybe in the future it could be a good idea to make this values private and serialized
        // to expose them if a quick change in the Inspector is needed.

        if (((CrosshairMouse.AimDirection.y < 0f && TadaInput.IsMouseActive) || 
            (CrosshairJoystick.AimDirection.y < -0.1f && !TadaInput.IsMouseActive)) && isHandAndHeadOnFront) // Hand and Head behind
        {
            SetRenderersLayerOrder(handRenderers, 9);
            SetRenderersLayerOrder(headRenderers, 13);
            isHandAndHeadOnFront = false;
        }
        else if (((CrosshairMouse.AimDirection.y > 0f && TadaInput.IsMouseActive) ||
            (CrosshairJoystick.AimDirection.y > 0f && !TadaInput.IsMouseActive)) && !isHandAndHeadOnFront) // Hand and Head on front
        {
            SetRenderersLayerOrder(handRenderers, 12);
            SetRenderersLayerOrder(headRenderers, 5);
            isHandAndHeadOnFront = true;
        }
    }

    private void SetRenderersLayerOrder(SpriteRenderer[] spriteRenderers, int layerOrder)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] != null)
            {
                spriteRenderers[i].sortingOrder = layerOrder;
            }
        }
    }

    private void UpdateBodyPartsDirection()
    {
        // Checks if the mouse is active or a joystick is being used and uses either the vector playerPos -> mouseWorldPos or
        // the AimAxis (Joystick right stick) X value to determine if the body should be poiting to the left or to the right.

        if ((CrosshairMouse.AimDirection.x < -0.1f && TadaInput.IsMouseActive) ||
            (CrosshairJoystick.AimDirection.x < -0.1f && !TadaInput.IsMouseActive))
        {
            SetBodyPartsDirection(Direction.Left);
        }

        if ((CrosshairMouse.AimDirection.x > 0.1f && TadaInput.IsMouseActive) ||
            (CrosshairJoystick.AimDirection.x > 0.1f && !TadaInput.IsMouseActive))
        {
            SetBodyPartsDirection(Direction.Right);
        }
    }

    /// <summary>
    /// Method to visually flip the direction of the body.
    /// </summary>
    private void SetBodyPartsDirection(Direction direction)
    {
        switch (direction)
        {
            case Direction.Left:
                // bool to do this action once
                if (isRightDirection)
                {
                    lookAtHandler.FlipAxis(true);

                    // Invert the hips
                    if (hips != null)
                        hips.transform.localScale -= Vector3.right * 2;

                    // Invert the upperBody
                    if (upperBody != null)
                        upperBody.transform.localScale -= Vector3.right * 2;

                    // Stop this action
                    isRightDirection = false;
                }
                break;

            case Direction.Right:
                // bool to do this action once
                if (!isRightDirection)
                {
                    lookAtHandler.FlipAxis(false);

                    // Flip the hips
                    if (hips != null)
                        hips.transform.localScale += Vector3.right * 2;

                    // Flip the upperBody
                    if (upperBody != null)
                        upperBody.transform.localScale += Vector3.right * 2;

                    // Stop this action
                    isRightDirection = true;
                }
                break;

            default:
                break;
        }
    }

    private void CheckIfMissingClasses()
    {
        if (lookAtUpdater == null || lookAtHandler == null || shoulderSecondary == null)
        {
            Debug.LogWarning(gameObject.name + ": Missing behaviour classes!");
            return;
        }
    }
}
