using UnityEngine;

/// <summary>
/// To handle N amount of LookAt2Dv2 classes at the same time.
/// </summary>
public class LookAt2Dv2Handler : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(1, 10)]
    public string notes = "To handle N amount of LookAt2Dv2 classes at the same time.";

    public LookAt2Dv2[] lookAts;

    private void Awake()
    {
        CheckArray();
    }

    public void FlipAxis(bool value)
    {
        for (int i = 0; i < lookAts.Length; i++)
        {
            if (lookAts[i] != null)
                lookAts[i].isFlipAxis = value;
        }
    }

    public void SwitchToTarget(LookAt2Dv2.LookAtTarget target)
    {
        for (int i = 0; i < lookAts.Length; i++)
        {
            if (lookAts[i] != null)
                lookAts[i].SwitchToTarget(target);
        }
    }

    private void CheckArray ()
    {
        for (int i = 0; i < lookAts.Length; i++)
        {
            if (lookAts[i] == null)
            {
                Debug.Log(gameObject.name + ": LookAts to Update missing! Check array!");
                return;
            }
        }
    }
}
