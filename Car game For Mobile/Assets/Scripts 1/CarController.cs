using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CarController : MonoBehaviour
{

    public float MaxAcc;
    public float MinAcc;

    public float Acc;
    public float PhysicSpeed;
    public LapManager LM;



    [Header("Car Light")]
    public Material BackLight;
    public Color color;
    public float Value;

    [Header("Car Sounnd")]
    public AudioSource Source;
    public AudioSource GearSource;
    public AudioSource SkiddingSource;
    public AudioClip CarIdle;
    public AudioClip CarMoving;
    public AudioClip Skidding;
    public AudioClip GearClip;
    public float Pitch;
    public float MaxPitch;


    [Header("Car UI")]
    public TextMeshProUGUI SpeedText;



    public Vector3 CenterOfMass;


    [Header("Effects")]
    public GameObject SlideEffect;
    public Transform[] SlideParticle;
    public GameObject Trail;




    public Transform[] Wheels;

    public WheelSuspension[] RareWheels;


    public Transform[] FrontWheels;



    [Header("Car Movement")]
    public float BreakSpeed;
    public float Acceleration;
    public float ReserveAcceleration;
    public float TrunSpeed;
    public float Speed;
    public float ReserveSpeed;

    [Header("Car Movement")]
    public int Gears;
    public float LowSpeed;
    public float RPM;
    public int MaxSpeed, MinSpeed;
    public float WheelsRPM;
    public float MinRpm, MaxRpm;

    public float Drift;

   public float Vertical;
   public float Horizontal;

    float SteerAngle;
   public float WheelAngle;


    public bool Automatic;
   public Rigidbody rb;



    [Header("Mobile Input")]
    public MobileInput MI;
    public bool MobileInput;
    public float GyroMeter;
    public float GyroSPeed;
    public float TurningSpeed;


    public Vector3 TrackstionForce;
    private void Start()
    {
        MI = GetComponent<MobileInput>();
        Source = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody>();
    }

    private void Update()
    {
        if (LM.StartLap == true)
        {

            ChangeLight();

            CarSound();



            


            Vertical = Input.GetAxisRaw("Vertical");
            Horizontal = Input.GetAxisRaw("Horizontal");


            RPM = Mathf.Clamp(RPM, MinRpm, MaxRpm);
            Speed = Mathf.Clamp(Speed, MinSpeed, MaxSpeed);
            ReserveSpeed = Mathf.Clamp(ReserveSpeed, 0f, 20f);
            WheelAngle = Mathf.Lerp(WheelAngle, SteerAngle, 5f * Time.deltaTime);
            Gears = Mathf.Clamp(Gears, 0, 6);
            foreach (var W in FrontWheels)
            {

                W.transform.localRotation = Quaternion.Euler(Vector3.up * WheelAngle);

            }


            GyroMeter = Input.acceleration.x * TurningSpeed;

            GyroSPeed = (0.1f * GyroMeter) + (0.9f * GyroSPeed);

           GyroMeter = Mathf.Clamp(GyroMeter, -2f, 2f);

        }
    }


    private void FixedUpdate()
    {

        if (LM.StartLap)
        {

            if(Vertical != -1 && MI.Reserve == false)
            {
            Speed = rb.velocity.magnitude;

            }
     



            if(rb.velocity.magnitude < 1f)
            {

                Source.clip = CarIdle;
                if(!Source.isPlaying)
                {

                    Source.Play();
                }

            } else if(rb.velocity.magnitude > 1f)
            {



                Source.clip = CarMoving;
                if (!Source.isPlaying)
                {

                    Source.Play();
                }

            }


            if(rb.velocity.magnitude < 15f)
            {

                Acc = MaxAcc;

            } else
            {

                Acc = MinAcc;
            }

            SpeedText.text = Mathf.RoundToInt(rb.velocity.magnitude).ToString();




            CarMOvement();

            rb.velocity = Vector3.ClampMagnitude(rb.velocity, MaxSpeed);

        }
    }



    public void CarMOvement()
    {
        WheelRotation();
        GearsChange();

        foreach (var R in RareWheels)
        {

            if (Horizontal > 0)
            {
                SteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.26f / (10f + (1.25f / 2))) * Horizontal;

            }
            else if (Horizontal < 0)
            {
                SteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.26f / (10f - (1.25f / 2))) * Horizontal;

            }
            else
            {
                SteerAngle = 0f;
            }

            if (R.IsGrounded && R.RareWheels)
            {
                if (MobileInput)
                {


                    if (MI.Accelerate)
                    {

                        color = Color.red;
                        Value = 0.5f;
                        ReserveSpeed -= BreakSpeed * Time.deltaTime;
                        Speed += Acceleration * Time.deltaTime;
                        R.rb.AddForce( R.transform.forward * Speed * Acc, ForceMode.Impulse);


                    } else
               

                    if (MI.Reserve)
                    {

                        Speed -= BreakSpeed * Time.deltaTime;



                        if (Speed != 0)
                        {
                            color = Color.red;
                            Value = 2f;
                        }


                        if (Speed <= 0 && ReserveSpeed > 0)
                        {


                            color = Color.white;
                            Value = 2f;
                        }


                        ReserveSpeed += ReserveAcceleration * Time.deltaTime;

                        R.rb.AddForce( -R.transform.forward * ReserveSpeed * Acc, ForceMode.Impulse);

                    }
                  

                    if (GyroMeter > 0.4f)
                    {
                        SteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.26f / (10f + (1.25f / 2))) * GyroMeter;

                    }
                    else if (GyroMeter < -0.4f)
                    {
                        SteerAngle = Mathf.Rad2Deg * Mathf.Atan(2.26f / (10f - (1.25f / 2))) * GyroMeter;

                    } else
                    {

                        GyroMeter = 0f;

                    }


                    if (rb.velocity.magnitude >= 1f)
                    {


                      float tf = Mathf.Lerp(0, TrunSpeed, rb.velocity.magnitude / 1);

                 //       R.rb.angularVelocity = Vector3.up * GyroMeter  * tf


                        float Trailing = GyroMeter * tf * Time.deltaTime;
                        transform.Rotate(0f, Trailing, 0f);





                        if (GyroMeter > 0.4f || GyroMeter < -0.4f)
                        {

                            foreach (var P in SlideParticle)

                            {


                                if (rb.velocity.magnitude > 15f)
                                {


                                    if (!SkiddingSource.isPlaying)
                                    {

                                        SkiddingSource.Play();
                                    }




                                    GameObject T = Instantiate(Trail, P.transform.position, Quaternion.identity);
                                    T.transform.parent = P.transform;

                                    if (R.IsGrounded == false)
                                    {
                                        T.transform.parent = null;
                                    }



                                    Destroy(T.gameObject, 2f);



                                    GameObject g = Instantiate(SlideEffect, P.transform.position, P.transform.rotation);
                                    Destroy(g, 1f);
                                }


                            }
                        } else
                        {
                            if (rb.velocity.magnitude < 8f)
                            {

                                if (SkiddingSource.isPlaying)
                                {

                                    SkiddingSource.Stop();
                                }
                            }




                            if (SkiddingSource.isPlaying)
                            {

                                SkiddingSource.Stop();
                            }


                        }





                    }
                }



                if (!MobileInput)
                {

                    if (Vertical > 0)
                    {
                        ReserveSpeed -= BreakSpeed * Time.deltaTime;
                         Speed += Acceleration  * Time.deltaTime;
                       R.rb.AddForce(Vertical * R.transform.forward * Speed * Acc, ForceMode.Impulse);

                     
                        color = Color.red;
                        Value = 0.5f;

                        


                    }
                    else if (Vertical < 0)
                    {

                       
                        Speed -= BreakSpeed * Time.deltaTime;

                   
                        if(Speed != 0 )
                        {
                            color = Color.red;
                            Value = 2f;
                        } 
                        
                        
                        if(Speed <= 0 && ReserveSpeed > 0)
                        {


                            color = Color.white;
                            Value = 2f;
                        }

                             

                        ReserveSpeed += ReserveAcceleration  * Time.deltaTime;

                        R.rb.AddForce(Vertical * R.transform.forward * ReserveSpeed *Acc, ForceMode.Impulse);



                    }
                    
                  



                    if (rb.velocity.magnitude > 1f)
                    {


                      float    tf  = Mathf.Lerp(0f, TrunSpeed, rb.velocity.magnitude / 1f);

                            /* R.rb.angularVelocity =Vector3.up * Horizontal * tf;
                        */


                       float   Trailing = Horizontal  * tf * Time.deltaTime;
                        transform.Rotate(0f, Trailing, 0f);






                        if (Horizontal > 0 ||  Horizontal < 0 )
                        {
                            foreach (var P in SlideParticle)

                            {

                              
                                if(rb.velocity.magnitude > 15f)
                                {


                                    if(!SkiddingSource.isPlaying)
                                    {

                                        SkiddingSource.Play();
                                    }


                               
                                  
                                    GameObject T = Instantiate(Trail, P.transform.position, Quaternion.identity);
                                    T.transform.parent = P.transform;

                                    if (!R.IsGrounded)
                                    {
                                        T.transform.parent = null;
                                    }


                                    Destroy(T.gameObject, 2f);
                                  
                                     

                                    GameObject g = Instantiate(SlideEffect, P.transform.position, P.transform.rotation);
                                    Destroy(g, 1f);
                                } 
                               


                            }


                        } 
                       
                        else
                        {

                            if (rb.velocity.magnitude < 8f)
                            {

                                if (SkiddingSource.isPlaying)
                                {

                                    SkiddingSource.Stop();
                                }
                            }




                            if (SkiddingSource.isPlaying)
                            {

                                SkiddingSource.Stop();
                            }


                          
                        }

                    }



                }
            }
        }

    }
     
    
    
    
    
        void WheelRotation()
            {
                WheelsRPM = rb.velocity.magnitude;

                foreach (var W in Wheels)
                {


                    W.transform.Rotate(rb.velocity.magnitude, 0f,0f);



                }

            }
        



        void GearsChange()
        {
            RPM = WheelsRPM * Speed * 10f;

            if (!Automatic)
            {


                if (Input.GetKeyDown(KeyCode.LeftShift))
                {
                    Gears++;
                }
                if (Input.GetKeyDown(KeyCode.LeftControl))
                {
                    Gears--;
                }
            } else
            {


             if (rb.velocity.magnitude >= MaxSpeed)
                {




                  /* GearSource.clip = GearClip;
               

                    GearSource.Play();*/

                
                    Gears += 1;
                }

          


            }

            if(Gears == 0)
        {

            MinSpeed = 0;
            MaxSpeed = 0;
        }
         else  if (Gears == 1)
            {

                MinSpeed = 0;
                MaxSpeed = 20;
            

        } else if (Gears == 2)
            {

                MinSpeed = 0;
                MaxSpeed = 40;

                LowSpeed = 20;
            
            if (Speed < LowSpeed)
            {

                Gears -= 1;

            }

        } else if (Gears == 3)
            {

                MinSpeed = 0;
              LowSpeed = 20;
                MaxSpeed = 50;
            if (Speed < LowSpeed)
            {

                Gears -= 1;

            }

        }
        else if (Gears == 4)
            {

            MinSpeed = 0;
            LowSpeed = 50;
                MaxSpeed = 60;
            if (Speed < LowSpeed)
            {

                Gears -= 1;

            }


        }
            else if (Gears == 5)
            {
                MinSpeed = 0;
            LowSpeed = 60;
                MaxSpeed = 65;
            if (Speed < LowSpeed)
            {

                Gears -= 1;

            }


        }
            else if (Gears == 6)
            {

                MinSpeed = 0;
                LowSpeed = 65;
                MaxSpeed = 70;
            if(Speed  < LowSpeed)
            {

                Gears -= 1;

            }

            }

        }


        public void CarSound()
        {
        if (!Source.isPlaying)
        {


            Source.clip = CarMoving;
            Source.Play();
        }
        MaxPitch = MaxSpeed;

        Pitch = rb.velocity.magnitude / MaxPitch;
        Pitch = Mathf.Clamp(Pitch, 0f, 1f);


        Source.pitch = Pitch;




    }






    void ChangeLight()
    {
        BackLight.SetFloat("Power", Value);
        BackLight.SetColor("_Color", color);
        BackLight.SetVector("_Color", color * Value);

      
      


    }





  







    /* public void Tractions()
     {

         foreach (var Track in RareWheels)
         {


                 TrackstionForce = Track.transform.InverseTransformDirection(rb.GetPointVelocity(Track.hit.point));

                 if (Track.IsGrounded)
                 {

                     Track.rb.AddForceAtPosition(new Vector3(TrackstionForce.x , 0f , 0f),  -Track.transform.right * Force);
                 }
                 else
                 {
                     Track.rb.AddForceAtPosition(new Vector3(TrackstionForce.x, 0f, 0f), -Track.transform.right * Force);
                 }







         }




     }*/

}
