using System.Collections;
using System.Collections.Generic;
using GamePush;
using UnityEngine;

namespace Platformer
{
    public class GPMuteForPreloader : MonoBehaviour
    {
        [SerializeField] SliderMusic sliderMusic;
        [SerializeField] SliderSound sliderSound;

        async void Start()
        {
            await GP_Init.Ready;
            AudioManager.Instance.SetMusicVolume(0.5f);
            AudioManager.Instance.SetSoundVolume(0.5f);
            if(GP_Ads.IsPreloaderPlaying()) AudioManager.Instance.MuteForAd();
            GP_Ads.OnPreloaderClose += (bool isClosed) => { AudioManager.Instance.UnmuteAfterAd(); RefreshSliders(); };
        }

        void RefreshSliders()
        {
            sliderMusic.Refresh();
            sliderSound.Refresh();
        }
    }
}
