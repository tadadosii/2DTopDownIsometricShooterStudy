using UnityEngine;

/// <summary>
/// To shoot a projectile towards the current body direction using Transform.Translate
/// and a public speed variable. It also checks for 2D trigger collisions.
/// </summary>
public class Projectile : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    // NOTE: Remove destroy actions if a pool system is implemented

    #region ---------------------------- PROPERTIES

    public float Speed { get { return _Speed; } set {  _Speed = value; } }
    [SerializeField] private float _Speed = 20f;

    public float LifeTime { get { return _LifeTime; } set { _LifeTime = value; } }
    [SerializeField] private float _LifeTime = 1f;

    [Header("FXs")]
    [SerializeField] private GameObject hitPFX = null;

    private BoxCollider2D _Collider;
    private SpriteRenderer _Renderer;
    private SoundHandlerLocal _Sfx;

    private Vector3 travelDirection;
    private float movement;
    private bool hasLaunched;
    private int impactCount;

    #endregion

    #region ---------------------------- UNITY CALLBACKS
    private void Awake()
    {
        TryGetComponent(out _Sfx);
        TryGetComponent(out _Renderer);
        TryGetComponent(out _Collider);
    }

    private void Start()
    {
        // Used to get a message on the console if any important component is missing.
        if (_Sfx == null || _Renderer == null || _Collider == null)
        {
            Debug.LogWarning(gameObject.name + ": BoxCollider2D || SpriteRenderer || SoundFXHandler!");
        }
    }

    private void Update()
    {
        // If Fire() is called this becomes true and Travel() gets updated until this projectile gets destroyed.
        if (hasLaunched)
            Travel();
        //Debug.DrawRay(transform.position, transform.up * 30f, Color.red);
    }
    #endregion

    #region ---------------------------- METHODS
    /// <summary>
    /// To enable or disable this projectile <see cref="SpriteRenderer"/> and <see cref="BoxCollider2D"/>.
    /// </summary>
    /// <param name="value"></param>
    public void SetActive(bool value)
    {
        _Renderer.enabled = _Collider.enabled = value;
    }

    /// <summary>
    /// To launch the projectile towards <see cref="travelDirection"/>.
    /// </summary>
    public void Fire()
    {
        //Debug.Log("Projectile: Fire()");
        hasLaunched = true;
        _Sfx.PlaySound(0);

        // Set travel direction based on the current direction of the body
        if (!PlayerBodyPartsHandler.isRightDirection)
            travelDirection = -Vector3.right;
        else
            travelDirection = Vector3.right;

        transform.parent = null;
        Destroy(gameObject, LifeTime);
    }

    /// <summary>
    /// Moves the projectile towards <see cref="travelDirection"/> using transform.Translate.
    /// </summary>
    private void Travel()
    {
        movement = Time.deltaTime * Speed;
        transform.Translate(travelDirection.normalized * movement, Space.Self);
    }
    #endregion

    #region ---------------------------- COLLISIONS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // Used to break the method at this point if it already had an impact.
        if (impactCount > 0)
            return;

        if (collision.CompareTag("Wall"))
        {
            // Add +1 to the inpact count.
            impactCount++;

            // Spawn the impact visual effect.
            Instantiate(hitPFX, collision.ClosestPoint(transform.position), Quaternion.identity);

            // Destroy this gameobject (this happens in the next frame after all the actions of this method are executed).
            Destroy(gameObject);
        }
    }
    #endregion
}
