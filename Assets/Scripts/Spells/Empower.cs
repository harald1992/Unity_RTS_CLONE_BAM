using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Empower : MonoBehaviour, ISpell
{
    private readonly float cost = 5f;

    [SerializeField]
    private GameObject spellEffectPrefab;

    public void Cast()
    {
        if (Player.instance.unitScript.currentMp < cost)
        {
            return;
        }

        Player.instance.unitScript.ChangeMana(-cost);

        Player.instance.gameObject.transform.localScale += new Vector3(0.5F, 0.5F, 0);
        Player.instance.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 0.7f, 0.7f);
        Player.instance.unitScript.ChangeAttack(2);

        GameObject ob = Instantiate(spellEffectPrefab, Player.instance.transform);
        ob.transform.localPosition = new Vector3(0, -0.5f, 0);   // so it starts on the feet

    }

}
