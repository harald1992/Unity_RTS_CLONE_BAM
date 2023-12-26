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
        // unitScript.Start();
        unitScript.SetupHealthBar();

        gameObject.tag = "Player";
        GameObject playerCamera = GameObject.FindWithTag("MainCamera");
        if (playerCamera != null)
        {
            CameraFollow2D script = playerCamera.GetComponent<CameraFollow2D>();
            if (script != null)
            {
                script.target = transform;

            }
        }


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
            // unitScript.StopActionCoroutine();
            unitScript.animator.SetFloat("lastMoveX", moveX);
            unitScript.animator.SetFloat("lastMoveY", moveY);
        }
        Vector3 difference = new Vector3(moveX, moveY, 0).normalized;
        transform.position += unitScript.moveSpeed * Time.deltaTime * difference;
    }

}
