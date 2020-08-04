using UnityEngine;

public class AfterImage : MonoBehaviour
{
    // --------------------------------------
    // ----- 2D Isometric Shooter Study -----
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- Support my work by following ----
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    private SpriteRenderer _SpriteRenderer;
    private Transform _Reference;

    private Vector2 moveDirection;
    private bool isActive;
    private float t;

    private const float MOVE_SPEED = 0.125f;
    private const float DURATION = 0.3f;

    private void Awake()
    {
        TryGetComponent(out _SpriteRenderer);
    }

    private void Update()
    {
        if (isActive)
        {
            t -= Time.deltaTime;

            // Reduce alpha from 1 to 0.
            _SpriteRenderer.color = new Color(1, 1, 1, t / DURATION);

            // Move this gameobject towards move direction.
            transform.Translate(moveDirection * MOVE_SPEED * t, Space.World);

            if (t <= 0)
            {        
                // Disable gameobject
                gameObject.SetActive(false);

                // Set sprite color back to white.
                _SpriteRenderer.color = Color.white;

                // Stop this behaviour
                isActive = false;

                // Reset t.
                t = DURATION;
            }
        }
    }

    public void SetActive()
    {
        // Reset t.
        t = DURATION;

        // Enable this gameobject
        gameObject.SetActive(true);

        // Store last move direction.
        moveDirection = TadaInput.MoveAxisRawInput;

        // Store last position and rotation.
        transform.position = _Reference.position;
        transform.rotation = _Reference.rotation;

        // Activate Update() behaviour.
        isActive = true;
    }

    public void SetProperties(Sprite sprite, Material material,  Transform posAndRotReference)
    {
        _SpriteRenderer.sprite = sprite;
        _SpriteRenderer.sharedMaterial = material;
        _Reference = posAndRotReference;
        gameObject.SetActive(false);
    }
}
