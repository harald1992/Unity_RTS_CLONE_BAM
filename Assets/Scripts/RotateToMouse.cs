using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotateToMouse : MonoBehaviour
{


    // Start is called before the first frame update
    void Start()
    {

    }



    // Update is called once per frame
    void Update()
    {
        Vector3 mousePosition = MousePosition.instance.GetMousePositionFromRotatedCamera();
        Vector3 direction = mousePosition - transform.position;

        transform.up = direction;
    }
}
