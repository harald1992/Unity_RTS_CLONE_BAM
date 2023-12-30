using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, ISpell
{

    [SerializeField]
    private GameObject spellEffectPrefab;

    private readonly float cost = 3f;

    public void Cast()
    {
        if (Player.instance.unitScript.currentMp < cost)
        {
            return;
        }

        Player.instance.unitScript.ChangeMana(-cost);
        Player.instance.unitScript.ChangeHealth(5f);

        GameObject ob = Instantiate(spellEffectPrefab, Player.instance.transform);
        ob.transform.localPosition = new Vector3(0, -0.5f, 0);   // so it starts on the feet

        // StartCoroutine(DestroyInTwoSeconds(ob));
    }

    private IEnumerator DestroyInTwoSeconds(GameObject particleObject)
    {
        yield return new WaitForSeconds(2.0F);
        if (gameObject != null && particleObject != null)
        {
            Destroy(particleObject);
        }

    }


}
