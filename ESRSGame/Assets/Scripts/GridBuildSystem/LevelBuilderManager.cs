using System;
using System.Collections.Generic;
using System.Linq;
using LevelFileSystem;
using SOScripts;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;

namespace GridBuildSystem
{
    public class LevelBuilderManager : MonoBehaviour
    {
        
        public static LevelBuilderManager Instance { get; private set; }


        public Button topButton;
        public Button floorButton;
        public Color selectedColour;
        private Color _initialColour;
        private GridBuildingSystem _floorLayer;
        private GridBuildingSystem _topLayer;
        private GridBuildingSystem _activeLayer;
        private List<UtilsClass.LevelPlaceListObject> _placedLevelObjectsList;
        private List<PlacedObjectTypeSO> _currentLayerPlacedObjects;
        public PlacedListSO placedListSo;
        private Transform _colliderPlane;


        public int levelWidth =20;
        public int levelHeight = 20;
        public float cellSize = 1f;
        
        public event EventHandler<OnActiveLayerChangeArgs> OnActiveLayerChange;
    
        public class  OnActiveLayerChangeArgs :EventArgs
        {
            public GridBuildingSystem ActiveLayer;
            public List<PlacedObjectTypeSO> Objects;
        };

        private void Awake()
        {
            Instance = this;
            try
            {

                _floorLayer = transform.Find("GridManagerFloor").GetComponent<GridBuildingSystem>();
                _topLayer = transform.Find("GridManagerInteractable").GetComponent<GridBuildingSystem>();
                _colliderPlane = transform.Find("ColliderPlane");
                _activeLayer = _floorLayer;
                _placedLevelObjectsList = placedListSo.listOfObjects;
                
                


            }
            
            
            catch (Exception e)
            {
                Debug.LogError(e);
                throw;
            }

            _floorLayer.InitializeGrid(levelWidth, levelHeight, cellSize, Vector3.zero, 1);
            _topLayer.InitializeGrid(levelWidth,levelHeight,cellSize,new Vector3(0,1,0),0);
            _floorLayer.SetInteractable(true);
            _topLayer.SetLowerGrid(_floorLayer.GETGrid());
            _initialColour = topButton.image.color;
            floorButton.image.color = selectedColour;

        }

        private void Start()
        {
            List<UtilsClass.LevelPlaceListObject> tempItemList = _placedLevelObjectsList.Where(x => x.isFloor == true).ToList();
            _currentLayerPlacedObjects = tempItemList.ConvertAll((x) => x.PlacedObjectTypeSo);
            // Debug .Log(currentLayerPlacedObjects.Count);
            OnActiveLayerChange?.Invoke(this,new OnActiveLayerChangeArgs{ActiveLayer = _activeLayer, Objects = _currentLayerPlacedObjects});
            

        }
        





        public void SwitchLayer(int layerValue)
        {
            List<UtilsClass.LevelPlaceListObject> tempItemList;
            switch (layerValue)
            {
                case 0:
                    _topLayer.SetInteractable(false);
                    _floorLayer.SetInteractable(true);
                    topButton.image.color = _initialColour;
                    floorButton.image.color = selectedColour;
                    _activeLayer = _floorLayer;
                    _colliderPlane.position = new Vector3(10, 0, 10);
                    tempItemList = _placedLevelObjectsList.Where(x => x.isFloor == true).ToList();
                    _currentLayerPlacedObjects = tempItemList.ConvertAll((x) => x.PlacedObjectTypeSo);
    
                    OnActiveLayerChange?.Invoke(this,new OnActiveLayerChangeArgs{ActiveLayer = this._activeLayer, Objects = _currentLayerPlacedObjects});
                    break;
                case 1:
                    _floorLayer.SetInteractable(false);
                    _topLayer.SetInteractable(true);
                    topButton.image.color = selectedColour;
                    floorButton.image.color = _initialColour;
                    _activeLayer = _topLayer;
                    _colliderPlane.position = new Vector3(10, 1, 10);
                    tempItemList = _placedLevelObjectsList.Where(x => x.isInteractable == true).ToList();
                    _currentLayerPlacedObjects = tempItemList.ConvertAll((x) => x.PlacedObjectTypeSo);
                    OnActiveLayerChange?.Invoke(this,new OnActiveLayerChangeArgs{ActiveLayer = this._activeLayer, Objects = _currentLayerPlacedObjects});
                    break;
                case 2:
                    break;
                
            }
        }

        public GridBuildingSystem GetActiveGrid() => _activeLayer;
        
        public bool CheckForRequirements()
        {
            bool valid = true;
            List<PlacedGridObject> floorItems =
                transform.Find("GridManagerFloor").GetComponentsInChildren<PlacedGridObject>().ToList();
            List<PlacedGridObject> interactiveItems  =
                transform.Find("GridManagerInteractable").GetComponentsInChildren<PlacedGridObject>().ToList();
            
            
            if (interactiveItems
                .FindAll(x => x.GETPlacedObjectTypeSo().objectType == PlacedObjectTypeSO.ObjectType.START).Count != 1)
            {
                Debug.Log("NEEDS 1 START POS");
                valid = false;
            }

            if (interactiveItems
                    .FindAll(x => x.GETPlacedObjectTypeSo().objectType == PlacedObjectTypeSO.ObjectType.END).Count ==
                1) return valid;
            Debug.Log("NEEDS 1 END POS");
            valid = false;

            return valid; 
        }
        
        public void SaveLevel(string title, string desc)
        {

            transform.GetComponent<LevelSaveHandler>().WriteLevelToFile(title, desc,
                transform.Find("GridManagerFloor").GetComponentsInChildren<PlacedGridObject>().ToList(),
                transform.Find("GridManagerInteractable").GetComponentsInChildren<PlacedGridObject>().ToList());
        }

        public int GETIndexFromSo(PlacedObjectTypeSO so)
        {
           return _placedLevelObjectsList.Find((x) => x.PlacedObjectTypeSo.nameString == so.nameString).index;
        }

    }
}
