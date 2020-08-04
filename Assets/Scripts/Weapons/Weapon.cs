using UnityEngine;

/// <summary>
/// Base class that has public methods for actions and a SwitchUseRate method to switch between a given 
/// float called UseRate which is used to control how often the actions can be called.
/// </summary>

public class Weapon : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    #region ---------------------------- PROPERTIES

    /// <summary>
    /// Option for how to switch use rates using SwitchUseRate.
    /// </summary>
    public enum SwitchUseRateType { Next, Previous, ByIndex }

    [SerializeField] private bool debug = false;

    public float UseRate { get { return _UseRate; } private set { _UseRate = value; } }
    [SerializeField] protected float _UseRate;

    protected bool canUse;
    protected float[] useRateValues;
    protected int useRateIndex;
    protected float t;

    #endregion

    #region ---------------------------- UNITY CALLBACKS
    protected virtual void Awake()
    {
        canUse = true;
    }

    protected virtual void Update()
    {
        if (!canUse)
        {
            t += Time.deltaTime;
            if (t >= _UseRate)
            {
                canUse = true;
                OnCanUse();
                t = 0.0f;
            }
        }
    }
    #endregion

    #region ---------------------------- METHODS

    public virtual void SwitchUseRate(SwitchUseRateType type, int index = 0)
    {
        if (useRateValues.Length == 0)
        {
            Debug.LogWarning(gameObject.name + ": Need to add at least one UseRate value!");
            return;
        }

        switch (type)
        {
            case SwitchUseRateType.Next:
                useRateIndex = ArraysHandler.GetNextIndex(useRateIndex, useRateValues.Length);
                break;

            case SwitchUseRateType.Previous:
                useRateIndex = ArraysHandler.GetPreviousIndex(useRateIndex, useRateValues.Length);
                break;

            case SwitchUseRateType.ByIndex:
                if (index >= 0 && index <= useRateValues.Length - 1)
                    useRateIndex = index;
                break;

            default:
                break;
        }

        _UseRate = useRateValues[useRateIndex];

        if (debug)
            Debug.Log(gameObject.name + " Weapon UseRate # " + useRateIndex + ": " + _UseRate);
    }

    protected virtual void OnCanUse()
    {
        if (debug)
            Debug.Log(gameObject.name + "Weapon: OnCanUse");
    }

    public virtual void PrimaryAction(bool value)
    {
        if (debug)
            Debug.Log(gameObject.name + "Weapon: PrimaryAction | " + value);
    }

    public virtual void SecondaryAction(bool value)
    {
        if (debug)
            Debug.Log(gameObject.name + "Weapon: SecondaryAction | " + value);
    }

    #endregion
}
