using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class Physics2DHandler : MonoBehaviour
{
    protected Rigidbody2D _RigidBody;
    protected Collider2D _Collider;

    protected virtual void Awake()
    {
        TryGetComponent(out _RigidBody);
        TryGetComponent(out _Collider);
    }

    public void SetActiveCollider(bool value)
    {
        if (_Collider != null)
            _Collider.enabled = value;
    }

    public void SetRigidbodyType(RigidbodyType2D type)
    {
        if (_RigidBody != null)
            _RigidBody.bodyType = type;
    }

    /// <summary>
    /// Rigidbody2D velocity.
    /// </summary>
    public Vector2 Velocity { get { return _RigidBody.velocity; } private set { _RigidBody.velocity = value; } } 

    public virtual void SetVelocity(Vector2 newVelocity)
    {
        if (_RigidBody != null)
            Velocity = newVelocity;
    }

    public virtual void SetVelocity(Vector2 direction, float speed)
    {
        if (_RigidBody != null)
            Velocity = direction * speed;
    }

    public virtual void AddVelocity(Vector2 value)
    {
        if (_RigidBody != null)
            Velocity += value;
    }

    public virtual void AddVelocity(Vector2 direction, float value)
    {
        if (_RigidBody != null)
            Velocity += new Vector2 (direction.x + value, direction.y + value);
    }

    public virtual void AddForce(Vector2 direction, float force, ForceMode2D mode)
    {
        if (_RigidBody != null)
            _RigidBody.AddForce(direction * force, mode);
    }
}
