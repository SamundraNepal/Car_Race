using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class LapManager : MonoBehaviour
{



    


    public string RetryLevel;
    public string LevelName;

    public GameObject PauseMenu;

    [Header("Count Down")]
    public GameObject Green;
    public GameObject Yellow;
    public GameObject Red;



    public float CountDownTimer;
    public float CountDownSpeed;

    public bool StartLap;



    public bool FirstLap;
    public bool SecondLap;
    public bool ThirdLap;
    public bool LapComplete;

    public int MiniSec;
    public int Second;
    public int Minutes;



    bool Once;
    bool Once2;

    [Header("Lap 1")]
    public TextMeshProUGUI Sec;
    public TextMeshProUGUI Min;
    [Header("Lap 2")]
    public TextMeshProUGUI SecLap2;
    public TextMeshProUGUI MinLap2;

    [Header("Lap 3")]
    public TextMeshProUGUI SecLap3;
    public TextMeshProUGUI MinLap3;




    public GameObject One;
    public GameObject Two;
    public GameObject Three;


    private void Start()
    {


  


        One.SetActive(false);
        Two.SetActive(false);
        Three.SetActive(false);



        Green.SetActive(false);
        Yellow.SetActive(false);
        Red.SetActive(false);

        StartLap = false;

        Once = true;
        Once2 = true;

    }




   

    private void Update()
    {
      /*  if(Input.GetKeyDown(KeyCode.Escape))
        {
            Time.timeScale = 0f;
            PauseMenu.SetActive(true);

        }*/

  

        CountDownTimer += CountDownSpeed * Time.deltaTime;

        if (CountDownTimer >= 5f)
        {

            Red.SetActive(true);


        }  if (CountDownTimer >= 8f)
        {


            Yellow.SetActive(true);

        }  if (CountDownTimer > 12f)
        {

            Green.SetActive(true);
            StartLap = true;
           

        }




        if (Time.timeScale != 0)
        {



            if (StartLap)
            {

                if (LapComplete == false)
                {



                    if (FirstLap)
                    {
                        One.SetActive(true);

                        Sec.text = Second.ToString("00");
                        Min.text = Minutes.ToString("00");

                        if (MiniSec <= 60)
                        {
                            MiniSec += 01;

                        }


                        if (MiniSec == 60)
                        {
                            MiniSec = 0;
                            Second += 01;

                        }


                        if (Second == 60)
                        {


                            Second = 0;
                            Minutes += 1;
                        }

                    }


                    if (SecondLap)
                    {
                        Two.SetActive(true);


                        if (Once)
                        {


                            MiniSec = 0;
                            Second = 0;
                            Minutes = 0;
                            Once = false;
                        }

                        FirstLap = false;
                        SecLap2.text = Second.ToString("00");
                        MinLap2.text = Minutes.ToString("00");

                        if (MiniSec <= 60)
                        {
                            MiniSec += 01;

                        }


                        if (MiniSec == 60)
                        {
                            MiniSec = 0;
                            Second += 01;

                        }


                        if (Second == 60)
                        {


                            Second = 0;
                            Minutes += 1;
                        }
                    }


                    if (ThirdLap)
                    {
                        Three.SetActive(true);

                        if (Once2)
                        {


                            MiniSec = 0;
                            Second = 0;
                            Minutes = 0;
                            Once2 = false;
                        }

                        SecondLap = false;

                        SecLap3.text = Second.ToString("00");
                        MinLap3.text = Minutes.ToString("00");

                        if (MiniSec <= 60)
                        {
                            MiniSec += 01;

                        }


                        if (MiniSec == 60)
                        {
                            MiniSec = 0;
                            Second += 01;

                        }


                        if (Second == 60)
                        {


                            Second = 0;
                            Minutes += 1;
                        }

                    }



                }
            }
        }
    }



    public void Retry()
    {

        SceneManager.LoadScene(RetryLevel);
        PauseMenu.SetActive(false);
        Time.timeScale = 1f;


    }


    public void Resume()
    {

        Time.timeScale = 1f;
        PauseMenu.SetActive(false);

    }



    public void Menu()
    {


        Time.timeScale = 1f;
        SceneManager.LoadScene(LevelName);

    }



    public void PauseFOrPhone()
    {

        Time.timeScale = 0f;
        PauseMenu.SetActive(true);

    }

}
