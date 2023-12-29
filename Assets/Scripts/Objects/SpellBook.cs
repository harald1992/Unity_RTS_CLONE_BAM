using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellBook : MonoBehaviour, IInteractable
{

    public List<GameObject> possibleSpellPrefabs;

    public void Interact(GameObject player)
    {
        GameObject randomSpellPrefab = possibleSpellPrefabs.ElementAt(Random.Range(0, possibleSpellPrefabs.Count));
        PlayerStats.instance.AddSpell(randomSpellPrefab);
    }

}
