using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarMovement : MonoBehaviour
{


    private float MoveInuput;
    private float TrunInput;

    public Rigidbody MotorRb;
    public float ForwardSpeed;
    public float ReserveSpeed;
    public float TrunSpeed;
    public bool IsGrounded;
    public LayerMask GroundMask;
    public float GravityForce;
    public float AirDrag;
    public float GroundDrag;



    [Header("Speed UI")]
    public float SpeedUI;

    private void Start()
    {

        MotorRb.transform.parent = null;

    }


    private void Update()
    {

        SpeedUI = MotorRb.velocity.z;


        MoveInuput = Input.GetAxisRaw("Vertical");
        TrunInput = Input.GetAxisRaw("Horizontal");



        MoveInuput *= MoveInuput > 0 ? ForwardSpeed : ReserveSpeed;

        float NewRotation = TrunInput * TrunSpeed * Time.deltaTime * Input.GetAxisRaw("Vertical");
        transform.Rotate(0f, NewRotation, 0f);




        CheckGround();

    }
    public void FixedUpdate()
    {


        transform.position = MotorRb.transform.position;

        if (IsGrounded)
        {

            MotorRb.drag = GroundDrag;
        MotorRb.AddForce(transform.forward * MoveInuput, ForceMode.Acceleration);

        }
        else
        {

            MotorRb.drag = AirDrag;

            MotorRb.AddForce(transform.up * -GravityForce);
        }

    }



    public void CheckGround()
    {
        RaycastHit hit;
        IsGrounded = Physics.Raycast(transform.position, -transform.up, out hit, 1f ,GroundMask);

        Vector3 rot = hit.normal;


      // transform.rotation = Quaternion.FromToRotation(transform.up, hit.normal) * transform.rotation;
        //transform.rotation = Quaternion.Slerp(transform.rotation,Quaternion.FromToRotation(transform.up , rot), 5f * Time.deltaTime);

    }


}
