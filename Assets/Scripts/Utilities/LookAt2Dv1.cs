using UnityEngine;

/// <summary>
/// The gameobject that has this component attached will instantly rotate to make its x or y axis look 
/// towards the assigned target or towards mouse world position if a public bool is checked. The direction can be
/// inverted by checking a bool. Also there is an option to disable local update if a linked control is needed.
/// </summary>
public class LookAt2Dv1 : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public enum Axis { x, y }
    public Axis axis = Axis.y;
    public Transform target;
    public bool localUpdate;
    public bool lookAtMousePosition;
    public bool inverted;

    [Header("Debug")]
    public Color debugColor;
    public bool debug;

    private Vector2 direction;

    private void Update()
    {
        if (!localUpdate)
            return;
        UpdateLookAt();
    }

    public void UpdateLookAt()
    {
        // Calculate normalized direction
        if (lookAtMousePosition)
        {
            direction = (MouseInput.mouseWorldPos - transform.position).normalized;
        }
        else
        {
            if (target == null)
            {
                Debug.LogError(gameObject.name + " target missing!");
                return;
            }
            direction = (target.position - transform.position).normalized;

        }


        switch (axis)
        {
            case Axis.x:
                if (!inverted)
                    transform.right = direction; // Point x axis towards direction
                else
                    transform.right = -direction; // Point x axis towards inverted direction
                break;

            case Axis.y:
                if (!inverted)
                    transform.up = direction; // Point y axis towards direction
                else
                    transform.up = -direction; // Point y axis towards inverted direction
                break;

            default:
                break;
        }

        if (debug)
            if (target != null)
                Debug.DrawLine(transform.position, target.position, debugColor);
            else
                Debug.DrawLine(transform.position, MouseInput.mouseWorldPos, debugColor);
    }
}
