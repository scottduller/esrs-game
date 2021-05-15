using System;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Services;
using Assets.Scripts.Api.Models;
using Assets.Scripts.Api.Services;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;
using UnityEngine.UI;
using Random = UnityEngine.Random;


namespace LevelFileSystem
{
    public enum BrowserType
    {
        USERLEVELS = 1,
        ALLLEVELS =2,
        RANDOM = 3
    }
    public class LevelSelection : MonoBehaviour
    {

        public BrowserType browserType;
        public Transform buttonTemplate;
        public LevelServices levelServices;
        public UserServices userServices;
        private Dictionary<Level, Transform> _levelButtons;
        private Transform _contentLocation;
        private List<Level> _levels;
        public TMP_Dropdown _sort;
        public TMP_InputField _search;

        public AppSceneManager appSceneManager;
        // Start is called before the first frame update

        private void Start()
        {
            if (buttonTemplate) buttonTemplate.gameObject.SetActive(false);
            switch (browserType)
            {
                case BrowserType.USERLEVELS:
                    levelServices.GetUsersLevels(SetLevels);
                    break;
                case BrowserType.ALLLEVELS:
                    levelServices.GetAllLevels(SetLevels);
                    break;
                case BrowserType.RANDOM:
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        private void UpDateUserName(User user, string id)
        {
            foreach (Level level in _levels)
            {
                level.user = id == level.user ? user.username : level.user;
            }
        }

        private void SetLevels(List<Level> levels)
        {
            _levelButtons = new Dictionary<Level, Transform>();
            _sort.onValueChanged.AddListener((int i) => UpdateButtons());
            _search.onValueChanged.AddListener((string s) => UpdateButtons());
            _contentLocation = buttonTemplate.parent;
            if (levels != null)
            {
                _levels = levels;
                foreach (string id in _levels.ConvertAll(x =>  x.user).Distinct().ToList())
                {
                    // Debug.Log(id);
                    userServices.GetUserById(id, (User user) =>  UpDateUserName(user,id));

                }

                
                // Debug.Log(levels.Count);
                
                Invoke(nameof(UpdateButtons), 0.5f);
            }
            else
            {
                Debug.Log("ERROR: Cant Get Levels");
            }
        }

        private List<Level> SortLevels(List<Level> levels)
        {
            switch (_sort.value)
            {
                case 1:
                    levels.Sort((x, y) => (int.Parse(x.votes).CompareTo(int.Parse(y.votes))));
                    break;
                case 2:
                    levels.Sort((x, y) => (int.Parse(y.votes).CompareTo(int.Parse(x.votes))));
                    break;
                case 3:
                    levels.Sort((x, y) => (string.Compare(y.name, x.name, StringComparison.CurrentCultureIgnoreCase)));

                    break;
                case 4: 
                    levels.Sort((x, y) => (string.Compare(x.name, y.name, StringComparison.CurrentCultureIgnoreCase)));

                    break;
                case 5:
                    levels.Sort((x, y) => (string.Compare(x.createdAt, y.updatedAt, StringComparison.CurrentCultureIgnoreCase)));
                    break;
                case 6:
                    levels.Sort((x, y) => (string.Compare(y.createdAt, x.updatedAt, StringComparison.CurrentCultureIgnoreCase)));
                    break;
                default:
                    break;
            }

            return levels;

        }


        private void UpdateButtons()
        {
            if (_levelButtons.Count != 0)
            {
                foreach (Transform button in _levelButtons.Values)
                {
                    Destroy(button.gameObject);
                }

                _levelButtons.Clear();
            }



            List<Level> currentSearch = _levels.FindAll(x => x.name.ToLower().Contains(_search.text.ToLower()) || x.user.ToLower().Contains(_search.text.ToLower()) );
            currentSearch = SortLevels(currentSearch);
            foreach (Level level in currentSearch)
            {
                Transform btnTransform = Instantiate(buttonTemplate, _contentLocation);
                btnTransform.gameObject.SetActive(true);
                btnTransform.Find("Outline/Background/LevelName").GetComponent<TextMeshProUGUI>().SetText(level.name);
                btnTransform.Find("Outline/Background/Desc").GetComponent<TextMeshProUGUI>().SetText(level.description);
                btnTransform.Find("Outline/Background/User").GetComponent<TextMeshProUGUI>().SetText(level.user);
                btnTransform.Find("Outline/Background/Rating").GetComponent<TextMeshProUGUI>().SetText(level.votes);
                btnTransform.Find("Outline").GetComponent<Button>().onClick.AddListener(() =>
                    {
                        LoadLevel(level);
                    });
                _levelButtons[level] = btnTransform;
                
            }

        }
        

        public void GetARandomLevel()
        {
            levelServices.GetAllLevels(SetRandomLevel);
        }

        private void SetRandomLevel(List<Level> result)
        {
            _levelButtons = new Dictionary<Level, Transform>();
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
