using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Unit unitScript;

    public static Player instance;
    public string areaTransitionName;   // exit just used changed by AreaExit&AreaEntrance scripts


    private bool isRightClick;

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

        unitScript = gameObject.GetComponent<Unit>();
        unitScript.maxHp = PlayerStats.instance.maxHp;
        unitScript.currentHp = unitScript.maxHp;
        unitScript.maxMp = PlayerStats.instance.maxMp;
        unitScript.currentMp = PlayerStats.instance.currentMp;
        unitScript.attack = PlayerStats.instance.attack;

        unitScript.SetupHealthBar();

        gameObject.tag = "Player";
        GameObject playerCamera = GameObject.FindWithTag("MainCamera");
        if (playerCamera != null)
        {
            CameraFollow2D script = playerCamera.GetComponent<CameraFollow2D>();
            script.target = transform;
        }
    }

    private void Update()
    {

        if (unitScript.animator == null) { return; }

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
        Vector2 direction = UtilService.instance.GetMousePosition2D();
        float moveX = direction.x - transform.position.x;
        float moveY = direction.y - transform.position.y;

        Vector3 difference = new Vector3(moveX, moveY, 0).normalized;

        unitScript.animator.SetFloat("moveX", difference.x);
        unitScript.animator.SetFloat("moveY", difference.y);
        unitScript.animator.SetFloat("lastMoveX", moveX);
        unitScript.animator.SetFloat("lastMoveY", moveY);
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
            Vector2 direction = UtilService.instance.GetMousePosition2D();
            float moveX = direction.x - transform.position.x;
            float moveY = direction.y - transform.position.y;

            Vector3 difference = new Vector3(moveX, moveY, 0).normalized;

            unitScript.animator.SetFloat("moveX", difference.x);
            unitScript.animator.SetFloat("moveY", difference.y);
            unitScript.animator.SetFloat("lastMoveX", moveX);
            unitScript.animator.SetFloat("lastMoveY", moveY);
            transform.position += unitScript.moveSpeed * Time.deltaTime * difference;
        }
        else
        {
            unitScript.animator.SetFloat("moveX", 0);
            unitScript.animator.SetFloat("moveY", 0);
        }

    }

    // protected void MovementOnArrowKeys()
    // {
    //     float moveX = Input.GetAxisRaw("Horizontal");   // Input = InputManager (edit -> project settings -> InputManager)
    //     float moveY = Input.GetAxisRaw("Vertical");
    //     unitScript.animator.SetFloat("moveX", moveX);
    //     unitScript.animator.SetFloat("moveY", moveY);

    //     if (moveX == 1 || moveX == -1 || moveY == 1 || moveY == -1)
    //     {
    //         unitScript.animator.SetFloat("lastMoveX", moveX);
    //         unitScript.animator.SetFloat("lastMoveY", moveY);

    //         // set new direction the character is facing for raycast stuff  
    //     }
    //     Vector3 difference = new Vector3(moveX, moveY, 0).normalized;
    //     transform.position += unitScript.moveSpeed * Time.deltaTime * difference;
    // }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactible))
        {
            interactible.Interact(gameObject);
        }

    }

}
