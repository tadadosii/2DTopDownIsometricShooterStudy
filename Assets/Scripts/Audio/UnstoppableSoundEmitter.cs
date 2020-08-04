using UnityEngine;

public class UnstoppableSoundEmitter : SoundEmitter
{
    [TextArea(4, 10)]
    public string notes = "This class is a derived class that's inheriting from the base class SoundEmitter. Sounds played " +
        "using this class will play until they finish (cannot be stopped, e.g. projectile shots, impacts, etc.)";

    protected override void Awake()
    {
        base.Awake();
        Source.playOnAwake = false;
        Source.loop = false;
    }
}
