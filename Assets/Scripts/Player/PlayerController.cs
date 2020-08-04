using UnityEngine;
using UnityEngine.SceneManagement;
/// <summary>
/// This class receives inputs from TadaInput and calls methods from other classes based on those inputs. It 
/// also handles movement.
/// </summary>
public class PlayerController : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(2, 10)]
    public string notes = "It receives inputs from TadaInput and calls methods from other classes based on those inputs. It " +
        "also handles movement.";

    #region ---------------------------- PROPERTIES

    private PlayerPhysics _PlayerPhysics;
    private PlayerSkills _PlayerSkills;
    private PlayerAnimations _PlayerAnimations;
    private WeaponHandler _WeaponHandler;

    #endregion

    #region ---------------------------- UNITY CALLBACKS

    // Ignore never invoked message, it's 
    private void Awake()
    {
        Initialize();
    }

    private void Update()
    {
        CheckIfMissingClasses();

        if (PauseController.isGamePaused)
            return;

        #region ---------------------------- SKILLS

        if (TadaInput.GetKeyDown(TadaInput.ThisKey.Dash) && _PlayerPhysics.Velocity.sqrMagnitude > 0)
            _PlayerSkills.Dash();

        #endregion

        #region ---------------------------- ANIMATIONS

        // Animations: Method to determine if the animation should be walking forward, walking backwards or Idle
        _PlayerAnimations.PlayMoveAnimationsByMoveInputAndLookDirection(TadaInput.MoveAxisRawInput);

        // To dynamically change WalkForward and WalkBackwards animation speed
        _PlayerAnimations.SetAnimationSpeed(PlayerAnimations.AnimName.WalkForward, _PlayerPhysics.Velocity.magnitude);
        _PlayerAnimations.SetAnimationSpeed(PlayerAnimations.AnimName.WalkBackwards, _PlayerPhysics.Velocity.magnitude);

        #endregion

        #region ---------------------------- WEAPON ACTIONS

        // Weapon: Use Primary Action while holding Left Mouse Button
        if (TadaInput.GetKey(TadaInput.ThisKey.PrimaryAction))
            _WeaponHandler.UseWeapon(WeaponHandler.ActionType.Primary);

        // Weapon: Cancel Weapon Primary Action if Left Mouse Button is released
        if (TadaInput.GetKeyUp(TadaInput.ThisKey.PrimaryAction))
            _WeaponHandler.UseWeapon(WeaponHandler.ActionType.Primary, false);

        // Weapon: Use Secondary Action while holding Right Mouse Button
        if (TadaInput.GetKeyDown(TadaInput.ThisKey.SecondaryAction))
            _WeaponHandler.UseWeapon(WeaponHandler.ActionType.Secondary);

        // Weapon: Cancel Weapon Secondary Action if Right Mouse Button is released
        if (TadaInput.GetKeyUp(TadaInput.ThisKey.SecondaryAction))
            _WeaponHandler.UseWeapon(WeaponHandler.ActionType.Secondary, false);

        #endregion

        #region ---------------------------- WEAPON USE RATE

        // Weapon: Switch to Next Use Rate
        if (TadaInput.GetKeyDown(TadaInput.ThisKey.NextUseRate))
            _WeaponHandler.SwitchUseRate(Weapon.SwitchUseRateType.Next);

        // Weapon: Switch to Previous Use Rate
        if (TadaInput.GetKeyDown(TadaInput.ThisKey.PreviousUseRate))
            _WeaponHandler.SwitchUseRate(Weapon.SwitchUseRateType.Previous);

        #endregion

        #region ---------------------------- WEAPON SWITCH

        // Weapon: Switch weapon to index 0
        if (Input.GetKeyDown(KeyCode.Alpha1))
            _WeaponHandler.SwitchWeapon(WeaponHandler.WeaponSwitchMode.ByIndex, 0);

        // Weapon: Switch weapon to index 1
        if (Input.GetKeyDown(KeyCode.Alpha2))
            _WeaponHandler.SwitchWeapon(WeaponHandler.WeaponSwitchMode.ByIndex, 1);

        // Weapon: Switch to next weapon
        if (TadaInput.GetKeyDown(TadaInput.ThisKey.NextWeapon))
            _WeaponHandler.SwitchWeapon(WeaponHandler.WeaponSwitchMode.Next);

        // Weapon: Switch to previous weapon
        if (TadaInput.GetKeyDown(TadaInput.ThisKey.PreviousWeapon))
            _WeaponHandler.SwitchWeapon(WeaponHandler.WeaponSwitchMode.Previous);

        #endregion
    }

    #endregion

    #region ---------------------------- METHODS

    /// <summary>
    /// All the actions that should be done on <see cref="Awake"/>
    /// </summary>
    private void Initialize()
    {
        TryGetComponent(out _PlayerAnimations);
        TryGetComponent(out _WeaponHandler);
        TryGetComponent(out _PlayerPhysics);
        TryGetComponent(out _PlayerSkills);
    }

    /// <summary>
    /// To display a warning message in the console if there is something missing 
    /// that should be added in the Inspector.
    /// </summary>
    private void CheckIfMissingClasses()
    {
        if (_PlayerAnimations == null || _WeaponHandler == null || _PlayerPhysics == null || _PlayerSkills == null)
        {
            Debug.LogWarning(gameObject.name + ": Missing behaviour classes!");
            return;
        }
    }

    #endregion
}