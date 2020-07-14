using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Behavior_Death_Zone : MonoBehaviour
{
    [SerializeField] private string[] tag_reset = null;
    [SerializeField] private string[] tag_destroy = null;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        foreach(string tag in tag_reset)
        {
            if (collision.tag == tag)
            {
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
