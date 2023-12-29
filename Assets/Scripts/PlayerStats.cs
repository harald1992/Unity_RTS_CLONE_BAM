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
    private Image manaBarAmountImage;

    [SerializeField]
    private TextMeshProUGUI attackTextUI;

    [SerializeField]
    private TextMeshProUGUI goldTextUI;

    public float maxHp = 25f;
    public float currentHp = 25f;

    public float maxMp = 10f;
    public float currentMp = 10f;

    public float attack = 2f;

    private int gold = 0;


    [SerializeField]
    private List<GameObject> spellContainers;



    public void UpdateUI()
    {
        attackTextUI.text = attack.ToString();

        healthBarAmountImage.fillAmount = currentHp / maxHp;
        manaBarAmountImage.fillAmount = currentMp / maxMp;

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

    public void ChangeMana(float amount)
    {
        currentMp += amount;
        UpdateUI();
    }



    public void CastSpell(int index)
    {
        ISpell spell = spellContainers.ElementAt(index).GetComponentInChildren<ISpell>();
        spell?.Cast();

    }
}
