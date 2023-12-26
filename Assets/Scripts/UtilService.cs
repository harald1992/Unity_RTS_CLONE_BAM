using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UtilService : MonoBehaviour
{

    public static UtilService instance;
    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }


    public Vector2 GetMousePosition2D()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // Adjusting the z coordinate
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePosition;
    }

    public Vector3 GetMousePosition3D()
    {
        Vector3 mousePos = Input.mousePosition;
        mousePos.z = -Camera.main.transform.position.z; // Adjusting the z coordinate
        // mousePos.z = 0;

        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(mousePos);
        return mousePosition;
    }
}
