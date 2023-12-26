using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerStats : MonoBehaviour
{
    public static PlayerStats instance;

    Text goldTextUI;
    private int gold = 0;

    public float maxHp = 25f;
    public float currentHp = 25f;

    public float attack = 2;
    // public float defence = 2f;


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

        Transform goldObject = gameObject.transform.Find("Gold");
        if (goldObject != null)
        {
            goldTextUI = goldObject.gameObject.GetComponent<Text>();
        }
    }

    public void IncreaseGold(int goldAmount)
    {
        gold += goldAmount;
        goldTextUI.text = "Gold: " + gold.ToString();
    }

}

