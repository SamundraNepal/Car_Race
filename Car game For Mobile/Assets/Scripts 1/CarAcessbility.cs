using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class CarAcessbility : MonoBehaviour
{
    public GameObject Car;
    public float RotateSpeed;
    public int Select;


    public Button[] B;

    public bool Mobile;




    public string SceneName;

    [Header("Colors")]

    public Material Spoliers;
    public Material Body;
    public Material Rims;
    public Material Wheels;
    public Material Glass;
    public Material BackLight;

    public Color BodyC;
    public Color RimsC;
    public Color WHeelsC;
    public Color GlassC;
    public Color BackLightC;
    public Color SpoliersC;


    [Header("Shines")]

    [Range( 0f, 1f)]
    public float ShineNess;




    public bool BodyS;
    public bool SpolersS;
    public bool RimS;
    public bool WheelsS;
    public bool GlassS;
    public bool BackLightS;


    public float MiddleOfTheScreen;

    public bool Accelerate;
    public bool Reserve;




 

    private void Start()
    {

        MiddleOfTheScreen = (float)Screen.width / 2.0f + (float)Screen.height / 2.0f;

        foreach (var Buttons in B)
        {


            Color go = Buttons.GetComponent<Image>().color;


           Buttons.onClick.AddListener(() => OnClick(go));


        }

      
    }
    private void Update()
    {

        if(Mobile)
        {

        MobileTest();
        }


        if (!Mobile)
        {


            float h = Input.GetAxisRaw("Horizontal");

            Car.transform.Rotate(0f, h * RotateSpeed * Time.deltaTime, 0f);
        }


     /*   Body.color = BodyC;
        Rims.color = RimsC;
        Wheels.color = WHeelsC;
        Glass.color = GlassC;
        BackLight.color = BackLightC;
        Spoliers.color = SpoliersC;*/


    

    }

   
  



    public void OnClick(Color C)
    {

        if(BodyS)
        {

            Body.color = C;
       


        }
        
        if(SpolersS)
        {


            Spoliers.color = C;


        }



        if (RimS)
        {



            Rims.color = C;

       

        }


        if(WheelsS)
        {




       
            Wheels.color = C;


           

        }
        
        if(GlassS)
        {


            Glass.color = C ;


         

        }


        if(BackLightS)
        {

        
            BackLight.color = C;
           

        }
            
           

    }




    public void Bo()
    {
        BodyS = true;


        SpolersS = false;
        RimS = false;
        WheelsS = false;
        GlassS = false;
        BackLightS = false;

    }


    public void S()
    {

        SpolersS = true;

        BodyS = false;


        RimS = false;
        WheelsS = false;
        GlassS = false;
        BackLightS = false;

    }


   public void R()
    {
        RimS = true;



        SpolersS = false;
        BodyS = false;


        WheelsS = false;
        GlassS = false;
        BackLightS = false;


    }

    public void W()
    {
        WheelsS = true;


        RimS = false;



        SpolersS = false;
        BodyS = false;


        GlassS = false;
        BackLightS = false;

    }



    public void G()
    {
        GlassS = true;

        WheelsS = false;


        RimS = false;



        SpolersS = false;
        BodyS = false;


        BackLightS = false;
    }


    public void BL()
    {


        BackLightS = true;


        GlassS = false;

        WheelsS = false;


        RimS = false;



        SpolersS = false;
        BodyS = false;



    }



    public void LoadScene()
    {


        SceneManager.LoadScene(SceneName);

    }


    public void MobileTest()
    {



        if (Input.touchCount == 1)
        {

            Touch T = Input.GetTouch(0);
            if (T.phase == TouchPhase.Stationary)
            {
                if (T.position.x < MiddleOfTheScreen)
                {



                    Accelerate = true;
                    Reserve = false;
                    Car.transform.Rotate(0f,  -RotateSpeed * Time.deltaTime, 0f);



                }


            }
            else if (T.phase == TouchPhase.Ended)
            {
                Accelerate = false;


            }



            if (T.phase == TouchPhase.Stationary)
            {




                if (T.position.x > MiddleOfTheScreen)
                {
                    Accelerate = false;

                    Car.transform.Rotate(0f, RotateSpeed * Time.deltaTime, 0f);

                    Reserve = true;


                }

            }
            else if (T.phase == TouchPhase.Ended)
            {

                Reserve = false;


            }

        }
    }

    }










