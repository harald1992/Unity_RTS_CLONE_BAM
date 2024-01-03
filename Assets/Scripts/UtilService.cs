
using UnityEngine;

public class UtilService : MonoBehaviour
{
    public static UtilService instance;
    // Start is called before the first frame update
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
    }

    void Update()
    { }


}
