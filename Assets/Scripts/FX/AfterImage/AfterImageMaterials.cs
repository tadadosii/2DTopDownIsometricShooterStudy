using UnityEngine;

public class AfterImageMaterials : MonoBehaviour
{
    public Material[] materials;

    private void Awake()
    {
        CheckIfReady();
    }

    private void CheckIfReady()
    {
        // Adding 6 materials because that's the max amount of after images that I think I could use.
        if (materials.Length == 0 || materials.Length < 6)
            Debug.LogWarning(gameObject.name + ": has no materials or needs to have at least 6 materials!");

        for (int i = 0; i < materials.Length; i++)
        {
            if (materials[i] == null)
                Debug.LogWarning(gameObject.name + ": material is missing, add it!");
        }
    }
}
