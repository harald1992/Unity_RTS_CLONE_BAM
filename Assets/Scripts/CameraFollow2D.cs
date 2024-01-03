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
            // transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);

            // MildRotation();
            IsometricView();
        }
    }

    private void MildRotation()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(-30f, 0f, 0f);
    }

    private void IsometricView()
    {
        transform.position = new Vector3(target.position.x, target.position.y, transform.position.z);
        transform.rotation = Quaternion.Euler(-30f, -45f, 60f);
        SpriteRenderer[] allSprites = FindObjectsOfType<SpriteRenderer>();
        foreach (var sprite in allSprites)
        {
            sprite.gameObject.transform.rotation = Quaternion.Euler(-30, -45, 60f);

        }
    }
}
