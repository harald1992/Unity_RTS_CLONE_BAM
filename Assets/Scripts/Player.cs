using System.Collections;
using System.Collections.Generic;
using System.Numerics;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Rigidbody2D rigidbody2D;

    public float moveSpeed = 5f;
    private Animator animator;
    public static Player instance;

    public string areaTransitionName;   // exit just used changed by AreaExit&AreaEntrance scripts

    void Start()
    {
        // if (instance == null)
        // {
        animator = gameObject.GetComponent<Animator>();
        // instance = this;
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null)
        {
            CameraFollow2D script = mainCamera.GetComponent<CameraFollow2D>();
            script.target = instance.transform;
        }
        // }
        // else
        // {
        // Destroy(gameObject); // Make it a singleton, so if a new object with script player is constructed, that new object is destroyed
        // }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event

        rigidbody2D = GetComponent<Rigidbody2D>();
        // DontDestroyOnLoad(gameObject);  // dont destroy object when changing scene
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

        // Normalize the movement vector to ensure constant speed in all directions
        UnityEngine.Vector2 movement = new UnityEngine.Vector2(moveX, moveY).normalized;
        rigidbody2D.velocity = movement * moveSpeed;
    }
}
