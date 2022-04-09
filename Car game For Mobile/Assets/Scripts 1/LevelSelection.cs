using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class LevelSelection : MonoBehaviour
{

    public GameObject FADE;


    public Button[] LevelButtons;


    public string Lev1;
    public string Lev2;



    private void Start()
    {


        int LevelReached = PlayerPrefs.GetInt("LevelReached", 1);

    
        for (int i = 0; i < LevelButtons.Length; i++)
        {

            if (i + 1 > LevelReached)
            {
            LevelButtons[i].interactable = false;

            }





        }


    }




    public void Level1()
    {

        FADE.SetActive(true);
        StartCoroutine(One());
    }



    public void Level2()
    {
        FADE.SetActive(true);
        StartCoroutine(Two());



    }


    IEnumerator One()
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(Lev1);
    }

    IEnumerator Two()
    {

        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene(Lev2);
    }


}
