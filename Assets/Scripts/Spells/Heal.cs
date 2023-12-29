using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, ISpell
{

    [SerializeField]
    private GameObject spellAnimationPrefab;

    private readonly float cost = 3f;

    public void Cast()
    {
        if (PlayerStats.instance.currentMp < cost)
        {
            return;
        }

        Player.instance.unitScript.ChangeMana(-cost);
        Player.instance.unitScript.ChangeHealth(5f);

        GameObject ob = Instantiate(spellAnimationPrefab, Player.instance.transform);
        ob.transform.localPosition = new Vector3(0, -0.5f, 0);   // so it starts on the feet
    }



    void Update()
    {

    }


}
