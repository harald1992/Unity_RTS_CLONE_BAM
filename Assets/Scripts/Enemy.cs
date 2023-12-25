using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Unit
{
    public Transform target;
    // private Rigidbody2D rigidbody2D;
    // private Animator animator;


    // Start is called before the first frame update
    protected new void Start()
    {
        base.Start();
        // rigidbody2D = GetComponent<Rigidbody2D>();
        // animator = gameObject.GetComponent<Animator>();

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

    protected new void Update()
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
