using System.Collections.Generic;
using Api.Models;
using Api.Services;
using UnityEngine;


namespace LevelFileSystem
{
    public class LevelSelection : MonoBehaviour
    {

        public LevelServices levelServices;

        public AppSceneManager appSceneManager;
        // Start is called before the first frame update
    
     

        public void GetARandomLevel()
        {
            levelServices.GetAllLevels(SetRandomLevel);
        }

        private void SetRandomLevel(List<Level> result)
        {

            int r = Mathf.FloorToInt(Random.Range(0, result.Count));
            Level selected = result[r];
            LoadLevel(selected);
        }


        private void LoadLevel(Level level)
        {
            GlobalValues.currentLevel = level;
            appSceneManager.LoadScene(5);
        }
    
    
    
    }
}
