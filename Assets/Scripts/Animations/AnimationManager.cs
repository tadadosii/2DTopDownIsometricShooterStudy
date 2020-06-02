using UnityEngine;

/// <summary>
/// Basic animation methods to play and set animations speed
/// </summary>
public class AnimationManager : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public bool getAnimInChild;
    protected Animation anim;

    protected virtual void Awake()
    {
        if (!getAnimInChild)
            anim = GetComponent<Animation>();
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
