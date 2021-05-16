using System;
using UnityEngine;
using UnityEngine.UI;

namespace GameScripts.UI
{
    public class LevelUIOptions : MonoBehaviour
    {
        private Button _exit;
        private Button _restart;
        public AppSceneManager _appSceneManager;

        private void Awake()
        {
            _exit = transform.Find("Exit").GetComponent<Button>();
            _restart = transform.Find("Restart").GetComponent<Button>();
            
            _exit.onClick.AddListener( () => _appSceneManager.LoadScene("MainMenu"));
            _restart.onClick.AddListener(() => _appSceneManager.ReloadScene());

        }
    }
}
