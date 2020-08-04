using UnityEngine;

/// <summary>
/// Script by the github user ftvs downloaded from https://gist.github.com/ftvs/5822103 
/// some editing was made to have a public static method called Shake() that can be easily
/// called from any other class.
/// </summary>
public class CameraShake : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    // NOTE: Maybe I should remane this to something more generic as this could be used on any object, not just the camera.

    [TextArea(4, 10)]
	public string notes = "Class by the github user ftvs downloaded from https://gist.github.com/ftvs/5822103, " +
		"some editing was made to have a public static method called Shake() that can be easily called from any other class. " +
        "With this behaviour an object will shake and then it will return to a default position stored in the Awake method.";

	// Amplitude of the shake. A larger value shakes the camera harder.
	private static float _ShakeAmount;
	private static float _DecreaseFactor;

	private static Vector3 defaultPosition;

	// How long the object should shake for.
	private static float shakeDuration;
	private static bool isActive;

    private void Awake()
    {
        defaultPosition = transform.localPosition;
    }

    private void FixedUpdate()
	{
		if (PauseController.isGamePaused)
			return;

		if (shakeDuration > 0 && isActive)
		{			
			transform.localPosition = defaultPosition + Random.insideUnitSphere * _ShakeAmount;
			shakeDuration -= Time.deltaTime * _DecreaseFactor;
			if (shakeDuration <= 0)
			{
				shakeDuration = 0;
			}
		}
		else
		{ 			
			if (isActive)
			{
				shakeDuration = 0f;
                transform.localPosition = defaultPosition;
				isActive = false;
			}	
		}
	}

	public static void Shake(float duration, float shakeAmount, float decreaseFactor)
	{
        _ShakeAmount = shakeAmount;
		_DecreaseFactor = decreaseFactor;
		shakeDuration = duration;
		isActive = true;
	}
}