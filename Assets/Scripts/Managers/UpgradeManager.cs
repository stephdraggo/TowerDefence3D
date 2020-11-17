using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Upgrade stuff")]
    [SerializeField]
    private Text fireRate;
    [SerializeField]
    private Text range, damage;


    [SerializeField]
    private GameObject _towerMaxRange, _towerMinRange;
    [SerializeField]
    private bool mouseOverObject = false;
    [SerializeField]
    private TowerDefence.Menus.MenuManager menuMan;

    


    private void Start()
    {
        menuMan = FindObjectOfType<TowerDefence.Menus.MenuManager>();
    }

    private void OnMouseUpAsButton()
    {
        
        _towerMaxRange.SetActive(true);
        _towerMinRange.SetActive(true);
        menuMan.SidePanels[0].SetActive(true);
        mouseOverObject = true;       
    }

    private void OnMouseExit()
    {
        mouseOverObject = false;
        Debug.Log(mouseOverObject);
    }

    private void TurnOffRange()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && mouseOverObject == false)
        {
            _towerMaxRange.SetActive(false);
            _towerMinRange.SetActive(false);
        }  
    }

    private void Update()
    {
        TurnOffRange();
    }
}
