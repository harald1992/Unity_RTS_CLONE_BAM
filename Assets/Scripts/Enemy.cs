using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public Transform target;
    private Rigidbody2D rigidbody2D;
    private Animator animator;

    public float moveSpeed = 4;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2D = GetComponent<Rigidbody2D>();
        animator = gameObject.GetComponent<Animator>();
        // gameObject.tag = "Enemy";
        // Debug.Log("set tag???");
        // Debug.Log(gameObject.tag);
        SetTarget();
    }

    private void SetTarget()
    {
        if (Player.instance)
        {
            target = Player.instance.transform;
            // target = FindObjectOfType<Player>().transform;
        }
    }

    void Update()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            Vector3 diff = target.position - gameObject.transform.position;
            if (diff.magnitude < 3)
            {
                Vector3 difference = (target.position - gameObject.transform.position).normalized;

                transform.position += moveSpeed * Time.deltaTime * difference;

                animator.SetFloat("moveX", difference.x);
                animator.SetFloat("moveY", difference.y);
                animator.SetFloat("lastMoveX", difference.x);
                animator.SetFloat("lastMoveY", difference.y);
            }
            else
            {
                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", 0);
            }
        }

    }
}
