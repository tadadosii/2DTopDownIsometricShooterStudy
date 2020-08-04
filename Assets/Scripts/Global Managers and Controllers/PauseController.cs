using UnityEngine;
using UnityEngine.Events;

public class PauseController : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public UnityEvent onGamePause;
    public UnityEvent onGameResume;
    public static bool isGamePaused;

    private void Update()
    {
        // Using Unity Events to easily connect UI actions in the inspector without hardcoding those actions.
        if (TadaInput.GetKeyDown(TadaInput.ThisKey.Pause))
        {
            if (!isGamePaused)
            {
                isGamePaused = true;
                OnGamePause();
                return;
            }
            isGamePaused = false;
            OnGameResume();
        }
    }

    private void OnGamePause()
    {
        Time.timeScale = 0;

        // Pause audio
        AudioListener.pause = true;

        // If there is at least one event added in the Inspector tab, Invoke it.
        if (onGamePause != null)
            onGamePause.Invoke();
    }

    private void OnGameResume()
    {
        Time.timeScale = 1;

        // Resume audio
        AudioListener.pause = false;

        // If there is at least one event added in the Inspector tab, Invoke it.
        if (onGameResume != null)
            onGameResume.Invoke();
    }
}
