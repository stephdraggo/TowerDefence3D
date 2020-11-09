using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Mechanics.Enemies
{
    public class FastEnemy : Enemy
    {
        [SerializeField] private float typeSpeed = 5f;

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