using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CamTrigger : MonoBehaviour
{

    public GameObject MainCamera;
    public GameObject ActionCamera;
    public GameObject SecondCamera;

     public float MaxTime;

    public Rigidbody Player;



    public void OnTriggerEnter(Collider other)
    {
        
        if(other.gameObject.tag == "CamTrigger")
        {
            if (Player.velocity.magnitude >= 20f)
            {



                MainCamera.SetActive(false);
                ActionCamera.SetActive(true);
                StartCoroutine(callback());
            }

        }


        if (other.gameObject.tag == "CamTriggerSecond")
        {
            if (Player.velocity.magnitude >= 20f)
            {



                MainCamera.SetActive(false);
                SecondCamera.SetActive(true);
                StartCoroutine(callback());
            }

        }

    }


    IEnumerator callback()
    {


        yield return new WaitForSeconds(MaxTime);
        MainCamera.SetActive(true);
        ActionCamera.SetActive(false);
        SecondCamera.SetActive(false);



    }



}
