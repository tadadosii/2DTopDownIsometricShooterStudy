using UnityEngine;

public class AfterImageGenerator : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    [TextArea(10, 13)]
    public string notes = "The gameobject with this class attached, and that has a Sprite Renderer, " +
        "will create a new gameobject with an AfterImageGroup class attached to it and a hardcoded " +
        "amount of AfterImage classes stored in the group. When the ground is created, it will immediately " +
        "look for the class AfterImageHandler and add itself to a List. This list is then used by that class to " +
        "set Active all the AfterImages stored in each group. I made it like this to avoid the hassle of " +
        "manually creating a bunch of after images and the pain it could be to change them all if I ever needed to.";

    public GameObject afterImagePrefab;

    private AfterImageMaterials _AfterImageMaterials;
    private SpriteRenderer _SpriteRenderer;
    public AfterImage[] _AfterImages { get; private set; }

    /// <summary>
    /// How many AfterImage classes and sprites should CreateAfterImages() instantiate? 
    /// If you want to add more than 6, make sure you have the same amount of materials in the materials
    /// array located in the class <see cref="AfterImageMaterials"/>.
    /// </summary>
    private const int MAX_AMOUNT = 6;

    private void Awake()
    {
        _AfterImageMaterials = FindObjectOfType<AfterImageMaterials>();
        TryGetComponent(out _SpriteRenderer);
        CreateAfterImages();
    }

    private void CreateAfterImages()
    {
        if (_SpriteRenderer == null || _AfterImageMaterials == null || afterImagePrefab == null)
        {
            Debug.LogError(gameObject.name + ": Has no SpriteRenderer or no AfterImagePrefab or AfterImageMaterials was not found!");
            Debug.Break();
            return;
        }

        // Note: I think AfterImageGroup could be further optimized by instantiating a prefab instead of adding the component. 
        // Create a new gameobject and add a AfterImageGroup class to it.
        AfterImageGroup group = new GameObject(gameObject.name + "_AfterImage_Group").AddComponent<AfterImageGroup>();

        // Initialize _AfterImages array.
        _AfterImages = new AfterImage[MAX_AMOUNT];

        // Populate the array.
        for (int i = 0; i < MAX_AMOUNT; i++)
        {
            // Instantiate afterImagePrefab and store a AfterImage class variable at the same time.
            AfterImage afterImage = Instantiate(afterImagePrefab, _AfterImageMaterials.transform).GetComponent<AfterImage>();

            // Name it to have everything organized in the scene hierarchy.
            afterImage.name = gameObject.name + "_AfterImage" + i;

            // Make it a child of the group transform (again to organize).
            afterImage.transform.SetParent(group.transform);

            // Set the properties that should be used by this AfterImage.
            afterImage.SetProperties(_SpriteRenderer.sprite, _AfterImageMaterials.materials[i], posAndRotReference: transform);

            // Add it to the _AfterImages array.
            _AfterImages[i] = afterImage;
        }

        // Pass the _AfterImages array to the group array.
        group.AfterImages = _AfterImages;
    }
}
