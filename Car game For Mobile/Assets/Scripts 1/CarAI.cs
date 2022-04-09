using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(LineRenderer))]
[RequireComponent(typeof(Rigidbody))]
public class CarAI : MonoBehaviour
{

    public enum CarBrain {Racing , hit}
    public CarBrain CarStates;

    public Transform GroundLine;
    public LayerMask GroundLayer;
    public Vector3 GroundPoint;

    public GameObject Trail;
    public GameObject Smokes;


    public float AngularSpeed;

    public Transform[] Wheels;


    public Rigidbody Rb;

    public LapManager LM;

    public float Speed;
    public float test;

    public float RotateSpeed;
    public float RotateDistance;


    public Transform[] Routes;
    public int CurrentLenght;
    public float NearDistance;


    public bool LapCompleted;

    public bool IsGrounded;
    public float TimerToReturn;
    public Quaternion Rot;

    private float ResetTimer;

    private void Start()
    {
        CarStates = CarBrain.Racing;

    }
    private void FixedUpdate()
    {

        if (LapCompleted == false)
        {


          //  AngularSpeed = Rb.angularVelocity.magnitude;



            if (AngularSpeed >= 0.2f)
            {

                foreach (var W in Wheels)
                {



                    GameObject S = Instantiate(Smokes, W.transform.position, Quaternion.identity);
                    Destroy(S, 1f);


                    GameObject T = Instantiate(Trail, W.transform.position, Quaternion.identity);

                    T.transform.parent = W.transform;
                    Destroy(T, 5f);



                }
            }

            if (LM.StartLap)
            {
               // if (CarStates == CarBrain.Racing)
               // {
                    RoateWheels();
                    Moving();
               // }
            }

        }

    }



    public void Update()
    {
       /* if(CarStates==CarBrain.hit)
        {
            if(ResetTimer < 3f)
            {
            ResetTimer += 0.1f * Time.deltaTime;

            }

            if (ResetTimer > 3f)
            {

                CarStates = CarBrain.Racing;
                ResetTimer = 0f;
            }



        }*/

        

        Ground();
        
    }


    public void RoateWheels()
    {



        foreach (var W in Wheels)
        {

            W.transform.Rotate(Speed, 0f, 0f);
        }
    }

    void Moving()
    {





        Vector3 direction = Routes[CurrentLenght].position - Rb.position;

        direction.Normalize();

        Vector3 RotateAmount = Vector3.Cross(direction, transform.forward);


        Rb.angularVelocity = -RotateAmount * RotateSpeed;

        Rb.velocity = (transform.forward * Speed);

      


        if (Vector3.Distance(Rb.position, Routes[CurrentLenght].position) < NearDistance)
        {

            if (CurrentLenght < Routes.Length)
            {

                CurrentLenght += 1;

            }


            if (CurrentLenght == Routes.Length)
            {




                CurrentLenght = 0;


            }

        }







    }




    public void Ground()
    {

        RaycastHit hit;

        if (Physics.Raycast(GroundLine.transform.position, -GroundLine.transform.up, out hit, 1f, GroundLayer))
        {

            Debug.DrawRay(GroundLine.transform.position, -GroundLine.transform.up * 1f, Color.red);

            Rot = transform.rotation;
            IsGrounded = true;
            if(Rb.velocity.magnitude < 5f)
            {

                TimerToReturn += 1f * Time.deltaTime;

                if (TimerToReturn >= 5f)
                {

                    transform.position = new Vector3(Routes[CurrentLenght].position.x, Routes[CurrentLenght].position.y + 0.5f, Routes[CurrentLenght].position.z);
                    transform.rotation = Rot;
                    TimerToReturn = 0f;

                }
            } else
            {
                TimerToReturn = 0f;

            }

        }
        else
        {
            Debug.DrawRay(GroundLine.transform.position, -GroundLine.transform.up * 1f, Color.white);

            IsGrounded = false;

            TimerToReturn += 1f * Time.deltaTime;

            if (TimerToReturn >= 5f)
            {

                transform.position = new Vector3(Routes[CurrentLenght].position.x, Routes[CurrentLenght].position.y + 0.5f, Routes[CurrentLenght].position.z);
                transform.rotation = Rot;
                TimerToReturn = 0f;

            }

        }

    }

    }








