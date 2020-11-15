using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace TowerDefence.Menus
{
    public class WinLose : MonoBehaviour
    {
        #region Variables
        private MenuManager menuManager;
        private Mechanics.Spawning.WaveSpawner spawner;
        private notPlayer.Player player;

        [SerializeField]
        private Text[] scoreText, statsText;

        private string score, stats;

        public float money, plebCount, fastCount, tankCount;
        #endregion
        #region Start
        void Start()
        {
            menuManager = FindObjectOfType<MenuManager>();
            spawner = FindObjectOfType<Mechanics.Spawning.WaveSpawner>();
            player = FindObjectOfType<notPlayer.Player>();
        }
        #endregion

        public void Win()
        {
            DisplayResults(3);
        }
        public void Lose()
        {
            DisplayResults(4);
        }
        private void DisplayResults(int index)
        {
            menuManager.ShowOnePanel(menuManager.Panels[index]);
            Time.timeScale = 0;
            GetScore();
            GetStats();
            scoreText[0].text = score;
            scoreText[1].text = score;
            statsText[0].text = stats;
            statsText[1].text = stats;
        }
        private void GetScore()
        {
            int _score = 0;
            _score += spawner.WaveNumber * 100; //add 100 per wave passed
            _score += player.HealthScore; //add score according to how much health is left

            score = "Overall Score: " + _score.ToString(); //convert score to displayable text
        }
        private void GetStats()
        {
            string _stats="";

            _stats += "Total Money Collected: ";
            _stats += ((int)money).ToString();

            _stats += "\nIsopods Encountered: ";
            _stats += ((int)plebCount).ToString();

            _stats += "\nSilverfish Encountered: ";
            _stats += ((int)fastCount).ToString();

            _stats += "\nRhino Beetles Encountered: ";
            _stats += ((int)tankCount).ToString();

            stats = _stats;
        }
    }
}