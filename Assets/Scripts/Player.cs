using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
using TowerDefence.Towers;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.notPlayer
{
    public class Player : MonoBehaviour
    {
        [Tooltip("How much money the player has")]
        public float money = 100f;
        [SerializeField]
        private float health;
        [SerializeField]
        private float maxHealth = 100f;
        [SerializeField]
        private float zSpeed;
        [SerializeField]
        private float xSpeed;
       
        [SerializeField]
        private GameObject _displayCurrency;
        [SerializeField]
        private Image healthBar;


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

        public void AddMoney(float _addMoney)
        {
            money += _addMoney;
        }

        private void SetHealth(float _health)
        {
            healthBar.fillAmount = Mathf.Clamp01(_health / maxHealth);
        }

        public void Damage(float _damage)
        {
            health -= _damage;
            if (health <= 0)
            {
                health = 0;
            }
        }

        private void Start()
        {
            health = maxHealth;
        }

        private void Update()
        {
            MoveCamera();
            DisplayCurrency();
            SetHealth(health);
        }
    }
}
