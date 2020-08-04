using UnityEngine;

/// <summary>
/// This class has a primary action to shoot projectiles based on a UseRate value
/// and a secondary action with a timer that after reaching its duration will shoot a
/// secondary projectile.
/// </summary>

public class Weapon_ShootProjectileCanCharge : Weapon
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(5, 10)]
    public string notes = "This is a derived class from the base class Weapon. It has a primary action to shoot projectiles " +
        "based on a UseRate value and a secondary action with a timer that after reaching its duration will shoot a secondary projectile.";

    public GameObject basicProjectilePrefab;
    public GameObject chargedProjectilePrefab;
    public Transform projectileSpawnPoint;
    public GameObject chargingPFX;
    public SoundHandlerLocal chargingSFX;

    private WeaponAnim_ShootProjectileCanCharge anim;
    private Projectile primaryProjectile;
    private Projectile secondaryProjectile;
    private bool isReceivingInput = false;
    private bool isCharging = false;
    private float chargingTime;

    private const float CHARGE_DURATION = 2f;

    protected override void Awake()
    {
        base.Awake();
        useRateValues = new float[] { 0.125f, 0.05f };
        TryGetComponent(out anim);
    }

    protected override void Update()
    {
        base.Update();
        if (isReceivingInput)
        {
            // Execute the initial actions that take place in the first frame after isReceivingInput is set to true.
            OnChargingStart();

            // Increase the value of charging time by adding Time.deltaTime on each frame.
            chargingTime += Time.deltaTime;

            // Update the OnCharging actions and pass chargingTime as argument.
            OnCharging(chargingTime);

            // If charging time is equal or greater than the constant charge duration, execute the last actions.
            if (chargingTime >= CHARGE_DURATION)
                OnChargingEnd();
        }

        // If the player releases the secondary action button making isReceivingInput false and the weapon is
        // currently charging, execute the cancel actions.
        if (!isReceivingInput && isCharging)
            OnChargeCancel();
    }

    private void OnEnable()
    {
        SpawnProjectiles();
    }

    protected override void OnCanUse()
    {
        base.OnCanUse();
        SpawnProjectiles();
        if (anim != null)
            anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.Idle);
    }

    private void SpawnProjectiles()
    {
        // Stop from executing if this variables are not set.
        if (basicProjectilePrefab == null || chargedProjectilePrefab == null || projectileSpawnPoint == null)
        {
            Debug.LogError(gameObject.name + " missing prefabs or spawnPoint!");
            return;
        }

        // If there is no projectile, instantiate one and store its Projectile component.
        if (primaryProjectile == null)
        {
            // Instantiate.
            primaryProjectile = Instantiate(basicProjectilePrefab, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation, projectileSpawnPoint).GetComponent<Projectile>();

            // Disable to hide it while it's behind the weapon.
            primaryProjectile.SetActive(false);
        }

        // If there is no projectile, instantiate one and store its Projectile component.
        if (secondaryProjectile == null)
        {
            // Instantiate.
            secondaryProjectile = Instantiate(chargedProjectilePrefab, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation, projectileSpawnPoint).GetComponent<Projectile>();

            // Disable to hide it while it's behind the weapon.
            secondaryProjectile.SetActive(false);
        }
    }

    public override void PrimaryAction(bool value)
    {
        base.PrimaryAction(value);

        // Can be executed only if there is a projectile available and canUse is true.
        if (primaryProjectile != null && canUse)
        {
            // Play the basic animation if WeaponAnim_ShootProjectileCanCharge is available.
            if (anim != null)
                anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.BasicShot);

            // Make the camera Shake.
            CameraShake.Shake(duration: 0.075f, shakeAmount: 0.1f, decreaseFactor: 3f);

            // Enable the projectile.
            primaryProjectile.SetActive(true);

            // Call the method Fire on the projectile to launch it towards the crosshair direction.
            primaryProjectile.Fire();

            // We make it null to give room to a new instantiated projectile.
            primaryProjectile = null;

            // We make it false to execute the base Update actions which makes it true again after UseRate duration is reached,
            // which then calls the method OnCanUse() that's used to spawn new projectiles and to return to the Idle anim.
            canUse = false;
        }
    }

    public override void SecondaryAction(bool value)
    {
        base.SecondaryAction(value);

        // The purpose of this action is to let the player hold the secondary action button to make the bool
        // isReceivingInput true, which in turn enables a timer and a series of actions to ultimately launch the
        // secondary projectile.

        // After firing the projectile, canUse is set to false and because the player can continuously call this method,
        // we use this to stop isReceivingInput from getting a true value.
        if (!canUse)
        {
            // Cancel inputs after use.
            isReceivingInput = false;
            return;
        }

        // We stop the code here if one of the needed variables is missing.
        if ((secondaryProjectile == null || chargingPFX == null || chargingSFX == null))
        {
            Debug.LogWarning(gameObject.name + ": missing prefabs!");
            return;
        }

        // We make it true if the player is pressing the secondary action button or false if not.
        // When it's true, it activates the actions on the Update method of this class.
        isReceivingInput = value;
    }

    /// <summary>
    /// Initial actions that take place in the first frame after isReceivingInput is set to true.
    /// </summary>
    private void OnChargingStart()
    {
        // This actions can only be executed if isCharging is false.
        if (!isCharging)
        {
            // Play the charging animation if WeaponAnim_ShootProjectileCanCharge is available.
            if (anim != null)
                anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.Charging);

            // We set it to true to avoid calling this method more than once.
            isCharging = true;

            // NOTE: Weapon Charging Shake. Right now this shake behaviour will override the OnChargingEnd shake.
            // In order to fix this I need to add timer to stop the charging option from being rapidly reused.
            CameraShake.Shake(duration: CHARGE_DURATION, shakeAmount: 0.065f, decreaseFactor: 1f);

            // Enable the charging visual effects.
            chargingPFX.SetActive(true);

            // Play the first sound of SoundHandlerLocal.
            chargingSFX.PlaySound();
        }
    }

    /// <summary>
    /// Actions that take place while isReceivingInput is true and time is being added to chargingTime.
    /// </summary>
    private void OnCharging(float t)
    {
        // Increase the size of the charging fx to enhance it with a feeling a anticipation.
        chargingPFX.transform.localScale = Vector2.one * t;
    }

    /// <summary>
    /// Last actions that take place when chargingTime value is equal or greater than CHARGE_DURATION.
    /// </summary>
    private void OnChargingEnd()
    {
        // Play the charged shot animation if WeaponAnim_ShootProjectileCanCharge is available.
        if (anim != null)
            anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.ChargedShot);

        // Set it to false to allow OnChargingStart to be called again.
        isCharging = false;

        // Set it to false to stop the player from making isReceivingInput true if it's holding the secondary action button.
        canUse = false;

        // Reset the timer to allow correctly restarting the charging action.
        chargingTime = 0.0f;

        // Make the camera Shake by a greater value.
        CameraShake.Shake(duration: 0.2f, shakeAmount: 1f, decreaseFactor: 3f);

        // Enable the projectile.
        secondaryProjectile.SetActive(true);

        // Call the method Fire on the projectile to launch it towards the crosshair direction.
        secondaryProjectile.Fire();

        // Make it null to give room to a new instantiated projectile.
        secondaryProjectile = null;

        // Reset the scale of the charging visual fx.
        chargingPFX.transform.localScale = Vector2.one;

        // Disable the charge visual fx.
        chargingPFX.SetActive(false);
    }

    /// <summary>
    /// Actions that take place if the secondary action is cancelled (when the player released the button before calling
    /// OnChargingEnd).
    /// </summary>
    private void OnChargeCancel()
    {
        // Return to Idle animation if WeaponAnim_ShootProjectileCanCharge is available.
        if (anim != null)
            anim.PlayAnimation(WeaponAnim_ShootProjectileCanCharge.Animation.Idle);

        // Is set to false to allow OnChargingStart to be called again.
        isCharging = false;

        // Is set to zero to reset the timer so that it can correctly count the time again.
        chargingTime = 0.0f;

        // Stop the camera from shaking.
        CameraShake.Shake(0f, 0f, 0f);

        // Stop the charging SoundHandlerLocal sounds.
        chargingSFX.StopSound();

        // Reset the scale of the charging visual fx.
        chargingPFX.transform.localScale = Vector2.one;

        // Disable the charging visual fx.
        chargingPFX.SetActive(false);
    }
}
