using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LapSystem : MonoBehaviour
{
    public GameObject MainCamera;
    public GameObject WCamera;
    public CarController CC;
    public GameObject Fire;

    public LapManager Lm;
    public CarAI CarAI;
    public CarRecover CR;

    public int EnemyLapComplete;
    public int PlayerLapComplete;


    public int NumberOfFirstE;
    public int NumberOfFirstP;
    public bool First;
    public bool Second;
    public bool Third;


    public GameObject Winning;
    public GameObject GameOver;

    private void Start()
    {

        First = false;
        Second = false;
        Third = false;
        Winning.SetActive(false);
        GameOver.SetActive(false);
    }



    private void Update()
    {
        
        if(Lm.LapComplete)
        {

            if(NumberOfFirstP >= 2)
            {
                MainCamera.SetActive(false);
                WCamera.SetActive(true);
                Fire.SetActive(true);
                CC.enabled = false;
                

                Winning.SetActive(true);
                Debug.Log("Player Win");
            }  else if(NumberOfFirstE >= 2)
            {
                Debug.Log("Enemy Win");
                GameOver.SetActive(true);


            }

        }

    }
    private void OnTriggerEnter(Collider other)
    {
        

        if(other.gameObject.tag == "Lap 1")
        {
            if(CR.CheckPoint)
            {
            Lm.FirstLap = true;

            }



        } else

        if (other.gameObject.tag == "Lap 2")
        {
            if(CR.CheckPoint)
            {
            Lm.SecondLap = true;

            }


        }

        else

        if (other.gameObject.tag == "Lap 3")
        {
            if(CR.CheckPoint)
            {

            Lm.ThirdLap = true;
            }


        }






    }




    private void OnTriggerExit(Collider other)
    {

        if (other.gameObject.tag == "Lap 1")
        {
            if(CR.CheckPoint)
            {
            other.gameObject.tag = "Lap 2";

            }
            CR.CheckPoint = false;


        } else  if (other.gameObject.tag == "Lap 2")
        {
            if (CR.CheckPoint)
            {
            other.gameObject.tag = "Lap 3";
                if (!First)
                {
                    NumberOfFirstP += 1;
                    First = true;
                }
            }
            CR.CheckPoint = false;



        } else if(other.gameObject.tag == "Lap 3")
        {
            if (CR.CheckPoint)

            {
            other.gameObject.tag = "Lap 4";

                if (!Second)
                {
                    NumberOfFirstP += 1;
                    Second = true;

                }
            }
            CR.CheckPoint = false;


         
        } else if(other.gameObject.tag == "Lap 4")
        {


            if (CR.CheckPoint)

            {

            Lm.LapComplete = true;

                if (!Third)
                {
                    NumberOfFirstP += 1;
                    Third = true;

                }
            }
            CR.CheckPoint = false;

        }





        if (other.gameObject.tag == "Enemy Lap 1")
        {


            other.gameObject.tag = "Enemy Lap 2";
        }
        else

        if (other.gameObject.tag == "Enemy Lap 2")
        {
            if (!First)
            {
                NumberOfFirstE += 1;
                First = true;
            }

            other.gameObject.tag = "Enemy Lap 3";


        } else if (other.gameObject.tag == "Enemy Lap 3")
        {
            if (!Second)
            {
                NumberOfFirstE += 1;

                Second = true;
            }

            other.gameObject.tag = "Enemy Lap 4";

        

        } else if(other.gameObject.tag == "Enemy Lap 4")
        {


            if (!Third)
            {
                NumberOfFirstE += 1;

                Third = true;
            }

            CarAI.LapCompleted = true;


        }

    }
}
