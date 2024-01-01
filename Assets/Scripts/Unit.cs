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

    public float maxMp;

    public float currentMp;

    public float attack;

    // TODO: Set attack and movespeed dynamically;
    // public float attackSpeed;
    // animator.speed = 0.1f;
    public float moveSpeed; // Adjust this speed as needed


    private Transform currentHpBar;


    public Animator animator;
    protected string currentState;

    protected bool isColliding;

    public Vector3 direction;
    Queue<Vector2> ordersQueue = new Queue<Vector2>();

    private Coroutine currentActionCoroutine;
    private Vector3 targetPosition;



    public void Start()
    {
        animator = gameObject.GetComponent<Animator>();
        currentHp = maxHp;
    }

    private void Update()
    {
        RayCast();
        CastCone();
        ChangeHealth(0.2f * Time.deltaTime);
        ChangeMana(0.2f * Time.deltaTime);
    }

    public void ChangeHealth(float amount)
    {
        currentHp += amount;
        currentHp = Mathf.Clamp(currentHp, 0f, maxHp); // Ensure health doesn't go below 0 or exceed maxHp
        float percentage = currentHp / maxHp;
        currentHpBar.localScale = new Vector3(percentage, 1, 1);
        if (gameObject.CompareTag("Player"))
            GameEvents.instance.PlayerChanged();

        if (currentHp <= 0)
        {
            Destroy(gameObject);
        }
    }


    public void ChangeMana(float amount)
    {
        currentMp += amount;
        currentMp = Mathf.Clamp(currentMp, 0f, maxMp); // Ensure health doesn't go below 0 or exceed maxHp
        // float percentage = currentMp / maxMp;
        // currentHpBar.localScale = new Vector3(percentage, 1, 1);
        if (gameObject.CompareTag("Player"))
            // {
            //     PlayerStats.instance.ChangeMana(amount);
            // }
            GameEvents.instance.PlayerChanged();


    }

    public void ChangeAttack(float amount)
    {
        attack += amount;
        if (gameObject.CompareTag("Player"))
        {
            // PlayerStats.instance.ChangeAttack(amount);
            GameEvents.instance.PlayerChanged();

        }
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
        animator.SetFloat("moveX", 0);
        animator.SetFloat("moveY", 0);
    }


    void CastCone()
    {
        // float radius = 1f;
        // float maxDistance = 5f;
        // int rayCount = 10;
        // // float angleStep = 360f / rayCount;
        float angleStep = 45f;
        for (int i = -1; i <= 1; i++)
        {
            Vector2 direction = Quaternion.Euler(0f, 0f, i * angleStep) * Vector2.down;
            Debug.Log(direction);
            // RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, radius, direction, maxDistance);
            RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, 1f);
            Debug.DrawLine(transform.position, new Vector2(transform.position.x + direction.x, transform.position.y + direction.y), Color.red, 0.1f);

            foreach (RaycastHit2D hit in hits)
            {
                Debug.Log("Hit object: " + hit.collider.gameObject.name);
                Debug.Log(hit.collider.gameObject.layer);
                Debug.DrawLine(transform.position, hit.point, Color.yellow, 0f);
            }
        }
    }

    private void RayCast()
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

        // RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, direction, size);
        RaycastHit2D[] hits = Physics2D.BoxCastAll(transform.position, new Vector2(0.35f, 0.35f), 0, direction, size);

        // RaycastHit2D[] hits = Physics2D.BoxCastAll(origin, new Vector2(0.1f, 0.1f), 0f, direction, 0.5f);
        HashSet<Collider2D> uniqueColliders = new HashSet<Collider2D>();    // boxCastAll can hit the same object twice, maybe because I have circle and box colliders on the units

        foreach (var hit in hits)
        {
            if (hit.collider != null && hit.collider is BoxCollider2D && !uniqueColliders.Contains(hit.collider))
            {
                uniqueColliders.Add(hit.collider);
                GameObject other = hit.collider.gameObject;
                if (gameObject.CompareTag("Player") && other.CompareTag("Enemy"))
                {
                    ObjectInstantiator.instance.InstantiateFloatingTextAt(attack.ToString(), other.transform.position, Color.yellow);
                    Unit unitScript = other.GetComponent<Unit>();
                    unitScript.ChangeHealth(-attack);
                }
                else if (gameObject.CompareTag("Enemy") && other.CompareTag("Player"))
                {
                    ObjectInstantiator.instance.InstantiateFloatingTextAt(attack.ToString(), other.transform.position, Color.red);
                    Unit unitScript = other.GetComponent<Unit>();
                    unitScript.ChangeHealth(-attack);
                }
            }
        }
    }

    void OnCollisionStay2D(Collision2D collision)
    {
        foreach (ContactPoint2D contact in collision.contacts)
        {
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
