using UnityEngine;

public class PlayerPhysics : Physics2DHandler
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public float MoveSpeed { get { return _MoveSpeed; } private set { _MoveSpeed = value; } }
    [SerializeField] private float _MoveSpeed = 6f;

    public bool CanMove { get; set; }

    protected override void Awake()
    {
        base.Awake();
        CanMove = true;
    }

    private void FixedUpdate()
    {
        if (CanMove)
            SetVelocity(TadaInput.MoveAxisRawInput, _MoveSpeed);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision != null)
        {
            if (collision.CompareTag("EnvironmentTrigger"))
            {
                collision.TryGetComponent(out EnvironmentObjectTrigger trigger);
                trigger.SetActiveObjects();
            }
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision != null)
        {

        }
    }
}
