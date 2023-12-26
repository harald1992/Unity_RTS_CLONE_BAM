using UnityEngine;

public class Player : MonoBehaviour
{
    private Unit unitScript;
    private Rigidbody2D rigidbody2D;

    public static Player instance;
    public string areaTransitionName;   // exit just used changed by AreaExit&AreaEntrance scripts

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
        // unitScript.RayCast();

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
            unitScript.animator.SetFloat("lastMoveX", moveX);
            unitScript.animator.SetFloat("lastMoveY", moveY);

            // set new direction the character is facing for raycast stuff  
            Vector3 diff = new Vector3(moveX, moveY, 0).normalized;
        }
        Vector3 difference = new Vector3(moveX, moveY, 0).normalized;
        transform.position += unitScript.moveSpeed * Time.deltaTime * difference;
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.TryGetComponent<IInteractable>(out var interactible))
        {
            Debug.Log("interact");
            interactible.Interact(gameObject);
        }

    }

}
