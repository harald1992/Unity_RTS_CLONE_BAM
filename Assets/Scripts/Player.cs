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
            CameraFollow2D script = cameraPivot.GetComponent<CameraFollow2D>();
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
        Vector3 mouseDirection_animator = MousePosition.instance.GetMousePositionFromUnrotatedCamera();
        Vector3 difference_animator = (mouseDirection_animator - Camera.main.transform.position).normalized;
        unitScript.animator.SetFloat("lastMoveX", difference_animator.x);
        unitScript.animator.SetFloat("lastMoveY", difference_animator.y);
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
            Vector3 mouseDirection_animator = MousePosition.instance.GetMousePositionFromUnrotatedCamera();
            Vector3 difference_animator = (mouseDirection_animator - Camera.main.transform.position).normalized;
            unitScript.animator.SetFloat("lastMoveX", difference_animator.x);
            unitScript.animator.SetFloat("lastMoveY", difference_animator.y);
            unitScript.animator.SetFloat("moveX", difference_animator.x);
            unitScript.animator.SetFloat("moveY", difference_animator.y);

            Vector3 mousePosition = MousePosition.instance.GetMousePositionFromRotatedCamera();
            Vector3 difference = mousePosition - transform.position;
            transform.position += unitScript.moveSpeed * Time.deltaTime * difference.normalized;
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
