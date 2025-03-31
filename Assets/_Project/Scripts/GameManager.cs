using System.Collections;
using System.Collections.Generic;
using GamePush;
using KBCore.Refs;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class GameManager : ValidatedMonoBehaviour
    {
        public static GameManager Instance;

        [Header("References")]
        [SerializeField, Anywhere] InputReader input;
        [SerializeField] Transform player;
        [SerializeField] CameraManager cameraManager;
        [SerializeField] CheckpointsManager checkpointsManager;
        [SerializeField] GameObject canvasPause;
        [SerializeField] GameObject canvasWin;
        [SerializeField] GameObject canvasLose;
        [SerializeField] GameObject tabIcon;
        [SerializeField] GameObject textPause;
        [SerializeField] GameObject canvasMobile;

        public bool isPaused;

        void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;
            }
        }

        void Start()
        {
            if (Application.isMobilePlatform)
            {
                tabIcon.SetActive(false);
                textPause.SetActive(false);
                canvasMobile.SetActive(true);
            }
        }

        void OnEnable()
        {
            input.Pause += OnPause;
        }

        void OnDisable()
        {
            input.Pause -= OnPause;
        }

        void OnPause()
        {
            TogglePause();
        }

        public void TogglePause()
        {
            if (!isPaused)
            {
                isPaused = true;
                canvasPause.SetActive(true);
                cameraManager.OnDisableMouseControlCamera();
            }
            else
            {
                isPaused = false;
                canvasPause.SetActive(false);
                cameraManager.OnEnableMouseControlCamera();
            }
        }

        public void LoadMainMenuScene()
        {
            isPaused = false;
            SceneManager.LoadScene("MainMenu");
        }

        public void ShowWin()
        {
            isPaused = true;
            canvasWin.SetActive(true);
            cameraManager.OnDisableMouseControlCamera();
        }

        public void ShowLose()
        {
            isPaused = true;
            canvasLose.SetActive(true);
            cameraManager.OnDisableMouseControlCamera();
        }

        public void CloseLose()
        {
            if (GP_Ads.IsFullscreenAvailable())
            {
                GP_Ads.ShowFullscreen(OnFullscreenStart, OnFullscreenClose);
            }
            else
            {
                canvasLose.SetActive(false);            
                isPaused = false;
                cameraManager.OnEnableMouseControlCamera();
            }
        }

        void OnFullscreenStart()
        {
            Debug.Log("ON FULLSCREEN START");
            AudioManager.Instance.MuteForAd();
        }

        void OnFullscreenClose(bool success)
        {
            Debug.Log("ON FULLSCREEN CLOSE: " + success);
            AudioManager.Instance.UnmuteAfterAd();
            canvasLose.SetActive(false);            
            isPaused = false;
            cameraManager.OnEnableMouseControlCamera();
        }

        public void MovePlayerToCheckpoint()
        {
            Vector3 spawnPosition = checkpointsManager.checkpointList[checkpointsManager.lastCheckpointIndex].gameObject.transform.position;
            player.position = spawnPosition;
        }
    }
}
