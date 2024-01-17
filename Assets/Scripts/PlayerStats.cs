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

    // public float maxHp = 25f;
    // public float currentHp = 25f;

    // public float maxMp = 10f;
    // public float currentMp = 10f;

    // public float attack = 2f;

    private int gold = 0;


    [SerializeField]
    private List<GameObject> spellContainers;

    [SerializeField]
    private List<GameObject> spellPrefabs = new();


    private void UpdateUI()
    {
        if (Player.instance == null)
        {
            Debug.Log("No Player yet");
            return;
        }
        Unit value = Player.instance.unitScript;

        attackTextUI.text = value.attack.ToString();

        healthBarAmountImage.fillAmount = value.currentHp / value.maxHp;
        manaBarAmountImage.fillAmount = value.currentMp / value.maxMp;

        goldTextUI.text = gold.ToString();

        if (value.currentHp <= 0)
        {
            ObjectInstantiator.instance.InstantiateFloatingTextAt("GAME OVER", Player.instance.gameObject.transform.position, Color.white);
        }
        // attackTextUI.text = attack.ToString();

        // healthBarAmountImage.fillAmount = currentHp / maxHp;
        // manaBarAmountImage.fillAmount = currentMp / maxMp;

        // goldTextUI.text = gold.ToString();

    }

    private void UpdateSpellContainers()
    {
        // destroy all SpellPrefabs in spellContainers
        for (int i = 0; i < spellContainers.Count; i++)
        {
            // destroy the SpellPrefab
            for (int j = 0; j < transform.childCount; j++)
            {
                Transform child = transform.GetChild(j);
                if (child != null && child.CompareTag("SpellPrefab"))
                {
                    Debug.Log("Destroy gameobject spellprefab");
                    Destroy(child.gameObject);
                }
            }

            // Instantiate the SpellPrefab if it has a spell
            if (i < spellPrefabs.Count)
            {
                GameObject spellPrefab = spellPrefabs.ElementAt(i);
                if (spellPrefab != null)
                {
                    // GameObject prefab = Instantiate(spellPrefab);
                    Transform parentTransform = spellContainers.ElementAt(i).transform;
                    Instantiate(spellPrefab, parentTransform.position, Quaternion.identity, parentTransform);
                }
            }
        }
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

        GameEvents.instance.onPlayerChanged += UpdateUI;    // subscribe to onPlayerChanged

        UpdateUI();
        UpdateSpellContainers();
    }

    private void OnDisable()    // also called when gameobject is destroyed
    {
        GameEvents.instance.onPlayerChanged -= UpdateUI; // unsubscribe
    }

    public void IncreaseGold(int goldAmount)
    {
        gold += goldAmount;
        UpdateUI();
    }

    // public void ChangeHealth(float amount)
    // {
    //     currentHp += amount;
    //     UpdateUI();
    //     if (currentHp <= 0)
    //     {
    //         ObjectInstantiator.instance.InstantiateFloatingTextAt("GAME OVER", Player.instance.gameObject.transform.position, Color.white);
    //     }
    // }

    // public void ChangeMana(float amount)
    // {
    //     currentMp += amount;
    //     UpdateUI();
    // }


    // public void ChangeAttack(float amount)
    // {
    //     attack += amount;
    //     UpdateUI();
    // }

    public void CastSpell(int index)
    {
        ISpell spell = spellContainers.ElementAt(index).GetComponentInChildren<ISpell>();
        spell?.Cast();
    }

    public void AddSpell(GameObject spellPrefab)
    {
        bool isNewSpell = true;

        foreach (var spell in spellPrefabs)
        {
            if (spell.name == spellPrefab.name)
            {
                isNewSpell = false;
                // TODO: upgrade the spell?
                return;
            }

        }
        if (isNewSpell == true)
        {
            spellPrefabs.Add(spellPrefab);
            UpdateSpellContainers();
        }

    }
}
