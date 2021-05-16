using System.Collections.Generic;
using Api.Models;
using Api.Services;
using GridBuildSystem;
using SOScripts;
using UnityEngine;

namespace LevelFileSystem
{
    public class LevelLoadHandler : MonoBehaviour
    {
        public Vector3 levelScale = new Vector3(5, 5, 5);
        public Vector3 StartOffset = new Vector3(0,1,0);
        public MeshCombiner meshCombiner;
        
        
        private Transform _startLocation;
        public Transform PlayerObject;
        private List<UtilsClass.LevelPlaceListObject> _placedLevelObjectsList;
        public PlacedListSO _PlacedListSo;
        [Header("TESTING")]
        public LevelServices _LevelServices;
        private string levelId =  "60a02eeb7cf13200230bdbef";
        [Header("WieldSettings")] 
        public bool useWield;

        public float threshold = 0.1f;

        public float bucketStep = 0.01f;
        // Update is called once per frame

        public bool instantLoad;
        private void Start()
        {
            _placedLevelObjectsList = _PlacedListSo.listOfObjects;
            if (instantLoad)
            {
                if (GlobalValues.currentLevel != null)
                {
                    LoadLevel(GlobalValues.currentLevel.levelData);

                }
                else
                {
                    _LevelServices.GetLevelById(levelId, (Level result) =>
                    {
                        if (result != null)
                        {
                            LoadLevel(result.levelData);

                        }
                        else
                        {
                            Debug.Log("UNABLE TO FIND LEVEL");
                        }
                    });
                }
            }
        }


        private void LoadLevel(string levelData)
        {
        
            foreach (var line in levelData.Split('\n'))
            {
                if(line == string.Empty) continue;
                
                var record = line.Split(','); //split each line by cells ( , )
                PlacedObjectTypeSO objectTypeSo = _placedLevelObjectsList.Find(x => x.index == int.Parse(record[0])).PlacedObjectTypeSo;
                Transform toBeBuilt = objectTypeSo.prefab;
                Vector3 positionVect = new Vector3(float.Parse(record[1]), float.Parse(record[2]), float.Parse(record[3]));
                Vector3 qEulers = new Vector3(float.Parse(record[4]), float.Parse(record[5]), float.Parse(record[6]));
                Quaternion quaternion = Quaternion.Euler(qEulers); 
                Transform newObject = (Transform) Instantiate(toBeBuilt, positionVect, quaternion, transform);
                PlacedGridObject placedObject = newObject.GetComponent<PlacedGridObject>();
                placedObject.SetPlacedObjectTypeSo(objectTypeSo);
                if (objectTypeSo.objectType == PlacedObjectTypeSO.ObjectType.START)
                {
                    _startLocation = newObject;
                }
            }

            transform.localScale = levelScale;
            meshCombiner.AdvancedMerge(gameObject, useWield, threshold,bucketStep);
            PlayerObject.position = _startLocation.position + StartOffset;

        }
    
    }
}
   

