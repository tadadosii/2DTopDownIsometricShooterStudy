using UnityEngine;

public class Crosshair : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [Tooltip("The gameobject that will be used to visually represent the crosshair.")]
    public GameObject crosshair;

    protected virtual void Awake()
    {
        if (crosshair == null)
        {
            Debug.LogError(gameObject.name + ": Missing crosshair!");
            Debug.Break();
        }
    }

    public virtual void UpdateCrosshair() { }

    /// <summary>
    /// To get or set the active state of the gameobject used to visually represent the crosshair.
    /// </summary>
    public bool IsActive
    {
        get { return _IsActive; }
        set { _IsActive = value; crosshair.SetActive(value); }
    }
    private bool _IsActive;
}
