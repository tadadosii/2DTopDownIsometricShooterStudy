using System.Collections;
using UnityEngine;

public class PlayerSkills : MonoBehaviour
{
    public TrailRenderer[] dashTrails;
    [SerializeField] private float dashForce = 5f;
    public SoundHandlerGlobal dashSFXHandler;

    private PlayerPhysics _PlayerPhysics;
    private PlayerMaterials _PlayerMaterials;
    private AfterImageHandler _AfterImageHandler;

    private bool canDash;

    private const float DASH_DURATION = 0.2f;

    private void Awake()
    {
        _AfterImageHandler = FindObjectOfType<AfterImageHandler>();
        TryGetComponent(out _PlayerPhysics);
        TryGetComponent(out _PlayerMaterials);

        // We can dash from the start, this should be handled by other behaviour that grants the player
        // the ability to dash after completing a task. 
        canDash = true;

        // Starts with disabled trails.
        SetActiveTrails(false);
    }

    public void Dash()
    {
        if (canDash)
            StartCoroutine(CO_Dash());
    }

    // Used a coroutine to have a WaitForSeconds method to set canDash to true after a given time.
    private IEnumerator CO_Dash()
    {
        canDash = false;

        // Enable trails.
        SetActiveTrails(true);

        _AfterImageHandler.SetActiveAfterImages();

        // SetVelocity is being used, if we don't stop it for the duration of the dash,
        // AddForce won't have any effect because the velocity will always be set to whatever
        // the TadaInput.MoveAxisRawInput * _MoveSpeed calculation value is.
        _PlayerPhysics.CanMove = false;

        ActualDash();
        yield return new WaitForSeconds(DASH_DURATION);

        // Disable trails.
        SetActiveTrails(false);

        // We can set the velocity again to be handler by the player's movement.
        _PlayerPhysics.CanMove = true;

        // We can dash again. This could be after another WaitForSeconds to add a little delay after a dash.
        canDash = true;
    }

    public void ActualDash()
    {
        // Play SFX
        if (dashSFXHandler != null)
            dashSFXHandler.PlaySound();

        // Activate body highlight effect
        _PlayerMaterials.SetActiveHighlightBody(DASH_DURATION, intensity: 1.25f);

        // Zero out rigidbody velocity to have a consistent dash
        _PlayerPhysics.SetVelocity(Vector2.zero);

        // AddForce towards move direction
        _PlayerPhysics.AddForce(TadaInput.MoveAxisRawInput.normalized, dashForce, ForceMode2D.Impulse);
    }

    private void SetActiveTrails(bool value)
    {
        for (int i = 0; i < dashTrails.Length; i++)
        {
            if (dashTrails != null)
            {
                dashTrails[i].emitting = value;
            }
        }
    }
}
