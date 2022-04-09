using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarRecover : MonoBehaviour
{


    public GroundCheck GC;

    public bool CheckPoint;
    public float Timer;


    private void OnTriggerStay(Collider other)
    {


        if (other.gameObject.tag == "Water")
        {


            Timer += 2f * Time.deltaTime;

            if (Timer > 5f)

            {

                 transform.position = new Vector3(GC.GetComponent<GroundCheck>().LastGroundCheck.x, GC.GetComponent<GroundCheck>().LastGroundCheck.y + 1f , GC.GetComponent<GroundCheck>().LastGroundCheck.z);
                transform.rotation = GC.GetComponent<GroundCheck>().Rotaion;

                    Timer = 0f;


            }


        }

    }



    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.tag == "CheckPoint")
        {

            CheckPoint = true;


        }

    }





}
