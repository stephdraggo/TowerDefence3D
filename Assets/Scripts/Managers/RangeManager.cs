using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeManager : MonoBehaviour
{
    [SerializeField]
    private GameObject _towerMaxRange, _towerMinRange;
    [SerializeField]
    private bool mouseOverObject = false;

    private void OnMouseUpAsButton()
    {
        _towerMaxRange.SetActive(true);
        _towerMinRange.SetActive(true);
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
