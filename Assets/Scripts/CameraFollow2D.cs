using UnityEngine;

public class CameraFollow2D : MonoBehaviour
{
    public Transform target; // Reference to the player's transform
                             // public float smoothSpeed = 0.125f; // Adjust this value to set the smoothness of the camera follow
                             // public Vector3 offset = new Vector3(0, 0, -10); // Offset of the camera from the player





    // LateUpdate is called once per frame after Update()
    void LateUpdate()
    {
        if (target != null)
        {
            // Vector3 desiredPosition = target.position + offset;
            // Vector3 smoothedPosition = Vector3.Lerp(transform.position, desiredPosition, smoothSpeed);
            // transform.position = new Vector3(smoothedPosition.x, smoothedPosition.y, -10);
            transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        }
    }

}
