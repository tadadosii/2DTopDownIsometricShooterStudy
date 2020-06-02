using UnityEngine;

/// <summary>
/// To shoot a projectile towards its x axis (bool to inverted) using Transform.Translate
/// and a public speed variable. It also checks for 2D trigger collisions.
/// </summary>
[RequireComponent(typeof(SoundFXHandler))]
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
    public float speed;
    public float lifetime;
    public bool invertDirection;

    [Header("FXs")]
    public GameObject hitPFX;

    private SpriteRenderer _Renderer;
    private SoundFXHandler _Sfx;
    private Vector3 travelDirection;
    private float movement;
    private bool hasLaunched;
    #endregion

    #region ---------------------------- UNITY CALLBACKS
    private void Awake()
    {
        TryGetComponent(out _Sfx);
        TryGetComponent(out _Renderer);
    }

    private void Update()
    {
        if (hasLaunched)
            Travel();
        //Debug.DrawRay(transform.position, transform.up * 30f, Color.red);
    }
    #endregion

    #region ---------------------------- METHODS
    public void SetActive(bool value)
    {
        _Renderer.enabled = value;
    }

    public void Fire()
    {
        //Debug.Log("Projectile: Fire()");
        hasLaunched = true;
        _Sfx.PlaySound(0);

        if (!invertDirection)
            travelDirection = Vector3.right;
        else
            travelDirection = -Vector3.right;
        transform.parent = null;
        Destroy(gameObject, lifetime);
    }

    private void Travel()
    {
        movement = Time.deltaTime * speed;
        transform.Translate(travelDirection.normalized * movement, Space.Self);
    }
    #endregion

    #region ---------------------------- COLLISIONS
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Wall"))
        {
            Instantiate(hitPFX, collision.ClosestPoint(transform.position), Quaternion.identity);
            Destroy(gameObject);
        }
    }
    #endregion
}
