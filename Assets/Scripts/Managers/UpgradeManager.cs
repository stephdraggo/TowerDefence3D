using System.Collections;
using System.Collections.Generic;
using TowerDefence.Managers;
using TowerDefence.notPlayer;
using TowerDefence.Towers;
using UnityEngine;
using UnityEngine.UI;

public class UpgradeManager : MonoBehaviour
{
    [Header("Upgrade stuff")]
    [SerializeField]
    public Text towerName;
    [SerializeField]
    public Text fireRate, range, damage;
    [SerializeField]
    public float fireRateCost = 10, rangeCost = 20, damageCost = 5;


    [SerializeField]
    private GameObject _towerMaxRange, _towerMinRange;
    [SerializeField]
    private bool mouseOverObject = false;
    [SerializeField]
    private TowerDefence.Menus.MenuManager menuMan;
    [SerializeField]
    private TowerManager towerManager;
    [SerializeField]
    public BaseTower tower;
    [SerializeField]
    private Player player;


   

    private void UpgradesDisplayText(string _towerName)
    {
        towerName.text = _towerName;
        fireRate.text = string.Format("${0}", fireRateCost);
        damage.text = string.Format("${0}", damageCost);
        range.text = string.Format("${0}", rangeCost);
    }

    private void SetTowerTextObjects()
    {
        towerName = towerManager.towerUpgradesText[0];
        fireRate = towerManager.towerUpgradesText[1];
        damage = towerManager.towerUpgradesText[3];
        range = towerManager.towerUpgradesText[2];
    }

    #region Mouse Activation Stuff
    private void OnMouseUpAsButton()
    {
        UpgradesDisplayText(tower.TowerName);
        _towerMaxRange.SetActive(true);
        _towerMinRange.SetActive(true);
        menuMan.SidePanels[0].SetActive(true);
        mouseOverObject = true;

        towerManager.currentTower = this;
    }
    
    private void OnMouseExit()
    {
        mouseOverObject = false;
        Debug.Log(mouseOverObject);
    }
    #endregion

    #region Range
    private void TurnOffRange()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0) && mouseOverObject == false)
        {
            _towerMaxRange.SetActive(false);
            _towerMinRange.SetActive(false);
        }  
    }
    #endregion

    private void Awake()
    {
        menuMan = FindObjectOfType<TowerDefence.Menus.MenuManager>();
        towerManager = FindObjectOfType<TowerManager>();
        tower = GetComponent<BaseTower>();
        SetTowerTextObjects();
    }

    private void Start()
    {

    }

    private void Update()
    {
        TurnOffRange();
       
    }
}
