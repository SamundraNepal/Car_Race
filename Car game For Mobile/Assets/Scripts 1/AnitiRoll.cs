using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnitiRoll : MonoBehaviour
{


    [SerializeField] private Rigidbody carRB;
    public WheelSuspension[] wheelsOnAxle;

    private float lengthDifference;

    [Tooltip("Stiffness Should Be Around A Tenth Of The Spring Stiffness Of Each Wheel")]
    public float antiRollBarStiffness;
    public float antiRollBarForce;

    public void Start()
    {
        carRB.GetComponent<Rigidbody>();
        wheelsOnAxle = GetComponentsInChildren<WheelSuspension>();
    }

    public void FixedUpdate()
    {
        for (int i = 0; i < wheelsOnAxle.Length; i++)
        {
            if (wheelsOnAxle[i].IsGrounded)
            {
                lengthDifference = (wheelsOnAxle[0].SpringLength - wheelsOnAxle[1].SpringLength) / ((wheelsOnAxle[0].RestLength + wheelsOnAxle[1].RestLength) / 2);
            }
            else
            {
                lengthDifference = 0;
            }
        }

        antiRollBarForce = lengthDifference * antiRollBarStiffness;

        if (wheelsOnAxle[0].IsGrounded)
        {
            carRB.AddForceAtPosition(-antiRollBarForce  * wheelsOnAxle[0].transform.up, wheelsOnAxle[0].transform.position);
        }

        if (wheelsOnAxle[1].IsGrounded)
        {
            carRB.AddForceAtPosition(antiRollBarForce * wheelsOnAxle[1].transform.up, wheelsOnAxle[1].transform.position);
        }
    }
}
