using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior_Death_Zone : MonoBehaviour
{
    [SerializeField] private Object scene_final = null;
    [SerializeField] private Object scene_ending = null;
    [SerializeField] private string tag_to_disable = null;
    [SerializeField] private string[] tag_reset = null;
    [SerializeField] private string[] tag_destroy = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (scene_final != null)
        {
            if (SceneManager.GetActiveScene().name == scene_final.name)
            {
                foreach (GameObject go in GameObject.FindGameObjectsWithTag(tag_to_disable))
                {
                    go.SetActive(false);
                }

                // Different behavior for final scene
                SceneManager.LoadScene(scene_ending.name);
            }
        }

        foreach(string tag in tag_reset)
        {
            if (collision.tag == tag)
            {
                Manager_Sounds.Instance.PlayHurt();
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
            }
        }

        foreach (string tag in tag_destroy)
        {
            if (collision.tag == tag)
            {
                Destroy(collision.gameObject);
            }
        }
    }
}
