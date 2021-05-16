using System;
using Api.Services;
using UnityEngine;
using UnityEngine.UI;

namespace LevelFileSystem
{
    
    public class LevelRating : MonoBehaviour
    {
        private Button _dislike;
        private Button _like;
        public LevelServices levelServices;
        private void Awake()
        {
            _dislike = transform.Find("HorizontalItems/Down").GetComponent<Button>();
            _like = transform.Find("HorizontalItems/Up").GetComponent<Button>();
            
            _dislike.onClick.AddListener( () =>
                {
                    levelServices.UpdateLevel(GlobalValues.currentLevel._id,
                            updateLevelVotes: (int.Parse(GlobalValues.currentLevel.votes) - 1).ToString());
                    gameObject.SetActive(false);
                });
            
            _like.onClick.AddListener( () =>
            {
                levelServices.UpdateLevel(GlobalValues.currentLevel._id,
                    updateLevelVotes: (int.Parse(GlobalValues.currentLevel.votes) + 1).ToString());
                gameObject.SetActive(false);
            });
                
                
        }
    }
}
