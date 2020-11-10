using System.Collections;
using System.Collections.Generic;
using TowerDefence.Managers;
using UnityEngine;

public class PlatformManager : MonoBehaviour
{
    [SerializeField]
    TowerManager manager;

    private void OnMouseUpAsButton()
    {
        manager.SpawnTowerPrefab(transform);
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
