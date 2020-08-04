using System.Collections;
using UnityEngine;

public class AfterImageGroup : MonoBehaviour
{
    public AfterImage[] AfterImages { get; set; }

    private AfterImageHandler _AfterImageHandler;
    private int _CurrentImageIndex;
    private const float SET_DELAY = 0.025f;

    private void Awake()
    {
        _AfterImageHandler = FindObjectOfType<AfterImageHandler>();
        _AfterImageHandler.AddAfterImageGroup(this);
    }

    public void SetActive()
    {
        StartCoroutine(CO_SetActiveAfterImage());
    }

    private IEnumerator CO_SetActiveAfterImage()
    {
        int i = 0;
        while (i < AfterImages.Length)
        {
            SetActiveAfterImage();
            yield return new WaitForSeconds(SET_DELAY);
            i++;
        }
    }

    private void SetActiveAfterImage()
    {
        AfterImages[_CurrentImageIndex].SetActive();
        _CurrentImageIndex = ArraysHandler.GetNextIndex(_CurrentImageIndex, AfterImages.Length);
    }
}
