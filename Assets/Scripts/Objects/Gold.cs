using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Gold : MonoBehaviour, IInteractable
{
    private int goldAmount;
    public int goldModifier = 1;
    private Animator animator;

    public void Interact(GameObject ob)
    {
        PlayerStats.instance.IncreaseGold(goldAmount);
        ObjectInstantiator.instance.InstantiateFloatingTextAt("+" + goldAmount.ToString(), gameObject.transform.position, Color.yellow);
        Destroy(gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        int level = 1;
        goldAmount = 2 * level + Random.Range(level, level * goldModifier);
        animator = GetComponent<Animator>();
        animator.speed = 0.5f;  // because to lazy to change the animations;
    }

}
