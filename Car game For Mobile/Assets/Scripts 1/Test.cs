using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{

    [SerializeField]

    public Transform[] Routs;

    public int IntRoute;

    public float Tpram;

    public Vector3 Carpo;


    public float speedModifier;

    public bool CoroutineAllowed;




    private void Start()
    {

        IntRoute = 0;
        Tpram = 0f;
        speedModifier = 0.5f;
        CoroutineAllowed = true;


        

    }


    private void Update()
    {
        
        if(CoroutineAllowed)
        {


            StartCoroutine(GobyRute(IntRoute));
        }

    }



    IEnumerator GobyRute( int RouteNumber)
    {

        CoroutineAllowed = true;
        Vector3 p0 = Routs[RouteNumber].position;
        Vector3 p1 = Routs[RouteNumber].position;
        Vector3 p2 = Routs[RouteNumber].position;
        Vector3 p3 = Routs[RouteNumber].position;



        while (Tpram < 1)
        {

            Tpram += Time.deltaTime * speedModifier;

            Carpo = Mathf.Pow(1 - Tpram, 3) * p0 + 3 * Mathf.Pow(1 - Tpram, 2) * Tpram * p1 + 3 * (1 - Tpram) * Mathf.Pow(Tpram, 2) * p2 + Mathf.Pow(Tpram, 3) * p3;

            transform.position = Vector3.Lerp(transform.position, Carpo, speedModifier * Time.deltaTime);

            yield return new WaitForEndOfFrame();


        }



        Tpram = 0f;
        IntRoute += 1;

        if(IntRoute > Routs.Length - 1)
            

            IntRoute = 0;
            CoroutineAllowed = true;

        


    }
}
