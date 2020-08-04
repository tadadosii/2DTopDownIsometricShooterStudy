using UnityEngine;

/// <summary>
/// This class has a unique action to do a continuous shooting (e.g. Continuous Laser Beam).
/// </summary>
public class Weapon_ChargeContinuousShooting : Weapon
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public GameObject basicProjectilePrefab;
    public GameObject chargedProjectilePrefab;
    public Transform projectileSpawnPoint;
    public GameObject chargingPFX;
    public SoundHandlerGlobal chargingSFX;

    private Projectile primaryProjectile;
    private Projectile secondaryProjectile;
    private bool isReceivingInput = false;
    private bool isChargingStarted = false;
    private bool isChargeShooting = false;
    private float chargingTime;

    private const float CHARGE_DELAY = 2f;

    // NOTE: Work in progress.

    protected override void Awake()
    {
        base.Awake();
        useRateValues = new float[] { 0.15f };
        SwitchUseRate(0);
    }

    protected override void Update()
    {
        base.Update();
        if (isReceivingInput)
        {
            if (!isChargeShooting)
            {
                OnChargingStart();

                chargingTime += Time.deltaTime;
                OnCharging(chargingTime);

                if (chargingTime >= CHARGE_DELAY)
                {
                    isChargeShooting = true;
                    OnChargingEnd();
                }
            }
            else
                OnChargeShooting();
        }

        if (!isReceivingInput && (isChargingStarted || isChargeShooting))
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
    }

    private void SpawnProjectiles()
    {
        if (basicProjectilePrefab == null || chargedProjectilePrefab == null || projectileSpawnPoint == null)
        {
            Debug.LogError(gameObject.name + " missing prefabs or spawnPoint!");
            return;
        }

        if (primaryProjectile == null)
        {
            primaryProjectile = Instantiate(basicProjectilePrefab, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation, projectileSpawnPoint).GetComponent<Projectile>();
            primaryProjectile.SetActive(false);
        }

        if (secondaryProjectile == null)
        {
            secondaryProjectile = Instantiate(chargedProjectilePrefab, projectileSpawnPoint.position,
            projectileSpawnPoint.rotation, projectileSpawnPoint).GetComponent<Projectile>();
            secondaryProjectile.SetActive(false);
        }
    }

    public override void PrimaryAction(bool value)
    {
        base.PrimaryAction(value);

        if (primaryProjectile != null && canUse)
        {
            CameraShake.Shake(duration: 0.075f, shakeAmount: 0.1f, decreaseFactor: 3f);
            primaryProjectile.SetActive(true);
            primaryProjectile.Fire();
            primaryProjectile = null;
            canUse = false;
        }
    }

    public override void SecondaryAction(bool value)
    {
        base.SecondaryAction(value);
        if (secondaryProjectile == null || chargingPFX == null || chargingSFX == null)
        {
            Debug.LogWarning(gameObject.name + ": missing prefabs!");
            return;
        }
        isReceivingInput = value;
    }

    private void OnChargingStart()
    {
        if (!isChargingStarted)
        {
            isChargingStarted = true;

            // NOTE: Weapon Charging Shake. Right now this shake behaviour will override the OnChargingEnd shake.
            // In order to fix this I need to add timer to stop the charging option from being rapidly reused.
            CameraShake.Shake(duration: CHARGE_DELAY, shakeAmount: 0.065f, decreaseFactor: 1f);

            chargingPFX.SetActive(true);
            chargingSFX.PlaySound();
        }
    }

    private void OnCharging(float t)
    {
        chargingPFX.transform.localScale = Vector2.one * t;
    }

    private void OnChargingEnd()
    {
        isReceivingInput = false;
        isChargingStarted = false;
        chargingTime = 0.0f;

        CameraShake.Shake(duration: 0.2f, shakeAmount: 1f, decreaseFactor: 3f);
        secondaryProjectile.SetActive(true);
        secondaryProjectile.Fire();
        secondaryProjectile = null;
        chargingPFX.transform.localScale = Vector2.one;
        chargingPFX.SetActive(false);
    }

    private void OnChargeShooting()
    {
        
    }

    private void OnChargeCancel()
    {
        isChargeShooting = false;
        isReceivingInput = false;
        isChargingStarted = false;
        chargingTime = 0.0f;
        CameraShake.Shake(0f, 0f, 0f);
        chargingSFX.StopSound();
        chargingPFX.transform.localScale = Vector2.one;
        chargingPFX.SetActive(false);
    }
}
