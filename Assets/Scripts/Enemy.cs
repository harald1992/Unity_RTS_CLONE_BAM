using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    private Unit unitScript;

    public Transform target;

    protected void Start()
    {
        unitScript = gameObject.GetComponent<Unit>();
        gameObject.tag = "Enemy";
        unitScript.SetupHealthBar();

        SetTarget();
    }

    private void SetTarget()
    {
        if (target == null)
        {
            target = FindObjectOfType<Player>()?.transform; // Attempt to find a new target
        }
    }

    protected void Update()
    {
        if (target == null)
        {
            SetTarget();
        }
        else
        {
            Vector3 diff = target.position - gameObject.transform.position;
            if (diff.magnitude < 1f)
            {
                unitScript.Attack();
            }
            else if (diff.magnitude < 3)
            {
                Vector3 difference = (target.position - gameObject.transform.position).normalized;

                transform.position += unitScript.moveSpeed * Time.deltaTime * difference;

                unitScript.animator.SetFloat("moveX", difference.x);
                unitScript.animator.SetFloat("moveY", difference.y);
                unitScript.animator.SetFloat("lastMoveX", difference.x);
                unitScript.animator.SetFloat("lastMoveY", difference.y);
            }
            else
            {
                unitScript.animator.SetFloat("moveX", 0);
                unitScript.animator.SetFloat("moveY", 0);
            }
        }

    }
}
