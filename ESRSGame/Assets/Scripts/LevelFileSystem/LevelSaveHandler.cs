using System;
using System.Collections.Generic;
using System.Linq;
using Api.Services;
using GridBuildSystem;
using UnityEngine;
using UnityEngine.UI;

namespace LevelFileSystem
{
    public class LevelSaveHandler : MonoBehaviour
    {
        public LevelServices levelServices;
        public string testNameRead; //test file for debugging
        public AppSceneManager appSceneManager;
        private List<string> _toWrite;
        private void Start()
        {
            Debug.Log(LevelBuilderManager.Instance);
        }
        // Update is called once per frame

        public bool WriteLevelToFile(string title, string desc, List<PlacedGridObject> floor, List<PlacedGridObject> interact)
        {
            _toWrite = new List<string>();
            foreach (PlacedGridObject placedGridObject in floor)
            {
                AddLevelItem(placedGridObject);
            }
            foreach (PlacedGridObject placedGridObject in interact)
            {
                AddLevelItem(placedGridObject);
            }
        
            SaveAndUploadLevel(title,desc);
            return true;
        }

    

        private void AddLevelItem(PlacedGridObject placedGridObject)
        {
            _toWrite.Add(placedGridObject.DataToString());
        
        }

        private void Result(bool result)
        {
            if(result){
                Debug.Log("SUCCESSFULLY SAVED");
                appSceneManager.LoadScene(3);
                
            }
            else
            {
                Debug.Log("ERROR: Failed to save level");
            }
        }
    
    
        private void SaveAndUploadLevel(string title, string desc)
        {
            string levelData  =string.Empty;
            try
            {
                foreach (string s in _toWrite)
                {
                    Debug.Log(s);
                    levelData = levelData + s + "\n";
                    Debug.Log(levelData.Length);
                }
                // Debug.Log(levelData);
                // Debug.Log(levelData.Length);
                levelServices.CreateLevel(title,desc ,levelData, Result);
            }
            catch (Exception e)
            {
                Debug.Log(e);
                throw;
            }
        }
    }
}
