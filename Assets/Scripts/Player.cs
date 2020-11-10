using System.Collections;
using System.Collections.Generic;
using TowerDefence.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.notPlayer
{
    public class Player : MonoBehaviour
    {
        [SerializeField, Tooltip("How much money the player has")]
        public float money = 100;
        [SerializeField]
        private float zSpeed;
        [SerializeField]
        private float xSpeed;
       
        [SerializeField]
        private GameObject _displayCurrency;


        private void MoveCamera()
        {
            transform.Translate(Input.GetAxis("Horizontal") * xSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * zSpeed * Time.deltaTime);
        }

        private void DisplayCurrency()
        {
            _displayCurrency.GetComponent<Text>().text = "$" + money;
        }

        public void PurchaseTower(int _towerCost)
        {
            money -= _towerCost;
        }

        private void Start()
        {

        }

        private void Update()
        {
            MoveCamera();
            DisplayCurrency();
        }
    }
}
