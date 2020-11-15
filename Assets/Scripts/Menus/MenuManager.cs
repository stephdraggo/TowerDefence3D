using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace TowerDefence.Menus
{
    [AddComponentMenu("Menus/Menu Manager")]
    public class MenuManager : MonoBehaviour
    {
        #region Variables
        public static bool isPaused;

        [Header("Reference Variables")]

        [SerializeField, Tooltip("Tell the script if this is a Game Scene or not before playing or building.")]
        private bool gameSceneActive;

        [SerializeField, Tooltip("All mutually exclusive panels in this scene, the order is important.\n\n" +
            "Menu scene: Main Menu, Options Menu, Level Select Menu.\n\n" +
            "Game scene: Options Menu, Pause Menu, HUD, Win Screen, Lose Screen.")]
        private GameObject[] panels;

        [SerializeField, Tooltip("Side panels in game scene, leave empty if menu scene.")]
        private GameObject[] sidePanels;

        [SerializeField, Tooltip("Attach one spawner to access wave number.")]
        private Mechanics.Spawning.WaveSpawner spawner;


        #endregion
        #region Properties
        //in case we want to access these from another script
        public bool GameSceneActive { get => gameSceneActive; }
        public GameObject[] Panels { get => panels; }
        public GameObject[] SidePanels { get => sidePanels; }
        #endregion
        #region Start
        void Start()
        {
            if (gameSceneActive) //if game scene
            {
                #region check pause
                if (isPaused) //if paused
                {
                    Resume(); //resume
                }
                #endregion

                #region display panels
                ShowOnePanel(panels[2]); //show HUD on start
                sidePanels = GameObject.FindGameObjectsWithTag("SidePanel"); //get side panels
                for (int i = 0; i < sidePanels.Length; i++) //hide side panels
                {
                    sidePanels[i].SetActive(false);
                }
                #endregion

            }
            else
            {
                #region display panels
                ShowOnePanel(panels[0]); //show main menu on start
                sidePanels = new GameObject[0]; //there are no side panels
                #endregion

            }
        }
        #endregion
        #region Update
        void Update()
        {
            if (gameSceneActive)
            {
                //Win();
                //Lose();
            }
            else
            {

            }
        }
        #endregion
        #region Functions
        #region Shared Functions
        #region show one panel, waiting on save function
        /// <summary>
        /// Deactivate all panels except the chosen panel.
        /// </summary>
        /// <param name="_panel">The target panel to show.</param>
        public void ShowOnePanel(GameObject _panel)
        {
            for (int i = 0; i < panels.Length; i++)
            {
                panels[i].SetActive(false);
            }
            _panel.SetActive(true);

            //save function here
        }
        #endregion
        #region quit, waiting on save function
        /// <summary>
        /// Quit game or play mode;
        /// </summary>
        public void Quit()
        {
            //call saving function here

            Application.Quit();
#if UNITY_EDITOR
            EditorApplication.isPlaying = false;
#endif
        }
        #endregion
        #region pause & resume, waiting on tower select
        //might have to deselect any tower attached to mouse
        /// <summary>
        /// Pause game, only works is game scene.
        /// </summary>
        public void Pause()
        {
            if (gameSceneActive) //make sure you can only pause in the game scene
            {
                isPaused = true;
                Time.timeScale = 0;
                ShowOnePanel(panels[1]); //show pause panel

                for (int i = 0; i < sidePanels.Length; i++) //hide side panels
                {
                    sidePanels[i].SetActive(false);
                }
            }
        }
        /// <summary>
        /// Resume game, only works in game scene.
        /// </summary>
        public void Resume()
        {
            if (gameSceneActive) //make sure you can only pause in the game scene
            {
                isPaused = false;
                Time.timeScale = 1;
                ShowOnePanel(panels[2]); //show HUD
            }
        }
        #endregion
        #region load scene done
        /// <summary>
        /// Load a scene by build index.
        /// </summary>
        /// <param name="_index">Build index of the scene to load.</param>
        public void LoadScene(int _index)
        {
            SceneManager.LoadScene(_index);
        }
        #endregion



        #endregion
        #region Game Functions
        #region side panel, waiting on tower select
        //not ready, do not use
        public void ShowSidePanel(GameObject _panel)
        {
            for (int i = 0; i < sidePanels.Length; i++) //hide side panels
            {
                sidePanels[i].SetActive(false);
            }
            //if a place is selected and there is a tower in that space
            sidePanels[0].SetActive(true); //show single
            //else
            sidePanels[1].SetActive(true); //show multiple

        }
        #endregion
        #endregion
        #region Menu Functions
        #region select difficulty, waiting on difficulty stats, change scene int when in main game project
        /// <summary>
        /// Load scene with difficulty.
        /// </summary>
        /// <param name="_index">0=easy, 1=normal, 2=hard</param>
        public void SelectDifficulty(int _index)
        {
            //difficulty info goes here

            LoadScene(0); //load game scene
        }
        #endregion
        #region load saved game, waiting on save function, change scene int when in main game project
        public void LoadSavedGame()
        {
            //saved info goes here

            LoadScene(0); //load game scene
        }
        #endregion



        #endregion
        #endregion
    }
}