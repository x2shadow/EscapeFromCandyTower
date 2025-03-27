using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Platformer
{
    public class SliderSound : MonoBehaviour
    {
        private const string SoundVolumeKey = "SoundVolume";

        Slider slider;

        void Start()
        {
            slider = GetComponent<Slider>();

            float savedVolume = PlayerPrefs.GetFloat(SoundVolumeKey, 0.5f);
            slider.value = savedVolume;

            slider.onValueChanged.AddListener(AudioManager.Instance.SetSoundVolume);
            AudioManager.Instance.SetSoundVolume(savedVolume);
        }

        public void Refresh()
        {
            slider.value = PlayerPrefs.GetFloat("LastSoundVolume", 0.5f);
        }
    }
}
