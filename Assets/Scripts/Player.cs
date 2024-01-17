using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Unit unitScript;

    public static Player instance;
    // public string areaTransitionName;   // exit just used changed by AreaExit&AreaEntrance scripts


    private bool isRightClick;

    // Rigidbody rb;

    private void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        // rb = GetComponent<Rigidbody>();



        unitScript = gameObject.GetComponent<Unit>();
        unitScript.maxHp = 25f;
        unitScript.currentHp = 25f;
        unitScript.maxMp = 10f;
        unitScript.currentMp = 10f;
        unitScript.attack = 2f;

        GameEvents.instance.PlayerChanged();

        unitScript.SetupHealthBar();

        gameObject.tag = "Player";
        GameObject cameraPivot = GameObject.FindWithTag("CameraPivot");
        if (cameraPivot != null)
        {
            CameraFollowPlayer script = cameraPivot.GetComponent<CameraFollowPlayer>();
            script.target = transform;
        }
    }

    private void FixedUpdate()
    {

    }

    private void Update()
    {
        // if (unitScript.animator == null) { return; }

        MovementControls();
    }

    private void MovementControls()
    {
        if (Input.GetMouseButtonDown(0) || Input.GetKeyDown(KeyCode.Space))
        {
            unitScript.Attack();

        }

        if (Input.GetKeyDown(KeyCode.UpArrow) || Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.DownArrow) || Input.GetKeyDown(KeyCode.LeftArrow))
        {
            // MovementOnArrowKeys();
        }
        ChangeOrientationOnMousePosition();
        MovementOnRightClick();
    }

    private void ChangeOrientationOnMousePosition()
    {
        // 3D
        Vector3 mousePosition = MousePosition.instance.GetMousePositionFromRotatedCamera();
        transform.LookAt(mousePosition);
    }

    private bool IsRunning()
    {
        return unitScript.animator.GetCurrentAnimatorStateInfo(0).IsName("Run");
    }

    private void MovementOnRightClick()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRightClick = true;
        }
        else if (Input.GetMouseButtonUp(1))
        {
            isRightClick = false;
        }

        if (isRightClick)
        {


            //     unitScript.animator.SetBool("isRunning", true);
            //     unitScript.animator.SetBool("isIdle", false);

            // }
            if (!unitScript.animator.GetBool("isRunning"))
            {
                Debug.Log("set isRunning");
                unitScript.animator.SetBool("isIdle", false);

                unitScript.animator.SetBool("isRunning", true);
                unitScript.animator.SetTrigger("run");

            }



            // Debug.Log("set running");
            // }

            /* 3D */
            Vector3 mousePosition = MousePosition.instance.GetMousePositionFromRotatedCamera();
            // Vector3 direction = mousePosition - transform.position;

            float speed = 5;
            float step = speed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, mousePosition, step);

        }
        else
        {
            // if (!unitScript.animator.GetCurrentAnimatorStateInfo(0).IsName("Idle"))
            // {
            //     unitScript.animator.SetTrigger("idle");

            // }

            if (!unitScript.animator.GetBool("isIdle"))
            {
                unitScript.animator.SetBool("isIdle", true);
                unitScript.animator.SetBool("isRunning", false);

                unitScript.animator.SetTrigger("idle");


            }
            // if (!unitScript.animator.GetBool("isIdle") && !unitScript.animator.GetBool("isRunning"))
            // {
            //     unitScript.animator.SetBool("isRunning", false);
            //     unitScript.animator.SetBool("isIdle", true);
            // }

        }

    }




    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactible))
        {
            interactible.Interact(gameObject);
        }

    }

}
