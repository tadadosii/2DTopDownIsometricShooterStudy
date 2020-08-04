using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSizeHandler : MonoBehaviour
{
    public float speed;
    public float maxSize;
    public AnimationCurve curve;

    private void FixedUpdate()
    {
        float curveTime = curve.Evaluate(Time.fixedTime * speed);
        Camera.main.orthographicSize = curveTime * maxSize;
    }
}
