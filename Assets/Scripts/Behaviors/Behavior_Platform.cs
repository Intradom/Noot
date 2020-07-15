using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Behavior_Platform : MonoBehaviour
{
    [SerializeField] private string search_tag = "";

    [SerializeField] private float scale_deadzone = 0f; // Any scale less than this is set to 0
    [SerializeField] private bool is_nootform = true;

    private Controller_Player script_controller = null;
    private Collider2D[] ref_children_colliders = null;
    private float original_scale_x = 0f;
    private float original_scale_y = 0f;
    private bool colliders_enabled = true;

    private void Awake()
    {
        script_controller = GameObject.FindGameObjectWithTag(search_tag).GetComponent<Controller_Player>();
        ref_children_colliders = this.GetComponentsInChildren<Collider2D>();
    }

    private void Start()
    {
        original_scale_x = transform.localScale.x;
        original_scale_y = transform.localScale.y;

        float noot_level = script_controller.GetNoot();
        SetActive(noot_level);
    }

    private void Update()
    {
        float noot_level = script_controller.GetNoot();
        SetActive(noot_level);
    }

    private void SetActive(float noot_level)
    {
        float multiplier = 0f;
        if ((is_nootform && noot_level <= 0.5f) || (!is_nootform && noot_level > 0.5f))
        {
            multiplier = Mathf.Abs((noot_level - 0.5f) * 2f); // Scales it between 0 and 1
            multiplier = (multiplier <= scale_deadzone ? 0f : multiplier);
        }
        transform.localScale = new Vector2(multiplier * original_scale_x, multiplier * original_scale_y);

        bool turn_on_colliders = multiplier > 0f;
        if (colliders_enabled != turn_on_colliders)
        {
            foreach (Collider2D c in ref_children_colliders)
            {
                c.enabled = turn_on_colliders;
            }
            colliders_enabled = turn_on_colliders;
        }
    }
}
