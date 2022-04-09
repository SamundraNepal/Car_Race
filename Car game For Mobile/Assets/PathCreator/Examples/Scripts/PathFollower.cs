using UnityEngine;

namespace PathCreation.Examples
{
    // Moves along a path at constant speed.
    // Depending on the end of path instruction, will either loop, reverse, or stop at the end of the path.
    public class PathFollower : MonoBehaviour
    {
        public LapManager Lm;

        public float AngularVelocity;

        public float CloseDistnace;
        public float Newdis;
        public float dot;
        public Transform Player;
        public PathCreator pathCreator;
        public EndOfPathInstruction endOfPathInstruction;
        public float speed = 5;
       public float distanceTravelled;

        public float CurrentSpeed;

        void Start() {
            if (pathCreator != null)
            {
                // Subscribed to the pathUpdated event so that we're notified if the path changes during the game
                pathCreator.pathUpdated += OnPathChanged;
            }
        }



        public void Update()
        {
            if (Lm.StartLap)
            {



                distanceTravelled += speed * Time.deltaTime;
                transform.position = pathCreator.path.GetPointAtDistance(distanceTravelled, endOfPathInstruction);
                transform.rotation = pathCreator.path.GetRotationAtDistance(distanceTravelled, endOfPathInstruction);
            }
            EnemyMotor();

        }
    

        // If the path changes during the game, update the distance travelled so that the follower's position on the new path
        // is as close as possible to its position on the old path
          void OnPathChanged() {
            distanceTravelled = pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }







        void EnemyMotor()
        {
            CloseDistnace = Vector3.Distance(Player.transform.position, transform.position);

            if(CloseDistnace <= 30f)
            {
                    speed = CurrentSpeed;

            } else
            {

                speed = 25f;
            }



            Vector3 dis = (Player.transform.position - transform.position).normalized;
            dot = Vector3.Dot(transform.forward, dis);
           

            if(dot > 0)
            {

                Debug.DrawRay(transform.position, dis * dot, Color.red);

            } if(dot < 0)
            {


                Debug.DrawRay(transform.position, dis * dot, Color.green);

            }

        }
    }
}