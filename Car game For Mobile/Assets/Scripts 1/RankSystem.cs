using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RankSystem : MonoBehaviour
{


    public float Rank;

    public Transform CLoseToPLayer;
    public Transform CloseToEnemy;

    public Transform Player;
    public Transform Enemy;

    public float Range;

    public float EnemyRnage;

    public Transform[] WayPoints;

    public float Forward;
    public float SecondForward;

    Vector3 dis;
    Vector3 Edis;
    public Quaternion q;

    private void Update()
    {

        PlayerPosition();
        EnemyPosition();



        Vector3 Direction = CLoseToPLayer.position - CloseToEnemy.position;
        Vector3 dir = CloseToEnemy.position - CLoseToPLayer.position;


        Forward = Vector3.Dot(Player.transform.forward.normalized , Direction.normalized);
        SecondForward = Vector3.Dot(Enemy.transform.forward.normalized, dir.normalized);

       
      Quaternion.Dot(Player.transform.rotation, Quaternion.identity);


        if(Forward > 0 && SecondForward < 0 )
        {
            Rank = 1f;

            Debug.Log("Forward");
        } else if(Forward < 0 && SecondForward > 0 )
        {

            Rank = 2f;
            Debug.Log("Back");
        }

    }



    void PlayerPosition()
    {
        for (int i = 0; i < WayPoints.Length; i++)
        {
        if(Vector3.Distance(WayPoints[i].position , Player.position) < Range)
            {
                 dis = WayPoints[i].position - Player.position;

                CLoseToPLayer = WayPoints[i];
              
            }

        }


    

      
    }


    void EnemyPosition()
    {
        for (int i = 0; i < WayPoints.Length; i++)
        {
            if (Vector3.Distance(WayPoints[i].position, Enemy.position) < EnemyRnage)
            {
                Edis = WayPoints[i].position - Enemy.position;
                CloseToEnemy = WayPoints[i];

            }

        }





    }



    private void OnDrawGizmos()
    {


        Gizmos.color = Color.green;

        Gizmos.DrawWireSphere(Player.position, Range);

        Gizmos.DrawWireSphere(Enemy.position, EnemyRnage);



    }


}
