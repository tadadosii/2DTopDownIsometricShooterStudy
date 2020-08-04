using UnityEngine;

/// <summary>
/// To call the method UpdateLookAt() from N amount of LookAt2Dv2 classes at the same time.
/// </summary>
public class LookAt2Dv2Updater : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(2, 10)]
    public string notes = "To call the method UpdateLookAt() from N amount of LookAt2Dv2 classes at the same time.";

    public LookAt2Dv2[] lookAtsToUpdate;

    public void UpdateLookAtClasses()
    {
        for (int i = 0; i < lookAtsToUpdate.Length; i++)
        {
            if (lookAtsToUpdate[i] == null)
            {
                Debug.Log(gameObject.name + ": LookAts to Update missing! Check array!");
                return;
            }
        }

        for (int i = 0; i < lookAtsToUpdate.Length; i++)
        {
            lookAtsToUpdate[i].UpdateLookAt();
        }
    }
}
