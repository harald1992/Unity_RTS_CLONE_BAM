using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour, ISpell
{

    public void Cast()
    {
        Debug.Log("Casting");
        Player.instance.unitScript.ChangeHealth(5f);
    }


    void Start()
    {

    }

    void Update()
    {

    }


}
