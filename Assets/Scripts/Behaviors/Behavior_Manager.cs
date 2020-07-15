using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Manager : MonoBehaviour
{
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

        DontDestroyOnLoad(gameObject);
    }
}
