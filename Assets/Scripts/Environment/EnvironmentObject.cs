using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnvironmentObject : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public enum WhenPlayerIsOnPosition { Above, Left, Right, Below }
    public WhenPlayerIsOnPosition startsOnPlayerPosition = WhenPlayerIsOnPosition.Right;

    public GameObject whenPlayerIsAbove;
    public GameObject whenPlayerIsOnLeft;
    public GameObject whenPlayerIsOnRight;
    public GameObject whenPlayerIsBelow;

    private GameObject[] objects;
    private SpriteRenderer spriteRenderer;

    private readonly bool debug = false;

    private void Awake()
    {
        StoreObjects();
        SetActiveObject(startsOnPlayerPosition);

        TryGetComponent(out spriteRenderer);
        if (debug)
            CheckIfMissingObjects();
    }

    public void SetActiveObject(WhenPlayerIsOnPosition position)
    {
        DisableSets();

        switch (position)
        {
            case WhenPlayerIsOnPosition.Above:
                if (whenPlayerIsAbove != null)
                    whenPlayerIsAbove.SetActive(true);
                break;

            case WhenPlayerIsOnPosition.Left:
                if (whenPlayerIsOnLeft != null)
                    whenPlayerIsOnLeft.SetActive(true);
                break;

            case WhenPlayerIsOnPosition.Right:
                if (whenPlayerIsOnRight != null)
                    whenPlayerIsOnRight.SetActive(true);
                break;

            case WhenPlayerIsOnPosition.Below:
                if (whenPlayerIsBelow != null)
                    whenPlayerIsBelow.SetActive(true);
                break;

            default:
                break;
        }
    }

    private void DisableSets()
    {
        for (int i = 0; i < objects.Length; i++)
        {
            if (objects[i] != null)
                objects[i].SetActive(false);
        }
    }

    /// <summary>
    /// Used as a debug method only.
    /// </summary>
    private void CheckIfMissingObjects()
    {
        if (whenPlayerIsAbove == null || whenPlayerIsOnLeft == null || whenPlayerIsOnRight == null || whenPlayerIsBelow == null)
        {
            Debug.LogWarning(gameObject.name + ": One or more objects missing!");
        }
    }

    private void StoreObjects()
    {
        objects = new GameObject[] { whenPlayerIsAbove, whenPlayerIsOnLeft, whenPlayerIsOnRight, whenPlayerIsBelow };
    }
}
