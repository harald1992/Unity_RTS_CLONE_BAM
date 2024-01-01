using System;
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
            StandardView();
            // IsometricView();
        }
    }

    private void StandardView()
    {
        transform.position = new Vector3(target.position.x, target.position.y - 5f, transform.position.z);
        transform.rotation = Quaternion.Euler(-30f, 0f, 0f);
    }

    private void IsometricView()
    {
        transform.position = new Vector3(target.position.x, target.position.y - 5f, transform.position.z);
        transform.rotation = Quaternion.Euler(-30f, 0f, -15f);
        Player.instance.transform.rotation = Quaternion.Euler(0f, 0f, -15f);
    }
}
