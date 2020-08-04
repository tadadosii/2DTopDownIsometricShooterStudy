using UnityEngine;

/// <summary>
/// This class has methods for controlling the player's animations. The PlayerController class is the 
/// one that calls those methods.
/// </summary>
public class PlayerAnimations : AnimationHandler
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(2, 10)]
    public string notes = "This class has methods for controlling the player's animations. The PlayerController class is the " +
        "one that calls those methods.";

    public enum AnimName { Idle, WalkForward, WalkBackwards }

    public void SetAnimationSpeed(AnimName name, float value)
    {
        switch (name)
        {
            case AnimName.Idle:
                SetAnimationSpeed("Idle", value);
                break;

            case AnimName.WalkForward:
                // value / 2.5f because player moveSpeed / 2.5f is what feels better for this animation
                SetAnimationSpeed("WalkForward", value / 2.5f);
                break;

            case AnimName.WalkBackwards:
                // value / 2.5f because player moveSpeed / 2.5f is what feels better for this animation
                SetAnimationSpeed("WalkBackwards", value / 2.5f);
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// Method to determine if the animation should be walking forward, walking backwards or Idle
    /// </summary>
    /// <param name="moveInput">Any Horizontal + Vertical inputs as a <see cref="Vector3"/></param>
    public void PlayMoveAnimationsByMoveInputAndLookDirection(Vector3 moveInput)
    {
        // NOTE: Look for a way to make PlayMoveAnimationsByMoveInputAndLookDirection conditions much more easier to read.

        // conditions to determine if the animation should be walking forward, walking backwards or Idle
        if ((moveInput.x > 0 || moveInput.y > 0) && ((CrosshairMouse.AimDirection.x > 0 && TadaInput.IsMouseActive) || 
            (CrosshairJoystick.AimDirection.x > 0 && !TadaInput.IsMouseActive)))
            PlayAnimation("WalkForward");
        else if ((moveInput.x > 0 || moveInput.y > 0) && ((CrosshairMouse.AimDirection.x < 0 && TadaInput.IsMouseActive) ||
            (CrosshairJoystick.AimDirection.x < 0 && !TadaInput.IsMouseActive)))
            PlayAnimation("WalkBackwards");
        else if ((moveInput.x < 0 || moveInput.y < 0) && ((CrosshairMouse.AimDirection.x < 0 && TadaInput.IsMouseActive) ||
            (CrosshairJoystick.AimDirection.x < 0 && !TadaInput.IsMouseActive)))
            PlayAnimation("WalkForward");
        else if ((moveInput.x < 0 || moveInput.y < 0) && ((CrosshairMouse.AimDirection.x > 0 && TadaInput.IsMouseActive) ||
            (CrosshairJoystick.AimDirection.x > 0 && !TadaInput.IsMouseActive)))
            PlayAnimation("WalkBackwards");
        else
            PlayAnimation("Idle");
    }
}
