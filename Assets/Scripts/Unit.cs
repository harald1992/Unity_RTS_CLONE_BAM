using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;


public class Unit : MonoBehaviour
{

    [SerializeField]
    protected float animationTimeAttack;

    [SerializeField]
    public float moveSpeed; // Adjust this speed as needed

    public Animator animator;
    protected string currentState;

    private Rigidbody2D rigidbody2D;
    protected bool isColliding;

    Queue<Vector2> ordersQueue = new Queue<Vector2>();

    private Coroutine currentActionCoroutine;
    private Vector3 targetPosition;


    // Start is called before the first frame update
    public void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
    }

    public void StopActionCoroutine()
    {
        if (currentActionCoroutine != null)
        {
            StopCoroutine(currentActionCoroutine);
        }
    }

    public void Attack()
    {
        AttackAnimation();
    }



    public void AttackAnimation()
    {
        StopActionCoroutine();
        animator.SetBool("isAttacking", true);

        currentActionCoroutine = StartCoroutine(TransitionToIdleAfterTime(animationTimeAttack));

    }
    IEnumerator TransitionToIdleAfterTime(float time)
    {
        yield return new WaitForSeconds(time);
        DamageEnemy();

        animator.SetBool("isAttacking", false);
    }

    private void DamageEnemy()
    {
        float size = 0.5f;
        Vector2 origin = new Vector2(
     transform.position.x + animator.GetFloat("lastMoveX") * size,
      transform.position.y + animator.GetFloat("lastMoveY") * size);

        Vector2 direction = new Vector2(
         transform.position.x + animator.GetFloat("lastMoveX") * 1.1f * size,
          transform.position.y + animator.GetFloat("lastMoveY") * 1.1f * size);


        RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, new Vector2(0.5f, 0.5f), 0f, direction, 0.5f);

        Color color = Color.red; // Define the color of the line

        Debug.DrawLine(origin, direction, color);

        foreach (var hit in hits)
        {
            if (hit.collider != null)
            {
                Debug.Log("Hit Object: " + hit.collider.gameObject.name);
                if (hit.collider.gameObject.CompareTag("Enemy"))
                {
                    ObjectInstantiator.instance.SpawnFloatingTextAt("20", hit.collider.gameObject.transform.position);
                    Destroy(hit.collider.gameObject);
                }
            }
        }
    }



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
        StopActionCoroutine();
        targetPosition = destination;
        targetPosition.z = transform.position.z; // in case the destination is weird Z position from mouseclick

        currentActionCoroutine = StartCoroutine(MoveToTarget());
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
