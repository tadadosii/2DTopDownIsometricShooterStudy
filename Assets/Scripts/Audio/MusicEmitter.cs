using UnityEngine;
/// <summary>
/// This class is a derived class that's inheriting from the base class SoundEmitter.
/// </summary>
public class MusicEmitter : SoundEmitter
{
    [TextArea(4,10)]
    public string notes = "This class is a derived class that's inheriting from the base class SoundEmitter.";

    protected override void Awake()
    {
        base.Awake();
        Source.playOnAwake = false;
        Source.loop = true;
    }
}
