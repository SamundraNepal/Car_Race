using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DarwLine : MonoBehaviour
{
    public Color lineColor;
    private List<Transform> nodes = new List<Transform>();

    //TO make the path visible in the editor
    public void OnDrawGizmos()
    {
        //Set color for editor
        Gizmos.color = lineColor;

        Transform []PathTransform = GetComponentsInChildren<Transform>();

        nodes = new List<Transform>();


        for (int i = 0; i < PathTransform.Length; i++)
        {
            if(PathTransform[i] != transform)
            {

                nodes.Add(PathTransform[i]);

            }



        }

        for (int i = 0; i < nodes.Count; i++)
        {

            Vector3 CurrentNode = nodes[i].position;
            Vector3 PrevoisNode = Vector3.zero;


            if (i > 0)
            {


                PrevoisNode = nodes[i - 1].position;

            } else if (i == 0 && nodes.Count > 1)
            {

                PrevoisNode = nodes[nodes.Count - 1].position;

            }

            Gizmos.DrawLine(PrevoisNode, CurrentNode);

        }



        }
    }

