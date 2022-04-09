using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeizerCarAI : MonoBehaviour
{

    public Transform[] Routes;

    public Vector3 GizmosPosition;


    private void OnDrawGizmos()
    {


        for (float i = 0; i <= 1; i += 0.5f)
        {


            GizmosPosition = Mathf.Pow(1 - i, 3) * Routes[0].position + 3 * Mathf.Pow(1 - i, 2) * i * Routes[1].position + 3 * (1 - i) * Mathf.Pow(i, 2) * Routes[2].position + Mathf.Pow(i, 3) * Routes[3].position;

            Gizmos.DrawSphere(GizmosPosition, 1f);





        }


        Gizmos.DrawLine(new Vector3(Routes[0].position.x, Routes[0].position.y , Routes[0].position.z), new Vector3(Routes[1].position.x, Routes[1].position.y, Routes[1].position.z));

        Gizmos.DrawLine(new Vector3(Routes[2].position.x, Routes[2].position.y, Routes[2].position.z), new Vector3(Routes[3].position.x, Routes[3].position.y, Routes[3].position.z));

    }

}

