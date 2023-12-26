using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    public GameObject goldObject;
    private Text goldTextUI;

    public GameObject healthBarObject;
    private Image healthBarAmount;

    private int gold = 0;

    public float maxHp = 25f;
    public float currentHp = 25f;

    public float attack = 2;
    // public float defence = 2f;

    public void UpdateUI()
    {
        goldTextUI.text = "Gold: " + gold.ToString();
        healthBarAmount.fillAmount = currentHp / maxHp;
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

        // Transform goldObject = gameObject.transform.Find("Gold");

        goldTextUI = goldObject.gameObject.GetComponent<Text>();


        healthBarAmount = healthBarObject.GetComponent<Image>();


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

