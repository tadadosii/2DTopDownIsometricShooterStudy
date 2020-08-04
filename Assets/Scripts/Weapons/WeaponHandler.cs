using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// This class is used as a bridge between the player and the weapons, everything that can be done with the 
/// base class Weapon should be handled by this class.
/// </summary>
public class WeaponHandler : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(2, 10)]
    public string notes = "This class is used as a bridge between the player and the weapons, everything that can be done with the " +
        "base class Weapon should be handled by this class.";

    /// <summary>
    /// Option for which action should be used when calling UseWeapon.
    /// </summary>
    public enum ActionType { Primary, Secondary }

    /// <summary>
    /// Option for how to switch use rates using SwitchUseRate.
    /// </summary>
    public enum WeaponSwitchMode { Next, Previous, ByIndex }

    public Weapon[] weapons;

    private Weapon currentWeapon;
    private int currentWeaponIndex;
    private bool isUsingPrimaryAction;
    private bool isUsingSecondaryAction;

    // -TODO: Add an animation to the switch action (to have a little delay before being able to shoot again).

    private void Start()
    {
        SwitchWeapon(WeaponSwitchMode.ByIndex);
        SwitchUseRate(Weapon.SwitchUseRateType.ByIndex);
    }

    private void DisableAllWeapons()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] != null)
                weapons[i].gameObject.SetActive(false);
        }
    }

    /// <summary>
    /// To switch to a different weapon among the stored ones in <see cref="weapons"></see>.
    /// </summary>
    /// <param name="index">Used when the type is set to ByIndex.</param>
    public void SwitchWeapon(WeaponSwitchMode mode, int index = 0)
    {
        CheckWeaponsAvailability();
        DisableAllWeapons();
        switch (mode)
        {
            case WeaponSwitchMode.Next:
                currentWeaponIndex = ArraysHandler.GetNextIndex(currentWeaponIndex, weapons.Length);
                break;

            case WeaponSwitchMode.Previous:
                currentWeaponIndex = ArraysHandler.GetPreviousIndex(currentWeaponIndex, weapons.Length);
                break;

            case WeaponSwitchMode.ByIndex:
                if (index >= 0 && index <= weapons.Length - 1)
                    currentWeaponIndex = index;
                break;

            default:
                break;
        }
        currentWeapon = weapons[currentWeaponIndex];
        currentWeapon.gameObject.SetActive(true);
    }

    /// <summary>
    /// By default it switchs all weapons use rates to a different value (stored by each weapon). 
    /// It can also be used to only switch the current weapon value.
    /// </summary>
    /// <param name="index">Used when <see cref="Weapon.SwitchUseRateType"/> is set to ByIndex.</param>
    /// <param name="applyToAllWeapons">True by default</param>
    public void SwitchUseRate(Weapon.SwitchUseRateType type, int index = 0, bool applyToAllWeapons = true)
    {
        CheckWeaponsAvailability();
        if (applyToAllWeapons)
        {
            for (int i = 0; i < weapons.Length; i++)
            {
                if (weapons[i] != null)
                    weapons[i].SwitchUseRate(type, index);
            }
            return;
        }

        if (currentWeapon != null)
            currentWeapon.SwitchUseRate(type, index);
    }

    /// <summary>
    /// This is what makes action happen, it calls the current weapon actions based on <see cref="ActionType"/> type. 
    /// It got two actions at the moment (Primary, Secondary).
    /// </summary>
    /// <param name="type"></param>
    /// <param name="value">It could either be dinamically added by the player when calling this method 
    /// or it could be left with the default value (true). This is useful to create actions that relies 
    /// on this bool to determine if they should be canceled mid process (e.g. charged shot).</param>
    public void UseWeapon(ActionType type, bool value = true)
    {
        CheckWeaponsAvailability();

        switch (type)
        {
            case ActionType.Primary:
                if (currentWeapon != null && !isUsingSecondaryAction)
                {
                    isUsingPrimaryAction = value;
                    currentWeapon.PrimaryAction(value);
                }                    
                break;

            case ActionType.Secondary:
                if (currentWeapon != null && !isUsingPrimaryAction)
                {
                    isUsingSecondaryAction = value;
                    currentWeapon.SecondaryAction(value);
                }
                break;

            default:
                break;
        }
    }

    /// <summary>
    /// To break the execution of the code and log a message in the console if an element 
    /// from <see cref="weapons"/> has a missing weapon.
    /// </summary>
    private void CheckWeaponsAvailability()
    {
        for (int i = 0; i < weapons.Length; i++)
        {
            if (weapons[i] == null)
            {
                Debug.LogWarning(gameObject.name + " WeaponHandler: weapons missing, check array!");
                return;
            }
        }
    }
}
