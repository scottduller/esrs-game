using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace GridBuildSystem.UI
{
    public class LevelSaveUI : MonoBehaviour
    {
        private GameObject _saveOptionsPane;

        private void Start()
        {
            _saveOptionsPane = transform.Find("LevelSavePanel").gameObject;
        
            _saveOptionsPane.transform.Find("CloseBtn").GetComponent<Button>().onClick.AddListener(CloseMenu);
        
            _saveOptionsPane.transform.Find("SubmitBtn").GetComponent<Button>().onClick.AddListener(Submit);
        
        }

        private void Submit()
        {
            string title = _saveOptionsPane.transform.Find("TitleInput").GetComponent<TMP_InputField>().text;
            string desc = _saveOptionsPane.transform.Find("DescInput").GetComponent<TMP_InputField>().text;
            if (title != string.Empty && desc != String.Empty)
            {
                LevelBuilderManager.Instance.SaveLevel(title,"test",desc);
                CloseMenu();
            }
            else
            {
                Debug.Log("INPUT ERROR");
            }
        }


        private void CloseMenu()
        {
            _saveOptionsPane.SetActive(false);
            //EXIT TO MAIN MENU
        
        }
    
        public void OpenMenu()
        {
            _saveOptionsPane.SetActive(true);
        
        }

    }
}
