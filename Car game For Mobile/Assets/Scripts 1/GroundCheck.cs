using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundCheck : MonoBehaviour
{

    public Transform[] SideRaycast;

    public AudioSource HitAudio;
    public AudioClip SideHit;
    public AudioClip Heavyhit;
    public AudioClip FallHit;
    public GameObject Sparks;

    public Transform[] Sides;
    public WheelSuspension[] WS;

    public CarController CC;

    public Transform DownCheck;
    public Transform UpCheck;
    public Vector3 LastGroundCheck;
    public Quaternion Rotaion;
    public LayerMask LM;
    public LayerMask SpeedMask;
    public LayerMask RecoverGround;
    public LayerMask SparkLayers;


    public bool Isgrounded;
    public bool SParksHIt;
    public float Acc;
    public float RCC;
    public float Drag;




    public float RecoverTimer;

    public LayerMask RecoverG;
   


    private void FixedUpdate()
    {

        WheelIsGrounded();
    }
    private void Update()
    {

        GroundCheckForRecover();

        if (Physics.Raycast(DownCheck.transform.position, -DownCheck.transform.up, 1f, SpeedMask))
        {

            if (!Isgrounded)
            {




                CC.Acceleration = 450f;
                CC.ReserveAcceleration = 450f;
                CC.rb.drag = 1f;
            }
            else if (Isgrounded)
            {
                CC.Acceleration = Acc;
                CC.ReserveAcceleration = RCC;
                CC.rb.drag = Drag;

            }

        } 
          





        



            RaycastHit hit;
        if (Physics.Raycast( DownCheck.transform.position, -DownCheck.transform.up, out hit,1f , LM ))
        {
            Debug.DrawRay(DownCheck.transform.position, -DownCheck.transform.up * 1f, Color.red);
        
            Isgrounded = true;
            if(SParksHIt )
            {

                SpringHIt();

            }



        } else
        {
            Debug.DrawRay(DownCheck.transform.position, -DownCheck.transform.up * 1f, Color.gray);


            Isgrounded = false;
            SParksHIt = true;
        }





        RaycastHit hit2;

        if (Physics.Raycast(UpCheck.transform.position, UpCheck.transform.up, out hit2, 1f, RecoverGround))
        {

            Debug.DrawRay(UpCheck.transform.position, UpCheck.transform.up * 1f, Color.red);


            HitAudio.clip = SideHit;
            if (!HitAudio.isPlaying)
            {
                HitAudio.Play();
            }

            Debug.Log("Flipped");

            GameObject Spark = Instantiate(Sparks, UpCheck.transform.transform.position, Quaternion.identity);
            Destroy(Spark, 0.3f);

            Recovercar();

          //  LastGroundCheck = hit2.point;

        } else
        {

            Debug.DrawRay(UpCheck.transform.position, UpCheck.transform.up * 1f, Color.grey);

        }





        foreach (var T in Sides)
        {


            RaycastHit hit3;

            if (Physics.Raycast(T.transform.position, T.transform.right, out hit2, 1f, RecoverGround))
            {


                HitAudio.clip = SideHit;
                if (!HitAudio.isPlaying)
                {
                    HitAudio.Play();
                }

                GameObject Spark = Instantiate(Sparks, T.transform.transform.position, Quaternion.identity);
                Destroy(Spark, 0.3f);


                Debug.DrawRay(T.transform.position, T.transform.right * 1f, Color.red);
                Recovercar();


            } else
            {

                Debug.DrawRay(T.transform.position, T.transform.right * 1f, Color.grey);
            }
             
        }



        foreach (var S in SideRaycast)
        {

            RaycastHit hit4;

            if (Physics.Raycast(S.transform.position, S.transform.forward, out hit4, 1f, SparkLayers))
            {


                HitAudio.clip = SideHit;
                if (!HitAudio.isPlaying)
                {
                    HitAudio.Play();
                }

                GameObject Spark = Instantiate(Sparks, S.transform.transform.position, Quaternion.identity);
                Destroy(Spark, 0.3f);




            }
           
        }



    }



    public void OnclickRecovercar()
    {
        

            transform.position = new Vector3(LastGroundCheck.x, LastGroundCheck.y + 2f, LastGroundCheck.z);
            transform.rotation = Rotaion;
        


    }


    public void Recovercar()
    {
        RecoverTimer += 0.1f;


        if (RecoverTimer > 10f)
        {

            RecoverTimer = 0f;
            transform.position = new Vector3(LastGroundCheck.x, LastGroundCheck.y + 2f, LastGroundCheck.z);
            transform.rotation = Rotaion;
        }

     
    }


    public void GroundCheckForRecover()

    {
        RaycastHit hit;

        if (Physics.Raycast(DownCheck.transform.position, -transform.transform.up, out hit,  1f, RecoverG))
        {


            LastGroundCheck = hit.point;
            Rotaion = transform.rotation;

        }


    }



    public void SpringHIt()
    {
        RaycastHit hit;



        if(Physics.Raycast(transform.position , -transform.up , out hit , 1f , SparkLayers))
        {


            HitAudio.clip = FallHit;
            if(!HitAudio.isPlaying)
            {
            HitAudio.Play();

            }


            GameObject Spark = Instantiate(Sparks,hit.point, Quaternion.identity);
            Destroy(Spark, 0.3f);
            StartCoroutine(Start());

        }



    }



  

    IEnumerator Start()
    {

        yield return new WaitForSeconds(0.5f);
        SParksHIt = false;



    }




    public void OnCollisionEnter(Collision collision)
    {

        if (collision.gameObject.layer == 11)
        {

            collision.gameObject.GetComponent<Rigidbody>().AddForce(transform.position * CC.Speed * 500f);
            CarAI Car = collision.gameObject.GetComponent<CarAI>();

            Car.CarStates = CarAI.CarBrain.hit;


            HitAudio.clip = Heavyhit;
          
            
                HitAudio.Play();
            
            GameObject E = Instantiate(Sparks, collision.transform.position, Quaternion.identity);
            Destroy(E, 1f);

        }

        if (collision.gameObject.layer == 12)
        {

        
            HitAudio.clip = Heavyhit;


            HitAudio.Play();

            GameObject E = Instantiate(Sparks, collision.transform.position, Quaternion.identity);
            Destroy(E, 1f);

        }

    }


    public void WheelIsGrounded()
    {

        foreach (var W in WS)
        {


            if(W.RareWheels)
            {


                if(W.IsGrounded == false)
                {



                    if(GetComponent<Rigidbody>().velocity.magnitude <= 1f)
                    {


                        Recovercar();


                    }
                }
            }


        }

    }





}
