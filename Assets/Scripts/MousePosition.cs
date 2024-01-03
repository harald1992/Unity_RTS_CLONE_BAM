using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MousePosition : MonoBehaviour
{

    [SerializeField]
    private Vector3 _screenPosition = Vector3.zero;

    [SerializeField]
    private Vector3 _worldPositionRotatedCamera = Vector3.zero;

    public Camera playerCamera;
    public Camera normalCamera;
    public LayerMask layersToHit;   // only hit one layer

    public static MousePosition instance;

    // Start is called before the first frame update
    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }

    }

    // Update is called once per frame
    void Update()
    { }

    public Vector3 GetMousePositionFromUnrotatedCamera()
    {
        Vector3 mousePosition = Input.mousePosition;
        mousePosition.z = 10f;
        _screenPosition = normalCamera.ScreenToWorldPoint(mousePosition);
        return _screenPosition;
    }

    public Vector3 GetMousePositionFromRotatedCamera()
    {
        Vector3 screenPosition = Input.mousePosition;
        screenPosition.z = 10f;

        // create a ray between mouseposition towards camera
        Ray ray = playerCamera.ScreenPointToRay(screenPosition);

        int maxDistance = 100;
        if (Physics.Raycast(ray, out RaycastHit hitData, maxDistance, layersToHit))
        {
            _worldPositionRotatedCamera = hitData.point;
        }

        _worldPositionRotatedCamera.z = 0;
        transform.position = _worldPositionRotatedCamera;

        return _worldPositionRotatedCamera;
    }
}
