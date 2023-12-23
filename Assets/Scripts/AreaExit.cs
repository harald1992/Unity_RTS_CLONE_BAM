using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaExit : MonoBehaviour
{
    public string sceneToLoad; // Name of the scene to load
                               // public Transform waypoint; // Reference to the waypoint object
                               // public string wayPointDestination;

    public string areaTransitionName;   // exit just used
    public AreaEntrance areaEntrance;


    private void Start()
    {
        areaEntrance.areaTransitionName = areaTransitionName;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
            Player.instance.areaTransitionName = areaTransitionName;
        }
    }
}
