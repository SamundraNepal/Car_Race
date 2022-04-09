using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MobileInput : MonoBehaviour
{
    public float MiddleOfTheScreen;

    public bool Accelerate;
    public bool Reserve;


    public GameObject A;
    public GameObject R;

    public bool True;




    private void Start()
    {

        MiddleOfTheScreen = (float)Screen.width / 2.0f + (float) Screen.height / 2.0f;


    }


    public void Update()
    {
        

        if(Input.touchCount == 1)
        {

            Touch T = Input.GetTouch(0);
            if (T.phase == TouchPhase.Stationary)
            {
                if (T.position.x < MiddleOfTheScreen)
                {

                    Accelerate = true;
                    Reserve = false;
                    if(True)
                    {
                    A.SetActive(false);
                    }


                }


            } else if(T.phase == TouchPhase.Ended)
            {
                Accelerate = false;


            }



            if (T.phase == TouchPhase.Stationary)
            {




                if (T.position.x > MiddleOfTheScreen)
                {
                    Accelerate = false;

                    Reserve = true;
                    if(True)
                    {
                    R.SetActive(false);
                    }


                }

            } else if(T.phase == TouchPhase.Ended)
            {

                Reserve = false;


            }

        }

    }

}
