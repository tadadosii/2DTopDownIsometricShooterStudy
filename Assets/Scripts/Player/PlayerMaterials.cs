using UnityEngine;

/// <summary>
/// This class will be used to control all the behaviours related to the player's materials.
/// </summary>
public class PlayerMaterials : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(4, 10)]
    public string notes = "This class will be used to control all the behaviours related to the player's materials.";

    public Material bodyMaterial;

    private bool isHighlightBodyActive;
    private float highlightBodyIntensity;
    private float HighlightBodyTime;
    private float highlightBodyDuration;

    private void Update()
    {
        if (isHighlightBodyActive)
        {
            HighlightBodyTime -= Time.deltaTime;

            // Reduce body color intensity from highlightBodyIntensity to 1.
            bodyMaterial.SetFloat("_ColorIntensity", (HighlightBodyTime / highlightBodyDuration) + highlightBodyIntensity);

            if (HighlightBodyTime <= 0)
            {
                // Set body color intensity back to 1
                bodyMaterial.SetFloat("_ColorIntensity", 1f);

                // Stop this behaviour
                isHighlightBodyActive = false;
            }
        }
    }

    /// <summary>
    /// Use this class to activate the highlight body effect. This effect will set the Color Intensity of the bodyMaterial to
    /// the argument intensity and it will go back to its default value of 1 after duration.
    /// </summary>
    public void SetActiveHighlightBody(float duration, float intensity)
    {
        isHighlightBodyActive = true;
        highlightBodyIntensity = intensity;
        HighlightBodyTime = duration;
        highlightBodyDuration = duration;
    }
}
