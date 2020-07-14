using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Experimental.U2D.Animation;

public class Controller_Player : MonoBehaviour
{
    // References
    [SerializeField] private LayerMask mask_ground = 0;
    [SerializeField] private Transform ref_ground_check = null;
    [SerializeField] private Rigidbody2D ref_rbody = null;
    [SerializeField] private Animator ref_animator = null;
    [SerializeField] private SpriteRenderer ref_renderer_body = null;
    [SerializeField] private SpriteResolver ref_resolver_head = null;

    // Parameters
    [SerializeField] private float initial_speed = 0;
    [SerializeField] private float jump_velocity = 0;
    [SerializeField] private float jump_reset_lock = 0;
    [SerializeField] private bool noot = true; // noot is black form, nert is white form

    /*******************************/

    // Inputs
    private float input_horizontal = 0;
    private float input_vertical = 0;

    private float ground_check_rad = 0;
    private int jump_frame_counter = 0;
    private bool vertical_lock = false;
    private bool facing_right = true;
    private bool jumping = false;

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
        // Variable updates
        ++jump_frame_counter;

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
        if (jumping && jump_frame_counter >= jump_reset_lock && Physics2D.OverlapCircle((Vector2)ref_ground_check.position, ground_check_rad, mask_ground))
        {
            jumping = false;
        }

        // Jumping, can't handle jumping in FixedUpdate, need to catch all frames due to acting on key down
        if (!jumping && input_vertical > 0)
        {
            ref_rbody.AddForce(new Vector2(0, jump_velocity), ForceMode2D.Force);
            jumping = true;
            jump_frame_counter = 0;
        }

        // Form
        if (Input.GetButtonDown("Swap"))
        {
            noot = !noot;

            if (noot) // black form
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