using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Unit : MonoBehaviour
{
    [SerializeField]
    protected float animationTimeAttack;

    public float maxHp;

    public float currentHp;

    public float attack;

    // TODO: Set attack and movespeed dynamically;
    // public float attackSpeed;
    // animator.speed = 0.1f;
    public float moveSpeed; // Adjust this speed as needed


    private Transform currentHpBar;


    public Animator animator;
    protected string currentState;

    private Rigidbody2D rigidbody2D;
    protected bool isColliding;

    public Vector3 direction;
    Queue<Vector2> ordersQueue = new Queue<Vector2>();

    private Coroutine currentActionCoroutine;
    private Vector3 targetPosition;

    public void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        currentHp = maxHp;
    }

    // private void Update()
    // {
    //     DamageEnemy();
    // }

    public void ReceiveDamage(float damage)
    {
        currentHp -= damage;
        currentHp = Mathf.Clamp(currentHp, 0f, maxHp); // Ensure health doesn't go below 0 or exceed maxHp
        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }

        float percentage = currentHp / maxHp;
        currentHpBar.localScale = new Vector3(percentage, 1, 1);
    }
    public void SetupHealthBar()
    {
        GameObject healthBarPrefab = Resources.Load<GameObject>("Prefabs/UI/HealthBar");
        GameObject healthOb = Instantiate(healthBarPrefab, gameObject.transform);
        healthOb.transform.localPosition = new Vector3(0, 0.3f, 0);
        currentHpBar = healthOb.transform.Find("CurrentHealthBar");
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

        if (animator.GetBool("isAttacking") == true)
        {
            // if already attacking, don't attack again.
            return;
        }
        animator.SetBool("isAttacking", true);

        StopActionCoroutine();
        currentActionCoroutine = StartCoroutine(DamageAndTransitionToIdle(animationTimeAttack));
    }

    IEnumerator DamageAndTransitionToIdle(float time)
    {
        yield return new WaitForSeconds(time);
        DamageEnemy();

        animator.SetBool("isAttacking", false);
    }

    public void RayCast()
    {
        float size = 0.5f;

        Vector3 direction = new Vector3(
               animator.GetFloat("lastMoveX"),
               animator.GetFloat("lastMoveY"),
                 0).normalized * size;

        Ray ray = new Ray(transform.position, direction);
        Debug.DrawRay(ray.origin, ray.direction, Color.white);
    }

    public void DamageEnemy()
    {
        float size = 0.5f;

        Vector3 direction = new Vector3(
        animator.GetFloat("lastMoveX"),
        animator.GetFloat("lastMoveY"),
          0).normalized * size;

        RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, size);
        // RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, new Vector2(0.1f, 0.1f), 0f, direction, 0.5f);
        HashSet<Collider2D> uniqueColliders = new HashSet<Collider2D>();    // boxCastAll can hit the same object twice, maybe because I have circle and box colliders on the units

        foreach (var hit in hits)
        {
            if (hit.collider != null && !uniqueColliders.Contains(hit.collider))
            {
                uniqueColliders.Add(hit.collider);
                GameObject ob = hit.collider.gameObject;
                if (gameObject.CompareTag("Player") && ob.CompareTag("Enemy"))
                {
                    ObjectInstantiator.instance.InstantiateFloatingTextAt(attack.ToString(), ob.transform.position);
                    ob.GetComponent<Unit>().ReceiveDamage(attack);
                }
                else if (gameObject.CompareTag("Enemy") && ob.CompareTag("Player"))
                {
                    ObjectInstantiator.instance.InstantiateFloatingTextAt(attack.ToString(), ob.transform.position);
                    ob.GetComponent<Unit>().ReceiveDamage(attack);
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
                // Debug.Log("Colliding with an obstacle!");
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
