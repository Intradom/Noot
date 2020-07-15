using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Effect_Spinning : MonoBehaviour
{
    [SerializeField] private float spin_speed = 0f;

    private void FixedUpdate()
    {
        transform.Rotate(new Vector3(0f, 0f, spin_speed * Time.fixedDeltaTime));
    }
}
