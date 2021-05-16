using System;
using UnityEngine;

namespace GameScripts.UI
{
    public class GameUIHandler : MonoBehaviour
    {
        private GameObject _completeWindow;
        private GameObject _gameOverWindow;


        private void OnDestroy()
        {
            GameEventManager.OnLevelComplete -= OnLevelComplete;
            GameEventManager.OnGameOver -= OnGameOver;
        }

        private void Awake()
        {
            _completeWindow = transform.Find("LevelEnd").gameObject;
            _gameOverWindow = transform.Find("GameOver").gameObject;
            _completeWindow.SetActive(false);
            _gameOverWindow.SetActive(false);
            GameEventManager.OnLevelComplete += OnLevelComplete;
            GameEventManager.OnGameOver += OnGameOver;
        }

        private void OnGameOver(object sender, EventArgs e)
        {
            Time.timeScale = 0;
            _gameOverWindow.SetActive(true);;
        }

        private void OnLevelComplete(object sender, EventArgs e)
        {
            Time.timeScale = 0;
            _completeWindow.SetActive(true);
        
        }
    }
}
