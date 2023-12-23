using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    public int moveSpeed = 4;
    private Animator animator;
    public static Player instance;

    public string areaTransitionName;   // exit just used changed by AreaExit&AreaEntrance scripts

    void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null)
        {
            CameraFollow2D script = mainCamera.GetComponent<CameraFollow2D>();
            script.target = instance.transform;
        }


        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
        rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("ON SCENE LOADED CALLED");
    }

    void Update()
    {
        if (animator == null) { return; }
        float moveX = Input.GetAxisRaw("Horizontal");   // Input = InputManager (edit -> project settings -> InputManager)
        float moveY = Input.GetAxisRaw("Vertical");
        animator.SetFloat("moveX", moveX);
        animator.SetFloat("moveY", moveY);

        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            animator.SetFloat("lastMoveX", moveX);
            animator.SetFloat("lastMoveY", moveY);
        }
        Vector3 difference = new Vector3(moveX, moveY, 0).normalized;
        transform.position += moveSpeed * Time.deltaTime * difference;
    }
}
