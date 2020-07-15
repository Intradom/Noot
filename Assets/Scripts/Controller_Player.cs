using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.Experimental.U2D.Animation;

public class Controller_Player : MonoBehaviour
{
    // References
    [SerializeField] private LayerMask mask_ground = 0;
    [SerializeField] private Transform ref_ground_check = null;
    [SerializeField] private Rigidbody2D ref_rbody = null;
    [SerializeField] private Animator ref_animator = null;
    [SerializeField] private SpriteRenderer ref_renderer_head = null;
    [SerializeField] private SpriteRenderer ref_renderer_body = null;
    [SerializeField] private SpriteRenderer ref_renderer_face = null;

    // Parameters
    [SerializeField] private float initial_speed = 0f;
    [SerializeField] private float jump_velocity = 0f;
    [SerializeField] private float noot_level = 0f; // 0 is black form, 1 is white form
    [SerializeField] private float noot_speed = 0f;

    /*******************************/

    // Inputs
    private float input_horizontal = 0f;
    private float input_vertical = 0f;

    private float ground_check_rad = 0f;
    private bool vertical_lock = false;
    private bool facing_right = true;

    private float noot_multiplier = -1f;

    public float GetNoot()
    {
        return noot_level;
    }

    private void FlipSprite()
    {
        transform.Rotate(0f, 180f, 0f);
        facing_right = !facing_right;
    }

    private void Start()
    {
        CircleCollider2D ref_gcheck_collider = ref_ground_check.GetComponent<CircleCollider2D>();
        ground_check_rad = ref_gcheck_collider.radius;
    }

    private void Update()
    {
        // Check inputs
        input_horizontal = Input.GetAxis("Horizontal");
        float input_vertical_raw = Input.GetAxisRaw("Vertical");
        input_vertical = vertical_lock ? 0 : input_vertical_raw;
        if (vertical_lock && input_vertical_raw == 0)
        {
            vertical_lock = false;
        }
        else if (!vertical_lock && input_vertical_raw != 0)
        {
            vertical_lock = true;
        }

        if ((input_horizontal < 0 && facing_right) || (input_horizontal > 0 && !facing_right))
        {
            FlipSprite();
        }

        // Check ground collision
        bool grounded = Physics2D.OverlapCircle((Vector2)ref_ground_check.position, ground_check_rad, mask_ground);
        ref_animator.SetBool("grounded", grounded);

        // Jumping, can't handle jumping in FixedUpdate, need to catch all frames due to acting on key down
        if (input_vertical > 0 && grounded)
        {
            ref_rbody.AddForce(new Vector2(0, jump_velocity), ForceMode2D.Force);
        }

        // Form update
        if (Input.GetButton("Swap"))
        {
            if (Input.GetButtonDown("Swap"))
            {
                noot_multiplier *= -1;
            }
            Color c = new Color(noot_level, noot_level, noot_level);
            ref_renderer_head.color = c;
            ref_renderer_body.color = c;
            ref_renderer_face.color = new Color(1f - noot_level, 1f - noot_level, 1f - noot_level);

            noot_level += noot_multiplier * noot_speed;
            if (noot_level < 0f)
            {
                noot_level = 0f;
            }
            else if (noot_level > 1f)
            {
                noot_level = 1f;
            }
            
            /*
            if (noot_level <= 0.5) // black form
            {
                ref_renderer_body.color = Color.black;
                ref_resolver_head.SetCategoryAndLabel("head", "head_noot");
            }
            else // white form
            {
                ref_renderer_body.color = Color.white;
                ref_resolver_head.SetCategoryAndLabel("head", "head_nert");
            }
            ref_resolver_head.ResolveSpriteToSpriteRenderer();
            */
        }
    }

    private void FixedUpdate()
    {
        // Horizontal
        float move_hori = initial_speed * Time.fixedDeltaTime * input_horizontal;

        ref_rbody.velocity = new Vector2(move_hori, ref_rbody.velocity.y);
        ref_animator.SetFloat("abs_speed_x", Mathf.Abs(move_hori));
    }
}