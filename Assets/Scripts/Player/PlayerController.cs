using UnityEngine;
/// <summary>
/// Movement, controlling weapons and body parts. This class inherits from AnimationManager
/// to easily control the animation behaviour with its methods.
/// </summary>
public class PlayerController : AnimationManager
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    #region ---------------------------- PROPERTIES
    [Header("Movement")]
    public float moveSpeed;

    [Header("Body parts")]
    // used to invert the legs based on the moving direction
    public GameObject hips;

    // arms that will be enable when looking to the left
    public GameObject armsLeftDirection;

    // arms that will be enable when looking to the right
    public GameObject armsRightDirection;

    // this properties are here to be updated from this script
    public Shoulder leftDirectionShoulder;
    public Shoulder rightDirectionShoulder;
    public LookAt2Dv1[] leftDirectionLookAt;
    public LookAt2Dv1[] rightDirectionLookAt; 

    [Header("Weapon")]
    public Weapon[] weapon;
    public ParticleSystem[] chargeParticles;
    public SoundFXHandler chargeSFX;

    private bool paused;
    private bool isRightDirection;
    private bool isCharging;
    private bool isCharged;
    private int currentProjectile;
    private float chargeTime;

    public const float chargeDelay = 2f;

    #endregion

    #region ---------------------------- UNITY CALLBACKS
    protected override void Awake()
    {
        base.Awake();

        // always start with right direction
        isRightDirection = true;
        ArmsSetActive(1);

        // this can be used to dynamically change the animation speed on Update
        SetAnimationSpeed("WalkForward", moveSpeed / 2.5f);
        SetAnimationSpeed("WalkBackwards", moveSpeed / 2.5f);
    }

    private void Update()
    {
        // NOTE: to add support to a joystick right stick, store the vector direction of
        // that stick and add that to all the logic that's being handled by MouseInput

        // Added here for testing and quickly pausing to create showcasing stuff
        if (Input.GetKeyDown(KeyCode.Space))
        {
            paused = !paused;
            if (paused)
                Time.timeScale = 0.0001f;
            else
                Time.timeScale = 1f;
        }

        if (paused)
            return;

        // simple movement with Translate
        Vector3 moveInput = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        transform.Translate(moveInput * Time.deltaTime * moveSpeed);

        // conditions to determine if the animation should be walking forward, walking backwards or Idle
        if ((moveInput.x > 0 || moveInput.y > 0) && MouseInput.directionFromPlayerToMouseWorldPos.x > 0)
            PlayAnimation("WalkForward");
        else if ((moveInput.x > 0 || moveInput.y > 0) && MouseInput.directionFromPlayerToMouseWorldPos.x < 0)
            PlayAnimation("WalkBackwards");
        else if ((moveInput.x < 0 || moveInput.y < 0) && MouseInput.directionFromPlayerToMouseWorldPos.x < 0)
            PlayAnimation("WalkForward");
        else if ((moveInput.x < 0 || moveInput.y < 0) && MouseInput.directionFromPlayerToMouseWorldPos.x > 0)
            PlayAnimation("WalkBackwards");
        else
            PlayAnimation("Idle");

        // conditions to switch between arms and update their behaviours
        if (MouseInput.directionFromPlayerToMouseWorldPos.x < -0.1f)
        {
            // Update all LookAt2Dv1 on leftDirectionLookAt array
            // needs to be done from this script to avoid snapping movements when
            // the arm got disabled with a rotation that's opposite to the new rotation when enabled again  
            UpdateLookAtArray(-1);
            if (leftDirectionShoulder != null)
                leftDirectionShoulder.UpdateRotation();

            // bool to do this action once
            if (isRightDirection)
            {
                // invert the hips
                if (hips != null)
                    hips.transform.localScale -= Vector3.right * 2;

                // actual method to switch the arms
                ArmsSetActive(-1);

                // stop this action
                isRightDirection = false;
            }
        }
        else if (MouseInput.directionFromPlayerToMouseWorldPos.x > 0.1f)
        {
            // Update all LookAt2Dv1 on leftDirectionLookAt array
            // needs to be done from this script to avoid snapping movements when
            // the arm got disabled with a rotation that's opposite to the new rotation when enabled again  
            UpdateLookAtArray(1);
            if (rightDirectionShoulder != null)
                rightDirectionShoulder.UpdateRotation();

            if (!isRightDirection)
            {
                // invert the hips
                if (hips != null)
                    hips.transform.localScale += Vector3.right * 2;

                // actual method to switch the arms
                ArmsSetActive(1);

                // stop this action
                isRightDirection = true;
            }
        }

        // get mouse input while holding left click 
        if (Input.GetMouseButton(0) && !isCharging)
        {
            FireWeapon();
        }

        if (Input.GetKeyDown(KeyCode.Q))
        {
            currentProjectile++;
            if (currentProjectile > 1)
                currentProjectile = 0;
            SetWeaponProjectile(currentProjectile);
        }

        #region Charged Shot - This should go into the weapon class, but I decided to do it in here for a faster implementation

        if (Input.GetMouseButtonDown(1))
        {
            CameraShake.Shake(chargeDelay, 0.05f, 1f);
            isCharging = true;
            SetActiveChargeParticle(true);
            if (chargeSFX != null)
                chargeSFX.PlaySound();
        }

        if (Input.GetMouseButton(1) && isCharging && !isCharged)
        {
            chargeTime += Time.deltaTime;
            SetChargePFXScale(chargeTime);
            if (chargeTime >= chargeDelay)
            {
                isCharged = true;
                isCharging = false;
                FireWeapon(1);
                SetChargePFXScale(1f);
                SetActiveChargeParticle(false);
                chargeTime = 0.0f;
            }
        }
        if (Input.GetMouseButtonUp(1))
        {
            if (isCharged)
            {
                isCharged = false;
                isCharging = false;
            }
            else
            {
                CameraShake.Shake(0f, 0f, 0f);
                isCharging = false;
                chargeSFX.StopSound();
                SetChargePFXScale(1f);
                chargeTime = 0.0f;
            }
            SetActiveChargeParticle(false);
        }
        #endregion

        if (Input.GetKeyDown(KeyCode.Alpha1))
            SetWeaponProperties(0.125f);

        if (Input.GetKeyDown(KeyCode.Alpha2))
            SetWeaponProperties(0.05f);
    }

    #endregion

    #region ---------------------------- METHODS
    /// <param name="direction"> -1 left | 1 right </param>
    private void UpdateLookAtArray(int direction)
    {
        if (leftDirectionLookAt.Length == 0 || rightDirectionLookAt.Length == 0)
            return;

        // simple switch to choose between -1 for left direction and +1 for right direction
        switch (direction)
        {
            case -1:
                for (int i = 0; i < leftDirectionLookAt.Length; i++)
                {
                    if (leftDirectionLookAt[i] != null)
                        leftDirectionLookAt[i].UpdateLookAt();
                }
                break;

            case 1:
                for (int i = 0; i < rightDirectionLookAt.Length; i++)
                {
                    if (rightDirectionLookAt[i] != null)
                        rightDirectionLookAt[i].UpdateLookAt();
                }
                break;

            default:
                break;
        }
    }

    /// <param name="direction"> -1 left | 1 right </param>
    private void ArmsSetActive(int direction)
    {
        if (armsLeftDirection == null && armsRightDirection == null)
            return;

        // simple switch to choose between -1 for left direction and +1 for right direction
        switch (direction)
        {
            case -1:
                for (int i = 0; i < leftDirectionLookAt.Length; i++)
                {
                    armsLeftDirection.SetActive(true);
                    armsRightDirection.SetActive(false);
                }
                break;

            case 1:
                for (int i = 0; i < rightDirectionLookAt.Length; i++)
                {
                    armsLeftDirection.SetActive(false);
                    armsRightDirection.SetActive(true);
                }
                break;

            default:
                break;
        }
    }

    /// <param name="type">0 Basic | 1 Charged </param>
    private void FireWeapon(int type = 0)
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            // if there is a weapon and that weapon is enabled
            if (weapon[i] != null && weapon[i].gameObject.activeInHierarchy)
            {
                //Debug.Log("PlayerController: Calling Weapon FireProjectile()");
                if (type == 0)
                    weapon[i].FireBasic();
                else
                    weapon[i].FireCharged();
            }
        }
    }

    private void SetWeaponProperties(float value)
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i] != null)
            {
                weapon[i].fireRate = value;
            }
        }
    }

    private void SetWeaponProjectile(int projectileIndex)
    {
        for (int i = 0; i < weapon.Length; i++)
        {
            if (weapon[i] != null)
            {
                weapon[i].ProjectileIndex = projectileIndex;
            }
        }
    }

    private void SetChargePFXScale(float multiplier)
    {
        if (chargeParticles.Length == 0)
            return;

        for (int i = 0; i < chargeParticles.Length; i++)
        {
            if (chargeParticles[i] != null)
            {
                chargeParticles[i].transform.localScale = Vector2.one * multiplier;
            }
        }
    }

    /// <param name="state">0 Stop | 1 Play</param>
    private void SetActiveChargeParticle(bool value)
    {
        if (chargeParticles.Length == 0)
            return;

        for (int i = 0; i < chargeParticles.Length; i++)
        {
            if (chargeParticles[i] != null)
            {
                chargeParticles[i].gameObject.SetActive(value);
            }
        }
    }

    #endregion
}
