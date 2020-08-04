using UnityEngine;

[System.Serializable]
public class Sound
{
    // NOTE: To show this kind of classes in the Inspector, make sure you add 
    // [System.Serializable] and also remove the Monobehaviour type.

    public string notes;
    public AudioClip clip;

    [Range(0f, 1f)]
    [SerializeField] private float _Volume = 1f;
    public float Volume { get { return _Volume; } private set { _Volume = value; } }

    [Range(0f, 1f)]
    [SerializeField] private float _MinPitch = 1f;
    public float MinPitch { get { return _MinPitch; } private set { _MinPitch = value; } }

    [Range(0f, 1f)]
    [SerializeField] private float _MaxPitch = 1f;
    public float MaxPitch { get { return _MaxPitch; } private set { _MaxPitch = value; } }
}
