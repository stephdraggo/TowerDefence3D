using System.Collections;
using System.Collections.Generic;
using TowerDefence.Managers;
using TowerDefence.notPlayer;
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

        [SerializeField, Tooltip("Target object.")]
        public GameObject target;

        [SerializeField, Tooltip("enemy Script")]
        private Enemy _enemy;

        void Start()
        {
            agent = gameObject.GetComponent<NavMeshAgent>(); //connect nav mesh agent

            target = GameObject.FindWithTag("Goal"); //find and connect goal

            _enemy = gameObject.GetComponent<Enemy>(); // find and connect enemyScript

            agent.SetDestination(target.transform.position); //set target position

            if (!gameObject.TryGetComponent(out Enemy enemy)) //if no enemy script attached
            {
                gameObject.AddComponent<PlebEnemy>(); //get generic enemy
            }

            agent.speed = gameObject.GetComponent<Enemy>().Speed; //get speed of this enemy

            
        }
        
        public void EnemyHasReachedGoal()
        {
            if (Vector3.Distance(target.transform.position, transform.position) < 1f) //if at goal
            {
                _enemy.inRange = true;

                //_enemy.AttackPlayer();

                //_enemy.Die();

                //gameObject.SetActive(false);
            }
        }

        private void Update()
        {
            //if (Vector3.Distance(target.transform.position, transform.position) < 1f) //if at goal
            //{
            //    _enemy.AttackPlayer();

            //    _enemy.Die();

            //    //gameObject.SetActive(false);
            //};
        }
    }
}