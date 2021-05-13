using System;
using System.Collections;
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
        public TMP_InputField levelId;

        public TMP_InputField createLevelName;
        public TMP_InputField createLevelDesc;
        public TMP_InputField createLevelVotes;
        public TMP_InputField createLevelData;

        public TMP_InputField updateLevelId;
        public TMP_InputField updateLevelName;
        public TMP_InputField updateLevelDesc;
        public TMP_InputField updateLevelVotes;
        public TMP_InputField updateLevelData;

        public TMP_InputField deleteLevelId;

        public void GetAllLevels()
        {
            StartCoroutine(GetAllLevelsRoutine());
        }

        public void GetUsersLevels()
        {
            StartCoroutine(GetUsersLevelsRoutine());
        }

        public void GetLevelById()
        {
            if (!string.IsNullOrEmpty(levelId.text))
            {
                StartCoroutine(GetLevelByIdRoutine());
            }
        }

        public void CreateLevel()
        {
            if (!string.IsNullOrEmpty(createLevelName.text) && !string.IsNullOrEmpty(createLevelData.text))
            {
                StartCoroutine(CreateLevelRoutine());
            }
        }

        public void UpdateLevel()
        {
            if (!string.IsNullOrEmpty(updateLevelId.text))
            {
                StartCoroutine(UpdateLevelRoutine());
            }
        }

        public void DeleteLevel()
        {
            if (!string.IsNullOrEmpty(deleteLevelId.text))
            {
                StartCoroutine(DeleteLevelRoutine());
            }
        }

        private IEnumerator GetAllLevelsRoutine()
        {
            var request = WebServices.Get("levels/");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get All Levels", request.error, "Ok");
            }
            else
            {
                Level[] levels = JsonHelper.FromJson<Level>(request.downloadHandler.text);

                string outStr = "";
                foreach (var level in levels)
                {
                    outStr += level + Environment.NewLine + "-------------------" + Environment.NewLine;
                }

                EditorUtility.DisplayDialog("Get All Levels", outStr, "Ok");
            }
        }

        private IEnumerator GetLevelByIdRoutine()
        {
            var request = WebServices.Get($"levels/{levelId.text}");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get Level By Id", request.error, "Ok");
            }
            else
            {
                Level level = JsonUtility.FromJson<Level>(request.downloadHandler.text);

                EditorUtility.DisplayDialog("Get Level By Id", level.ToString(), "Ok");
            }
        }

        private IEnumerator GetUsersLevelsRoutine()
        {
            var request = WebServices.Get("levels/user");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get Users Levels", request.error, "Ok");
            }
            else
            {
                Level[] levels = JsonHelper.FromJson<Level>(request.downloadHandler.text);

                string outStr = "";
                foreach (Level level in levels)
                {
                    outStr += level + Environment.NewLine + "-------------------" + Environment.NewLine;
                }

                EditorUtility.DisplayDialog("Get Users Levels", outStr, "Ok");
            }
        }

        private IEnumerator CreateLevelRoutine()
        {
            Level newLevel = new Level(createLevelName.text, createLevelDesc.text, createLevelVotes.text,
                createLevelData.text);

            string jsonLevel = JsonUtility.ToJson(newLevel);

            UnityWebRequest request = WebServices.Post("levels", jsonLevel);
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Create Level", request.error, "Ok");
            }
            else
            {
                Level level = JsonUtility.FromJson<Level>(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Create Level", level.ToString(), "Ok");
            }
        }

        private IEnumerator UpdateLevelRoutine()
        {
            var getRequest = WebServices.Get($"levels/{updateLevelId.text}");
            yield return getRequest.SendWebRequest();

            if (getRequest.isNetworkError | getRequest.responseCode >= 300)
            {
                Debug.LogError(getRequest.downloadHandler.text);
                EditorUtility.DisplayDialog("Update Level", getRequest.error, "Ok");
            }
            else
            {
                Level originalLevel = JsonUtility.FromJson<Level>(getRequest.downloadHandler.text);

                string updatedName = string.IsNullOrEmpty(updateLevelName.text)
                    ? originalLevel.name
                    : updateLevelName.text;
                string updatedDesc = string.IsNullOrEmpty(updateLevelDesc.text)
                    ? originalLevel.description
                    : updateLevelDesc.text;
                string updatedVotes = string.IsNullOrEmpty(updateLevelVotes.text)
                    ? originalLevel.votes
                    : updateLevelVotes.text;
                string updatedLevelData = string.IsNullOrEmpty(updateLevelData.text)
                    ? originalLevel.levelData
                    : updateLevelData.text;

                Level newLevel = new Level(updatedName, updatedDesc, updatedVotes,
                    updatedLevelData);

                string jsonLevel = JsonUtility.ToJson(newLevel);

                var request = WebServices.Put($"levels/{updateLevelId.text}", jsonLevel);
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

        private IEnumerator DeleteLevelRoutine()
        {
            var request = WebServices.Delete($"levels/{deleteLevelId.text}");
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