using UnityEngine;
using UnityEngine.AI;
using System.Collections.Generic;

namespace Platformer397
{
    public class PauseManager : MonoBehaviour
    {
        [SerializeField] private Rigidbody player;
        [SerializeField] private List<NavMeshAgent> enemies = new List<NavMeshAgent>();

        public void PauseByTimeScale()
        {
            Time.timeScale = 0.0f;
        }
        public void UnPauseByTimeScale()
        {
            Time.timeScale = 1.0f;
        }
        public void PauseByComponents()
        {
            player.constraints = RigidbodyConstraints.FreezeAll;
            foreach (var agent in enemies)
            {
                agent.enabled = false;
            }
        }
        public void UnPauseByComponents()
        {
            player.constraints = RigidbodyConstraints.FreezeRotation;
            foreach (var agent in enemies)
            {
                agent.enabled = true;
            }
        }
    }   
}
