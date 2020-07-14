using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Camera_Follow : MonoBehaviour
{
    [SerializeField] private GameObject ref_to_follow = null;

    private void Update()
    {
        float targ_x = ref_to_follow.transform.position.x;
        float targ_y = ref_to_follow.transform.position.y;

        transform.position = new Vector3(targ_x, targ_y, transform.position.z);
    }
}
