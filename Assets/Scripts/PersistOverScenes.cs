using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PersistOverScenes : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        DontDestroyOnLoad(gameObject);  // dont destroy object when changing scene
    }

    // Update is called once per frame
    void Update()
    {

    }
}
