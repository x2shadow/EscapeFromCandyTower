using System.Collections;
using System.Collections.Generic;
using GamePush;
using UnityEngine;

namespace Platformer
{
    public class AdManager : MonoBehaviour
    {
        void Start()
        {
            // Проверка включенного AdBlock
            Debug.Log("IsAdblockEnabled: " + GP_Ads.IsAdblockEnabled());

            // Проверка доступности рекламного баннера
            Debug.Log("IsFullscreenAvailable: " + GP_Ads.IsFullscreenAvailable());  
            Debug.Log("IsPreloaderAvailable: "  + GP_Ads.IsPreloaderAvailable());  
            Debug.Log("IsStickyAvailable: "     + GP_Ads.IsStickyAvailable());  
        }

        public void ShowFullscreen()
        {
            if (GP_Ads.IsFullscreenAvailable())
            {
                GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
            }
        }

        private void OnFullscreenStart()
        {
            Debug.Log("ON FULLSCREEN START");
            AudioManager.Instance.MuteForAd();
        }

        private void OnFullscreenClose(bool success)
        {
            Debug.Log("ON FULLSCREEN CLOSE: " + success);
            AudioManager.Instance.UnmuteAfterAd();
        }
    }
}
