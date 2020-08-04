using System.Collections.Generic;
using UnityEngine;

public class AfterImageHandler : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    private List<AfterImageGroup> _AfterImagesGroups = new List<AfterImageGroup>();

    public void AddAfterImageGroup(AfterImageGroup group)
    {
        group.transform.SetParent(transform);
        _AfterImagesGroups.Add(group);
    }

    public void SetActiveAfterImages()
    {
        for (int i = 0; i < _AfterImagesGroups.Count; i++)
        {
            _AfterImagesGroups[i].SetActive();
        }
    }
}
