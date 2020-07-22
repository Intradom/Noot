using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior_Goal : MonoBehaviour
{
    [SerializeField] private string[] tag_trigger = null;
    [SerializeField] private Object ref_next_level = null;

    private void OnCollisionEnter2D(Collision2D collision)
    {
        bool trigger = false;
        foreach (string t in tag_trigger)
        {
            if (t == collision.collider.tag)
            {
                trigger = true;
            }
        }
        if (!trigger)
        {
            return;
        }

        if (ref_next_level == null)
        {
            int next_build_index = 0;

            next_build_index = SceneManager.GetActiveScene().buildIndex + 1;

            // Check for valid 
            if (next_build_index < SceneManager.sceneCountInBuildSettings)
            {
                Manager_Sounds.Instance.PlayGoal();
                SceneManager.LoadScene(next_build_index);
            }
        }
        else
        {
            Manager_Sounds.Instance.PlayGoal();
            SceneManager.LoadScene(ref_next_level.name);
        }   
    }
}
