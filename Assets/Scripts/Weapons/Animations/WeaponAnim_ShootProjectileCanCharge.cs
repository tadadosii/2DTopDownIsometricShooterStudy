using UnityEngine;
public class WeaponAnim_ShootProjectileCanCharge : AnimationHandler
{
    [TextArea(2, 10)]
    public string notes = "This class has methods to handle the animations of the derived class W_ShootProjectileCanCharge.";

    public enum Animation { Idle, BasicShot, Charging, ChargedShot }

    public void PlayAnimation(Animation name)
    {
        switch (name)
        {
            case Animation.Idle:
                PlayAnimation("Idle");
                break;

            case Animation.BasicShot:
                PlayAnimation("BasicShot");
                break;

            case Animation.Charging:
                PlayAnimation("Charging");
                break;

            case Animation.ChargedShot:
                PlayAnimation("ChargedShot");
                break;

            default:
                break;
        }
    }
}
