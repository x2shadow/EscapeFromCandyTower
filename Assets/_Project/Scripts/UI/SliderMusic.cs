using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class SliderMusic : MonoBehaviour
    {
        private const string MusicVolumeKey = "MusicVolume";

        Slider slider;

        void Start()
        {
            slider = GetComponent<Slider>();

            float savedVolume = PlayerPrefs.GetFloat(MusicVolumeKey, 0.5f);
            slider.value = savedVolume;

            slider.onValueChanged.AddListener(AudioManager.Instance.SetMusicVolume);
            AudioManager.Instance.SetMusicVolume(savedVolume);
        }

        public void Refresh()
        {
            slider.value = PlayerPrefs.GetFloat("LastMusicVolume", 0.5f);
        }
    }
}
