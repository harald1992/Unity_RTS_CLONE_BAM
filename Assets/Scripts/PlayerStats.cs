using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;
using Unity.VisualScripting;

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

    public void ReceiveDamage(float damage)
    {
        currentHp -= damage;
        UpdateUI();
        if (currentHp <= 0)
        {
            ObjectInstantiator.instance.InstantiateFloatingTextAt("GAME OVER", Player.instance.gameObject.transform.position, Color.white);
        }
    }

}

