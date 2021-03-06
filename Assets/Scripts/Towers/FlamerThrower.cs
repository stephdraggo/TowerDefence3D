﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlamerThrower : MonoBehaviour
{
    [SerializeField]
    Flammer flamer;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Debug.Log("enemy");
            if (flamer.targets != null)
            {
                flamer.targets.Add(other.gameObject.GetComponent<Enemy>());
            }
               
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.GetComponent<Enemy>() != null)
        {
            Debug.Log("enemy Left");
            if(flamer.targets != null && flamer.index < flamer.targets.Count)
            {
                flamer.targets.RemoveAt(flamer.index);
            }
            
        }
    }
}
