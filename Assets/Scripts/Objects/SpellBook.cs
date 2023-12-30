using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class SpellBook : MonoBehaviour, IInteractable
{

    public List<GameObject> possibleSpellPrefabs;

    public GameObject deactivatedRostrumPrefab;

    public void Interact(GameObject player)
    {
        GameObject randomSpellPrefab = possibleSpellPrefabs.ElementAt(Random.Range(0, possibleSpellPrefabs.Count));
        PlayerStats.instance.AddSpell(randomSpellPrefab);

        if (deactivatedRostrumPrefab != null)
        {
            GameObject deactivatedRostrum = ObjectInstantiator.instance.InstantiateObject(deactivatedRostrumPrefab, new Vector2(transform.position.x, transform.position.y));
            // GameObject deactivatedRostrum = Instantiate(deactivatedRostrumPrefab, transform);
            Debug.Log(deactivatedRostrum);
            StartCoroutine(LogRostrum(deactivatedRostrum));
        }
        Destroy(gameObject);
    }
    private IEnumerator LogRostrum(GameObject ob)
    {
        yield return new WaitForSeconds(2f);
        Debug.Log(ob);
    }


}
