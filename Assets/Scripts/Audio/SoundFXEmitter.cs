public class SoundFXEmitter : SoundEmitter
{
    protected override void Awake()
    {
        base.Awake();
        _Source.playOnAwake = false;
        _Source.loop = false;
    }
}
