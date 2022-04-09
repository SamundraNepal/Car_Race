using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraLook : MonoBehaviour
{


    public Transform player;





    private void Update()
    {

        Vector3 Rot = player.position - transform.position;

        Rot.y = 0f;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(Rot), 1f);

    }

}
