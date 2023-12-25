using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{

    protected Animator animator;
    protected string currentState;

    private Rigidbody2D rigidbody2D;
    protected bool isColliding;

    Queue<Vector2> ordersQueue = new Queue<Vector2>();

    private Coroutine moveCoroutine;
    private Vector3 targetPosition;
    public float moveSpeed = 4.0f; // Adjust this speed as needed
                                   // Start is called before the first frame update
    protected void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    // Update is called once per frame
    protected void Update()
    {

    }

    public void AttackAnimation()
    {
        animator.SetBool("isAttacking", true);
        // Trigger the animation
        animator.SetTrigger("attack");
    }

    // Function to handle animation events
    public void OnAnimationFinished()
    {
        // Actions to perform when the animation finishes
        Debug.Log("Animation Finished!");
    }

    // protected void ChangeAnimationState(string newState)
    // {
    //     // animator.
    //     if (currentState == newState)
    //     {
    //         return;
    //     }
    //     animator.Play(newState);
    //     currentState = newState;
    // }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
            // Check if collision occurs with a specific tag, e.g., "Obstacle"
            if (collision.gameObject.CompareTag("Wall"))
            {
                isColliding = true;
                // Additional logic or actions when colliding with an object tagged as "Obstacle"
                Debug.Log("Colliding with an obstacle!");
            }
        }
    }

    public void MoveToDestinationInSteps(HashSet<Vector2> path)
    {
        ordersQueue = new Queue<Vector2>(path);

        MoveToDestination(ordersQueue.ElementAt(0));

    }

    public void MoveToDestination(Vector2 destination)
    {
        if (moveCoroutine != null)
        {
            StopCoroutine(moveCoroutine);
        }
        targetPosition = destination;
        targetPosition.z = transform.position.z; // in case the destination is weird Z position from mouseclick

        moveCoroutine = StartCoroutine(MoveToTarget());
    }

    protected IEnumerator MoveToTarget()
    {
        float distance = Vector2.Distance(transform.position, targetPosition);

        while (distance > 0.1f) // Adjust threshold for considering arrival
        {
            // if (isColliding) break;
            Vector2 difference = targetPosition - transform.position;

            animator.SetFloat("moveX", difference.x);
            animator.SetFloat("moveY", difference.y);
            animator.SetFloat("lastMoveX", difference.x);
            animator.SetFloat("lastMoveY", difference.y);

            transform.position = Vector2.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
            distance = Vector2.Distance(transform.position, targetPosition);
            yield return null;
        }
        // arrived
        ordersQueue.Dequeue();  // remove first element of thne queue, because it is finished.
        if (ordersQueue.Count > 1)
        {
            MoveToDestination(ordersQueue.ElementAt(0));
        }

        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", 0);
    }
}
