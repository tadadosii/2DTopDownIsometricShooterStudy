using UnityEngine;

/// <summary>
/// To set the sprites layer order based on the direction where the player is pointing at
/// </summary>
public class BodyPartsOrder : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------

    public SpriteRenderer[] handRenderers;
    public SpriteRenderer[] headRenderers;
    private bool inFront;

    private void Update()
    {
        if (MouseInput.directionFromPlayerToMouseWorldPos.y < 0 && inFront)
        {
            UpdateRenderersLayerOrder(handRenderers, 9);
            UpdateRenderersLayerOrder(headRenderers, 13);
            inFront = false;
        }
        else if (MouseInput.directionFromPlayerToMouseWorldPos.y > 0 && !inFront)
        {
            UpdateRenderersLayerOrder(handRenderers, 11);
            UpdateRenderersLayerOrder(headRenderers, 5);
            inFront = true;
        }
    }

    private void UpdateRenderersLayerOrder(SpriteRenderer[] spriteRenderers, int layerOrder)
    {
        for (int i = 0; i < spriteRenderers.Length; i++)
        {
            if (spriteRenderers[i] != null)
            {
                spriteRenderers[i].sortingOrder = layerOrder;
            }
        }
    }
}
