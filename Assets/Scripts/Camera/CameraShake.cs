using UnityEngine;

/// <summary>
/// script by the github user ftvs downloaded from https://gist.github.com/ftvs/5822103 
/// some editing was made to have a public static method called Shake() that can be easily
/// called from any other class 
/// </summary>
public class CameraShake : MonoBehaviour
{
	// Amplitude of the shake. A larger value shakes the camera harder.
	private static float _ShakeAmount;
	private static float _DecreaseFactor;

	// Transform of the camera to shake
	private static Transform camTransform;

	private static Vector3 originalPos;
	// How long the object should shake for.
	private static float shakeDuration;
	private static bool isActive;

	void Awake()
	{
		if (camTransform == null)
		{
			camTransform = Camera.main.transform;
		}
	}

	void Update()
	{
		if (shakeDuration > 0 && isActive)
		{
			originalPos = camTransform.localPosition;
			camTransform.localPosition = originalPos + Random.insideUnitSphere * _ShakeAmount;
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
				camTransform.localPosition = originalPos;
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