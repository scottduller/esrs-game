using System.Collections.Generic;
using SOScripts;
using UnityEngine;

namespace LevelFileSystem
{
    public class LevelLoadHandler : MonoBehaviour
    {
  
        private List<UtilsClass.LevelPlaceListObject> _placedLevelObjectsList;
        public PlacedListSO _PlacedListSo;
        List<string> _toWrite; //string list (item per line ) to be written to a file
        // Update is called once per frame

        public bool instantLoad;
        private void Start()
        {
            _placedLevelObjectsList = _PlacedListSo.listOfObjects;
            if (instantLoad)
            {
                LoadLevel(GlobalValues.currentLevel.levelData);
            }
        }


        private void LoadLevel(string levelData)
        {
        
            foreach (var line in levelData.Split('\n'))
            {
                var record = line.Split(','); //split each line by cells ( , )
                Transform toBeBuilt = _placedLevelObjectsList.Find(x => x.index == int.Parse(record[0])).PlacedObjectTypeSo.prefab;
                Vector3 positionVect = new Vector3(float.Parse(record[1]), float.Parse(record[2]), float.Parse(record[3]));
                Vector3 qEulers = new Vector3(float.Parse(record[4]), float.Parse(record[5]), float.Parse(record[6]));
                Quaternion quaternion = Quaternion.Euler(qEulers); 
                Transform newObject = (Transform) Instantiate(toBeBuilt, positionVect, quaternion, transform);
            }
        }
    
    }
}
   

