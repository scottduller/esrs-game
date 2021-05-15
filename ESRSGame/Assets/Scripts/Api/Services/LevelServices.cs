using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Utils;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Networking;

namespace Api.Services
{
    public class LevelServices : MonoBehaviour
    {

        public void GetAllLevels(Action<List<Level>> result)
        {
            StartCoroutine(GetAllLevelsRoutine(result));
        }

        public void GetUsersLevels(Action<List<Level>> result)
        {
            StartCoroutine(GetUsersLevelsRoutine(result));
        }

        public void GetLevelById(string levelId, Action<Level> result)
        {
            if (!string.IsNullOrEmpty(levelId))
            {
                StartCoroutine(GetLevelByIdRoutine(levelId, result));
            }
        }

        public void CreateLevel(string createLevelName, string createLevelDesc, string createLevelData, Action<bool> result)
        {
            if (!string.IsNullOrEmpty(createLevelName) && !string.IsNullOrEmpty(createLevelData))
            {
                StartCoroutine(CreateLevelRoutine(createLevelName,createLevelDesc, "0",createLevelData, result));
            }
        }

        //need 
        public void UpdateLevel(string updateLevelId,
            string updateLevelData = null,
            string updateLevelVotes = null, string updateLevelName = null, string updateLevelDesc = null)
        {
            if (!string.IsNullOrEmpty(updateLevelId))
            {
                StartCoroutine(UpdateLevelRoutine(updateLevelId,updateLevelData, updateLevelVotes, updateLevelName, updateLevelDesc));
            }
        }

        public void DeleteLevel(string deleteLevelId)
        {
            if (!string.IsNullOrEmpty(deleteLevelId))
            {
                StartCoroutine(DeleteLevelRoutine(deleteLevelId));
            }
        }

        private IEnumerator GetAllLevelsRoutine(Action<List<Level>> result)
        {
            UnityWebRequest request = WebServices.Get("levels/");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get All Levels", request.error, "Ok");
                result(null);
            }
            else
            {
                Level[] levels = JsonHelper.FromJson<Level>(request.downloadHandler.text);
                
                string outStr = "";
                foreach (Level level in levels)
                {
                    outStr += level + Environment.NewLine + "-------------------" + Environment.NewLine;
                }

                // EditorUtility.DisplayDialog("Get All Levels", outStr, "Ok");
                result(levels.ToList());
            }
        }

        private IEnumerator GetLevelByIdRoutine(string levelId, Action<Level> result)
        {
            UnityWebRequest request = WebServices.Get($"levels/{levelId}");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get Level By Id", request.error, "Ok");
                result(null);
            }
            else
            {
                Level level = JsonUtility.FromJson<Level>(request.downloadHandler.text);
                result(level);
                // EditorUtility.DisplayDialog("Get Level By Id", level.ToString(), "Ok");
            }
        }

        private IEnumerator GetUsersLevelsRoutine(Action<List<Level>> result)
        {
            UnityWebRequest request = WebServices.Get("levels/user");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                result(null);
                // EditorUtility.DisplayDialog("Get Users Levels", request.error, "Ok");
            }
            else
            {
                Level[] levels = JsonHelper.FromJson<Level>(request.downloadHandler.text);

                // string outStr = levels.Aggregate("", (current, level) => current + (level + Environment.NewLine + "-------------------" + Environment.NewLine));
                result(levels.ToList());
                // EditorUtility.DisplayDialog("Get Users Levels", outStr, "Ok");
            }
        }

        private IEnumerator CreateLevelRoutine(string createLevelName, string createLevelDesc, string createLevelVotes, string createLevelData, Action<bool> result)
        {
            Level newLevel = new Level(createLevelName, createLevelDesc, createLevelVotes,
                createLevelData);

            string jsonLevel = JsonUtility.ToJson(newLevel);

            UnityWebRequest request = WebServices.Post("levels", jsonLevel);
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Create Level", request.error, "Ok");
                result(false);
            }
            else
            {
                Level level = JsonUtility.FromJson<Level>(request.downloadHandler.text);
                // EditorUtility.DisplayDialog("Create Level", level.ToString(), "Ok");
                result(true);
            }
        }

        private IEnumerator UpdateLevelRoutine(string updateLevelId,
            string updateLevelData = null,
            string updateLevelVotes = null, string updateLevelName = null, string updateLevelDesc = null)
        {
            var getRequest = WebServices.Get($"levels/{updateLevelId}");
            yield return getRequest.SendWebRequest();

            if (getRequest.isNetworkError | getRequest.responseCode >= 300)
            {
                Debug.LogError(getRequest.downloadHandler.text);
                EditorUtility.DisplayDialog("Update Level", getRequest.error, "Ok");
            }
            else
            {
                Level originalLevel = JsonUtility.FromJson<Level>(getRequest.downloadHandler.text);

                string updatedName = string.IsNullOrEmpty(updateLevelName)
                    ? originalLevel.name
                    : updateLevelName;
                string updatedDesc = string.IsNullOrEmpty(updateLevelDesc)
                    ? originalLevel.description
                    : updateLevelDesc;
                string updatedVotes = string.IsNullOrEmpty(updateLevelVotes)
                    ? originalLevel.votes
                    : updateLevelVotes;
                string updatedLevelData = string.IsNullOrEmpty(updateLevelData)
                    ? originalLevel.levelData
                    : updateLevelData;

                Level newLevel = new Level(updatedName, updatedDesc, updatedVotes,
                    updatedLevelData);

                string jsonLevel = JsonUtility.ToJson(newLevel);

                UnityWebRequest request = WebServices.Put($"levels/{updateLevelId}", jsonLevel);
                yield return request.SendWebRequest();

                if (request.isNetworkError | request.responseCode >= 300)
                {
                    Debug.LogError(request.downloadHandler.text);
                    EditorUtility.DisplayDialog("Update Level", request.error, "Ok");
                }
                else
                {
                    Level level = JsonUtility.FromJson<Level>(request.downloadHandler.text);
                    EditorUtility.DisplayDialog("Update Level", level.ToString(), "Ok");
                }
            }
        }

        private IEnumerator DeleteLevelRoutine(string deleteLevelId)
        {
            UnityWebRequest request = WebServices.Delete($"levels/{deleteLevelId}");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Delete Level", request.error, "Ok");
            }
            else
            {
                EditorUtility.DisplayDialog("Delete Level", request.downloadHandler.text, "Ok");
            }
        }
    }
}