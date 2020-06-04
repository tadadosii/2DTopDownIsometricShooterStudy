using UnityEngine;

public class CameraBehaviour : MonoBehaviour
{
    // --------------------------------------
    // - 2D TopDown Isometric Shooter Study -
    // ----------- by Tadadosi --------------
    // --------------------------------------
    // ---- https://twitter.com/tadadosi ----
    // --------------------------------------

    public float followSpeed;
    public Transform target;

    private void Update()
    {
        Vector3 newPosition = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.position = Vector3.Lerp(transform.position, newPosition, Time.deltaTime * followSpeed);
    }
}
