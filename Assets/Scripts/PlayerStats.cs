using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;
using System.Linq;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    [SerializeField]
    private Image healthBarAmountImage;

    [SerializeField]
    private TextMeshProUGUI attackTextUI;

    [SerializeField]
    private TextMeshProUGUI goldTextUI;

    public float maxHp = 25f;
    public float currentHp = 25f;

    public float attack = 2f;

    private int gold = 0;


    [SerializeField]
    private List<GameObject> spellContainers;



    public void UpdateUI()
    {
        attackTextUI.text = attack.ToString();

        healthBarAmountImage.fillAmount = currentHp / maxHp;
        goldTextUI.text = gold.ToString();
    }

    void Start()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

        UpdateUI();
    }

    public void IncreaseGold(int goldAmount)
    {
        gold += goldAmount;
        UpdateUI();
    }

    public void ChangeHealth(float amount)
    {
        currentHp += amount;
        UpdateUI();
        if (currentHp <= 0)
        {
            ObjectInstantiator.instance.InstantiateFloatingTextAt("GAME OVER", Player.instance.gameObject.transform.position, Color.white);
        }
    }


    public void CastSpell(int index)
    {
        ISpell spell = spellContainers.ElementAt(index).GetComponentInChildren<ISpell>();

        if (spell != null)
        {
            Debug.Log(spell);
            spell.Cast();
        }



    }


    public void FindChildByTag(string tag)
    {
        Transform child = FindChildWithTag(transform, tag);

        if (child != null)
        {
            Debug.Log("Found child with the specified tag: " + child.name);
            // Access the child object or its components
        }
        else
        {
            Debug.Log("Child with the specified tag not found.");
        }
    }

    // Custom method to find child by tag using LINQ
    Transform FindChildWithTag(Transform parent, string tag)
    {
        return parent.Cast<Transform>().FirstOrDefault(child => child.CompareTag(tag));
    }
}
