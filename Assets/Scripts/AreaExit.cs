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
    // public bool playerEntered { get; set; } = false;


    // private float timer = 0f;
    // private float duration = 1f;

    private void Start()
    {
        areaEntrance.areaTransitionName = areaTransitionName;
        // Player.instance.RegisterWayPoint(gameObject);
    }

    // private void Update()
    // {

    //     if (playerEntered == true)
    //     {


    //         timer += Time.deltaTime; // Increment timer using Time.deltaTime

    //         if (timer >= duration)
    //         {
    //             // Code when the timer reaches the duration
    //             playerEntered = false;
    //             // Reset the timer and perform any necessary actions
    //             ResetTimer();
    //         }
    //     }

    // }


    // void ResetTimer()
    // {
    //     timer = 0f;
    //     // You can also reset any other variables or perform actions related to the reset here
    // }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player"))
        {
            SceneManager.LoadScene(sceneToLoad);
            Player.instance.areaTransitionName = areaTransitionName;
            // playerEntered = true;
            // LoadSceneAndPositionPlayer();
        }
    }

    private void LoadSceneAndPositionPlayer()
    {
        PlayerPrefs.SetString("WaypointName", gameObject.name);

        // Load the new scene
        SceneManager.LoadScene(sceneToLoad);


    }
}
