using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Util_Scale_Lock : MonoBehaviour
{
    [SerializeField] private bool lock_x = true;
    [SerializeField] private bool lock_y = true;
    [SerializeField] private bool lock_z = true;

    private float initial_scale_x = 0f;
    private float initial_scale_y = 0f;
    private float initial_scale_z = 0f;

    private void Start()
    {
        initial_scale_x = transform.localScale.x;
        initial_scale_y = transform.localScale.y;
        initial_scale_z = transform.localScale.z;
    }

    private void Update()
    {
        transform.localScale = new Vector3((lock_x ? initial_scale_x : transform.localScale.x), 
            (lock_y ? initial_scale_y : transform.localScale.y), (lock_z ? initial_scale_z : transform.localScale.z));
    }
}
