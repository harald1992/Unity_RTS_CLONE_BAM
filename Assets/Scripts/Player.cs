using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : MonoBehaviour
{
    private Unit unitScript;
    private Rigidbody2D rigidbody2D;

    public static Player instance;

    public string areaTransitionName;   // exit just used changed by AreaExit&AreaEntrance scripts

    private void Start()
    {
        unitScript = gameObject.GetComponent<Unit>();

        unitScript.Start();

        gameObject.tag = "Player";
        GameObject mainCamera = GameObject.FindWithTag("MainCamera");
        if (mainCamera != null)
        {
            CameraFollow2D script = mainCamera.GetComponent<CameraFollow2D>();
            script.target = instance.transform;
        }

        SceneManager.sceneLoaded += OnSceneLoaded; // Subscribe to the sceneLoaded event
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Debug.Log("ON SCENE LOADED CALLED");
    }

    private void Update()
    {
        if (unitScript.animator == null) { return; }

        if (Input.GetKeyDown(KeyCode.Space))
        {
            unitScript.Attack();
        }


        // if (!unitScript.animator.GetBool("isAttacking"))
        // {
        MovementOnArrowKeys();
        // }


    }

    protected void MovementOnArrowKeys()
    {
        float moveX = Input.GetAxisRaw("Horizontal");   // Input = InputManager (edit -> project settings -> InputManager)
        float moveY = Input.GetAxisRaw("Vertical");
        unitScript.animator.SetFloat("moveX", moveX);
        unitScript.animator.SetFloat("moveY", moveY);

        if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
        {
            // Debug.Log("stop coroutine?");
            // unitScript.StopActionCoroutine();
            unitScript.animator.SetFloat("lastMoveX", moveX);
            unitScript.animator.SetFloat("lastMoveY", moveY);
        }
        Vector3 difference = new Vector3(moveX, moveY, 0).normalized;
        transform.position += unitScript.moveSpeed * Time.deltaTime * difference;
    }

}
