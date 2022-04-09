using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class MEnu : MonoBehaviour
{


    [Header("Values")]
    public float InterValue;
    public float DivideValue;
    public float MultiplyRate;
    public float TotalCombinations;
    public float  Round;

    public string LevelName;
    public GameObject Trasition;

    public string WinLevel;



    private void Start()
    {

       TotalCombinations = InterValue / DivideValue -MultiplyRate;
       Round = Mathf.Round(TotalCombinations);


    }
    public void Play()
    {
        Time.timeScale = 1;
        Trasition.SetActive(true);

        StartCoroutine(StartGame());
    }

   public void Quit() {


        Application.Quit();
    
    }



   
    public void WinLevelName()
    {

        PlayerPrefs.SetInt("LevelReached", 2);
        Trasition.SetActive(true);
        Time.timeScale = 1f;
        StartCoroutine(NextLEvel());
    }







    IEnumerator NextLEvel()
    {


        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(WinLevel);

    }




    IEnumerator StartGame()
    {


        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene(LevelName);

    }

}
