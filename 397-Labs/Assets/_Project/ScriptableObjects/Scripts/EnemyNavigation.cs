using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Platformer397
{
    public class EnemyNavigation : MonoBehaviour, IObserver
    {
        private NavMeshAgent agent;
        [SerializeField] private PlayerController player;
        [SerializeField] private List<Transform> waypoints = new List<Transform>();
        [SerializeField] private float distanceThreshold = 1.0f;
        private int index = 0;
        private Vector3 destination;
        //Enemy sensing stats
        [SerializeField] private LayerMask mask; //The layer that correspond to player
        [SerializeField] private int viewDistance = 10;
        [SerializeField] private EnemyStates state = EnemyStates.Patrolling;
        private void Awake()
        {
            agent = GetComponent<NavMeshAgent>();
            destination = waypoints[index].position;
            player = GameObject.FindWithTag("Player").GetComponent<PlayerController>();
        } 
        private void OnEnable()
        {
            player.AbbObserver(this);
        }
        private void OnDisable()
        {
            player.RemoveObserver(this);
        }
        private void Start()
        {
            agent.destination = destination;
        }

        private void Update()
        {
            switch(state)
            {
                case EnemyStates.Patrolling:
                    if (Vector3.Distance(destination, transform.position) < distanceThreshold)
                    {
                        index = (index + 1) % waypoints.Count;
                        destination = waypoints[index].position;
                    }
                    break;
                case EnemyStates.Chasing:
                    //starts chasing the player while visable
                    destination = player.gameObject.transform.position;
                    break;
                default:
                    Debug.LogError("state not configured", this);
                    break;
            }
            agent.destination = destination;
        }

        private void FixedUpdate()
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, viewDistance, mask))
            {
                if (hit.transform.gameObject.CompareTag("Player"))
                {
                    state = EnemyStates.Chasing;
                }
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * hit.distance, Color.green);
            }
            else
            {
                state = EnemyStates.Patrolling;
                Debug.DrawRay(transform.position, transform.TransformDirection(Vector3.forward) * viewDistance, Color.yellow);
            }
        }

        private void OnDrawGizmos()
        {
            if (waypoints.Count > 0)
            {
                Gizmos.color = Color.red;
                for (int i = 0; i < waypoints.Count; i++)
                {
                    Gizmos.DrawSphere(waypoints[i].position, distanceThreshold);
                    if (i > 0)
                    {
                        Gizmos.DrawLine(waypoints[i - 1].position, waypoints[i].position);
                    }
                }
            }
        }

        public void OnNotify()
        {
            Debug.Log($"Notify the subject");
        }
    }
}
