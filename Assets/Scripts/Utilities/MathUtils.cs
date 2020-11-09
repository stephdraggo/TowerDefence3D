using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace TowerDefence.Utilities
{
    public class MathUtils
    {
        /// <summary>
        /// Used to get the distance and direction of the target
        /// from the transform of the gameobject to the transform the target
        /// </summary>
        /// <param name="_distance">The distance from A to B</param>
        /// <param name="_direction">The direction of A to B</param>
        /// <param name="_from">The starting point (attached gameObject)</param>
        /// <param name="_to">The target</param>
        public static void DistanceAndDirection(out float _distance, out Vector3 _direction, Transform _from, Transform _to)
        {
            // Defines the heading
            Vector3 heading = _to.position - _from.position;
            // Finds the distance of the heading
            _distance = heading.magnitude;
            // Finds the direction of the heading
            _direction = heading.normalized;
        }
    }
}

