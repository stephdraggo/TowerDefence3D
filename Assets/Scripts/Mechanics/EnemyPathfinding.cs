using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace TowerDefence.Mechanics.Enemies
{
    [AddComponentMenu("Mechanics/Enemy/Pathfinding with NavMesh")]
    [RequireComponent(typeof(NavMeshAgent))]
    [RequireComponent(typeof(Enemy))]
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

            if (!gameObject.TryGetComponent(out Enemy enemy)) //if no enemy script attached
            {
                gameObject.AddComponent<PlebEnemy>(); //get generic enemy
            }

            agent.speed = gameObject.GetComponent<Enemy>().Speed; //get speed of this enemy

            target = GameObject.FindGameObjectWithTag("Goal"); //find and connect goal
        }

        private void Update()
        {
            if (Vector3.Distance(target.transform.position, transform.position) < 1f) //if at goal
            {
                //take health from player here

                //destroy enemy here


                gameObject.SetActive(false);
            }
        }
    }
}