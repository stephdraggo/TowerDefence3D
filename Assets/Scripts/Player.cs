﻿using JetBrains.Annotations;
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
        [Tooltip("How much money the player wins at the end of a round")]
        public float rewardMoney;
        [SerializeField]
        private float health;
        [SerializeField]
        private float maxHealth = 100f;
        [SerializeField]
        private float zSpeed;
        [SerializeField]
        private float xSpeed;
        [SerializeField]
        public bool endWave;
       
        [SerializeField]
        private GameObject _displayCurrency;
        [SerializeField]
        private Text _displayReward;
        [SerializeField]
        private Image healthBar;
        [SerializeField]
        private TowerDefence.Menus.MenuManager menuManager;

        private Menus.WinLose winLose;
        public int HealthScore { get => (int)health; }

        private void MoveCamera()
        {
            transform.Translate(Input.GetAxis("Horizontal") * xSpeed * Time.deltaTime, 0, Input.GetAxis("Vertical") * zSpeed * Time.deltaTime);
        }

        public void PlayerMoneyReward(float _money)
        {
            rewardMoney += _money;
        }
        public void EndWaveShowReward()
        {
            bool endWave1 = endWave == true && health <= 80;
            bool endWave2 = endWave == true && health <= 40;
            bool endWave3 = endWave == true && health == 100;
            bool endWave4 = endWave == true && health < 100;

            if (endWave1)
            {
                rewardMoney *= 0.7f;
                AddMoney(rewardMoney);
                rewardMoney = 0;
                endWave = false;
            }
            if (endWave2)
            {
                rewardMoney *= 0.5f;
                AddMoney(rewardMoney);
                rewardMoney = 0;
                endWave = false;
            }
            if (endWave3)
            {
                rewardMoney += 100;
                AddMoney(rewardMoney);
                rewardMoney = 0;
                endWave = false;
            }
            if (endWave4)
            {
                AddMoney(rewardMoney);
                rewardMoney = 0;
                endWave = false;
            }

            
        }

        private void DisplayCurrency()
        {
            _displayCurrency.GetComponent<Text>().text = "$" + money;
            _displayReward.text = string.Format("${0}", rewardMoney);
        }

        public void PurchaseTower(int _towerCost)
        {
            money -= _towerCost;
        }

        public void AddMoney(float _addMoney)
        {
            money += _addMoney;
            winLose.money += _addMoney;
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

        private void PlayerDeath(GameObject deathScreen)
        {
            if (health <= 0)
            {
                winLose.Lose();
            }
        }

        private void Start()
        {
            health = maxHealth;
            menuManager = FindObjectOfType<TowerDefence.Menus.MenuManager>();
            winLose = FindObjectOfType<Menus.WinLose>();
            Time.timeScale = 1;
        }

        private void Update()
        {
            TowerDefence.Menus.MenuManager menMan = FindObjectOfType<TowerDefence.Menus.MenuManager>();

            MoveCamera();
            DisplayCurrency();
            SetHealth(health);

            PlayerDeath(menMan.Panels[4]);
            //EndWaveShowReward();
        }
    }
}
