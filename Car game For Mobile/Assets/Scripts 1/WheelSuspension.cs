using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelSuspension : MonoBehaviour
{
    public LapManager Lm;


    [Header("Wheels Name")]
    public Transform RightWHeel;
    public Transform LeftWHeel;

    public bool RW;
    public bool LW;


    [Header("Suspension")]
    public float RestLength;
    public float SpringTravel;
    public float SpringStifness;
    public float DamperStifness;
    public Vector3 Suspension_;

    private float MinLength, MaxLength;
    public float SpringLength;
    private float SpringForce;
    private float DamperForce;
    private float SpringVelocity;
    private float LastLenght;



    [Header("WHeel")]
    public float WheelRadius;

   public Rigidbody rb;
    public Vector3 WheelsLS;
    public float Vertical;

   public RaycastHit hit;


    public bool RareWheels;
   public bool IsGrounded;

    public float AntiTravel;
    public float AntiRollForce;
    public float Travel;
 

    private void Start()
    {
        rb = GetComponentInParent<Rigidbody>();
        MaxLength = RestLength + SpringTravel;
        MinLength = RestLength - SpringTravel;
    }



    private void Update()
    {
        
        

    }
    private void FixedUpdate()
    {
       
        Suspension();

        
     //   AntForce();

    }


    void Suspension()
    {

        if(Physics.Raycast(transform.position ,-transform.up, out hit ,MaxLength + WheelRadius))
            {

            IsGrounded = true;
            LastLenght = SpringLength;

            SpringLength = hit.distance - WheelRadius;

            SpringLength = Mathf.Clamp(SpringLength, MinLength, MaxLength);
            SpringVelocity = (LastLenght - SpringLength) / Time.fixedDeltaTime;
            SpringForce = SpringStifness * (RestLength - SpringLength);
              DamperForce = DamperStifness * SpringVelocity;
             Suspension_ = (SpringForce  + DamperForce) * transform.up;

            WheelsLS = transform.InverseTransformDirection(rb.GetPointVelocity(hit.point));
       //    Vertical = cc.Vertical * cc.Speed * SpringForce;
            float Y = WheelsLS.x * SpringForce;
            rb.AddForceAtPosition(Suspension_  + (Y * -transform.right) ,hit.point);
      
             Debug.DrawRay(transform.position, -transform.up * (MaxLength + WheelRadius), Color.red);

         //   rb.drag = 0.5f;


        } else
        {
         //   rb.drag = 0f;
            IsGrounded = false;
            Debug.DrawRay(transform.position, -transform.up * (MaxLength + WheelRadius), Color.grey);

        }

    }


    public void AntiForce()
    {


        if (IsGrounded)
        {

             Travel = (RightWHeel.transform.GetComponent<WheelSuspension>().SpringLength - LeftWHeel.transform.GetComponent<WheelSuspension>().SpringLength) / (RightWHeel.transform.GetComponent<WheelSuspension>().RestLength + LeftWHeel.transform.GetComponent<WheelSuspension>().RestLength) / 2;
             AntiRollForce = Travel * AntiTravel;

            if (RW)
            { 
            RightWHeel.GetComponent<WheelSuspension>().rb.AddForceAtPosition(  -AntiRollForce  * RightWHeel.transform.up, RightWHeel.transform.position);
            } else
            
            if (LW)
            {
                LeftWHeel.GetComponent<WheelSuspension>().rb.AddForceAtPosition(AntiRollForce * LeftWHeel.transform.up, LeftWHeel.transform.position);

            }

        } else
        {
            Travel = 0f;
            AntiRollForce = 0f;
        }




    }

    private void OnDrawGizmosSelected()
    {

        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(transform.position, WheelRadius);


    }







}



