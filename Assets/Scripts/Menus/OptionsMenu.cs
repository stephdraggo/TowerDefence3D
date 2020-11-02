using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Audio;

namespace TowerDefence.Menus
{
    [AddComponentMenu("Menus/Options Menu")]
    public class OptionsMenu : MonoBehaviour
    {
        #region Variables
        [SerializeField,Tooltip("Connect AudioMixer for this scene here.")]
        private AudioMixer mixer;

        [SerializeField, Tooltip("Connect resolution dropdown from the options panel here.")]
        private Dropdown resolution;

        [Tooltip("Array of possible resolutions.")]
        private Resolution[] resolutions;

        #endregion
        #region Start
        void Start()
        {
            StartResolution();
        }
        #endregion
        #region Functions
        #region fullscreen done
        /// <summary>
        /// Set fullscreen or windowed.
        /// </summary>
        public void SetFullscreen(bool f)
        {
            Screen.fullScreen = f;
        }
        #endregion
        #region quality done
        /// <summary>
        /// Set quality level from dropdown
        /// </summary>
        public void ChangeQuality(int i)
        {
            QualitySettings.SetQualityLevel(i);
        }
        #endregion
        #region audio, waiting on sound
        /// <summary>
        /// Set music volume from slider.
        /// </summary>
        public void SetMusicVolume(float value)
        {
            mixer.SetFloat("MusicVolume", value);
        }
        /// <summary>
        /// Set sfx volume from slider.
        /// </summary>
        public void SetSFXVolume(float value)
        {
            mixer.SetFloat("SFXVolume", value);
        }
        /// <summary>
        /// Mute all sound.
        /// </summary>
        public void MuteToggle(bool mute)
        {
            if (mute)
            {
                mixer.SetFloat("MasterVolume", -80);
            }
            else
            {
                mixer.SetFloat("MasterVolume", 0);
            }
        }
        #endregion
        #region resolution done
        /// <summary>
        /// Set up resolution options for this screen.
        /// </summary>
        public void StartResolution()
        {
            resolutions = Screen.resolutions; //fill array with all possible resolutions for the current screen
            resolution.ClearOptions(); //clear selection
            List<string> options = new List<string>(); //empty list of options
            int index = 0; //reset index
            for (int i = 0; i < resolutions.Length; i++) //for all resolutions in array
            {
                string option = resolutions[i].width + "x" + resolutions[i].height; //make string based on resolution
                float difference = ((float)resolutions[i].width / (float)resolutions[i].height) - (16f / 9f); //see if close enough to 16x9
                if (difference < 0.01f && difference > -0.01f) //if this resolution is 16x9
                {
                    options.Add(option); //add string to options list
                }
                if (resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height) //if selected resolution is active resolution
                {
                    index = i; //set index to active resolution index
                }
            }
            resolution.AddOptions(options); //put list of options into dropdown ui
            resolution.value = index; //ui select current resolution
            resolution.RefreshShownValue(); //refresh display
        }
        /// <summary>
        /// Set resolution according to given resolution index.
        /// </summary>
        /// <param name="index">index of selected resolution</param>
        public void SetResolution(int index)
        {
            Screen.SetResolution(resolutions[index].width, resolutions[index].height, Screen.fullScreenMode); //set selected resolution
        }
        #endregion
        #endregion
    }
}

//#region Variables

//#endregion
//#region Properties

//#endregion
//#region Start
//void Start()
//{

//}
//#endregion
//#region Update
//void Update()
//{

//}
//#endregion
//#region Functions

//#endregion
