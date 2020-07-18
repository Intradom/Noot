using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior_Manager : MonoBehaviour
{
    //[SerializeField] private Object scene_ending = null;

    public static Behavior_Manager Instance = null;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else if (Instance != null)
        {
            Destroy(gameObject);
        }

        /*
        Debug.Log(SceneManager.GetActiveScene().name + " : " + scene_ending.name);
        if (SceneManager.GetActiveScene().name == scene_ending.name)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject);
        }
        */
        DontDestroyOnLoad(gameObject);
    }
}
