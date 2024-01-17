using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy3D : MonoBehaviour
{

    public Transform target;
    public Animator animator;

    // private bool isAttacking = false;

    void Start()
    {
        animator = GetComponentInChildren<Animator>();

        SetTarget();
    }

    private void SetTarget()
    {
        if (target == null)
        {
            // target = FindObjectOfType<Player>()?.transform; // Attempt to find a new target
        }
    }

    private bool IsAttacking()
    {
        return animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1");
    }


    // Update is called once per frame
    void Update()
    {


        if (target == null)
        {
            SetTarget();
            return;
        }

        if (animator.IsInTransition(0) == true)
        {
            // return;
        }

        // if (isAttacking == true && animator.GetCurrentAnimatorStateInfo(0).IsName("Attack1h1") == false)
        // {
        // isAttacking = false;
        // Debug.Log("Do damage");

        // }

        else
        {




            Vector3 direction = target.position - transform.position;
            // transform.up = direction;

            if (direction.magnitude <= 1 && IsAttacking() == false)
            {
                // isAttacking = true;
                animator.SetBool("Attack1h1", true);
                Debug.Log("Set attack1h1 bool");
            }
            else if (direction.magnitude > 4)
            {
                animator.SetFloat("speedh", 100);
                // animator.SetFloat("speedv" , 2);
            }
        }
    }
}
