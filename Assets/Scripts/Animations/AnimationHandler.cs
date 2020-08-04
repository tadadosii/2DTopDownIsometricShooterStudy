using UnityEngine;

/// <summary>
/// Basic animation methods to play and set animations speed. This is a base class that 
/// any other class can inherit from, thus gaining access to its properties and methods (e.g. PlayerController).
/// </summary>
public class AnimationHandler : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [SerializeField] private bool getAnimInChild = false;
    protected Animation anim;

    protected virtual void Awake()
    {
        if (!getAnimInChild)
            TryGetComponent(out anim);
        else
            anim = GetComponentInChildren<Animation>();
    }

    protected virtual void PlayAnimation(string name)
    {
        if (anim.GetClip(name) == null)
            return;
        anim.Play(name);
    }

    protected virtual void SetAnimationSpeed(string name, float speed)
    {
        if (anim.GetClip(name) == null)
            return;
        anim[name].speed = speed;
    }
}
