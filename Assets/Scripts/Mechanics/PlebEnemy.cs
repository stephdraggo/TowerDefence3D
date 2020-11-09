using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Mechanics.Enemies
{
    public class PlebEnemy : Enemy
    {
        [SerializeField] private float typeSpeed = 1f;

        private void Awake()
        {
            speed = typeSpeed;
            //assign new stat values here
        }

        void Update()
        {

        }
    }
}