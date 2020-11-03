using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

/* to set up enemy:
 * Window > AI > Navigation
 * select all objects that will affect navigation
 * tick 'Navigation Static'
 */
namespace TowerDefence.Mechanics.Enemy
{
    [AddComponentMenu("Mechanics/Enemy/Pathfinding with NavMesh")]
    [RequireComponent(typeof(NavMeshAgent))]
    public class EnemyPathfinding : MonoBehaviour
    {
        [SerializeField, Tooltip("Auto connects on start to its own nav mesh agent component.")]
        private NavMeshAgent agent;

        [SerializeField, Tooltip("Target object, not automated yet.")]
        public GameObject target;

        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>(); //connect nav mesh agent

            agent.SetDestination(target.transform.position); //set target position
        }
    }
}