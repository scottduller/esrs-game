using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Api.Models;
using Api.Utils;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Api.Services
{
    public class PlaylistServices : MonoBehaviour
    {
        public TMP_InputField playlistId;

        public TMP_InputField createPlaylistName;
        public TMP_InputField createPlaylistLevels;

        public TMP_InputField updatePlaylistId;
        public TMP_InputField updatePlaylistName;
        public TMP_InputField updatePlaylistLevels;

        public TMP_InputField deletePlaylistId;

        public void GetAllPlaylists()
        {
            StartCoroutine(GetAllPlaylistsRoutine());
        }

        public void GetUsersPlaylists()
        {
            StartCoroutine(GetUsersPlaylistsRoutine());
        }

        public void GetPlaylistById()
        {
            if (!string.IsNullOrEmpty(playlistId.text))
            {
                StartCoroutine(GetPlaylistByIdRoutine());
            }
        }

        public void CreatePlaylist()
        {
            if (!string.IsNullOrEmpty(createPlaylistName.text) && !string.IsNullOrEmpty(createPlaylistLevels.text))
            {
                StartCoroutine(CreatePlaylistRoutine());
            }
        }

        public void UpdatePlaylist()
        {
            if (!string.IsNullOrEmpty(updatePlaylistId.text))
            {
                StartCoroutine(UpdatePlaylistRoutine());
            }
        }

        public void DeletePlaylist()
        {
            if (!string.IsNullOrEmpty(deletePlaylistId.text))
            {
                StartCoroutine(DeletePlaylistRoutine());
            }
        }

        private IEnumerator GetAllPlaylistsRoutine()
        {
            var request = WebServices.Get("playlists/");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get All Playlists", request.error, "Ok");
            }
            else
            {
                Playlist[] playlists = JsonHelper.FromJson<Playlist>(request.downloadHandler.text);

                string outStr = "";
                foreach (var playlist in playlists)
                {
                    outStr += playlist + Environment.NewLine + "-------------------" + Environment.NewLine;
                }

                EditorUtility.DisplayDialog("Get All Playlists", outStr, "Ok");
            }
        }

        private IEnumerator GetPlaylistByIdRoutine()
        {
            var request = WebServices.Get($"playlists/{playlistId.text}");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get Playlist By Id", request.error, "Ok");
            }
            else
            {
                Playlist playlist = JsonUtility.FromJson<Playlist>(request.downloadHandler.text);

                EditorUtility.DisplayDialog("Get Playlist By Id", playlist.ToString(), "Ok");
            }
        }

        private IEnumerator GetUsersPlaylistsRoutine()
        {
            var request = WebServices.Get("playlists/user");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get Users Playlists", request.error, "Ok");
            }
            else
            {
                Playlist[] playlists = JsonHelper.FromJson<Playlist>(request.downloadHandler.text);

                string outStr = "";
                foreach (var playlist in playlists)
                {
                    outStr += playlist + Environment.NewLine + "-------------------" + Environment.NewLine;
                }

                EditorUtility.DisplayDialog("Get Users Playlists", outStr, "Ok");
            }
        }

        private IEnumerator CreatePlaylistRoutine()
        {
            Debug.Log(createPlaylistLevels.text);
            
            var levelRequest = WebServices.Get("levels/");
            yield return levelRequest.SendWebRequest();
            
            Level[] levels = JsonHelper.FromJson<Level>(levelRequest.downloadHandler.text);

            List<string> ids = new List<string>();
            foreach (var level in levels)
            {
                ids.Add(level._id);
            }

            string[] idsArray = ids.ToArray();
            
            
            Playlist newPlaylist = new Playlist(createPlaylistName.text,
                idsArray);

            string jsonPlaylist = JsonUtility.ToJson(newPlaylist);
            
            // jsonPlaylist = jsonPlaylist.Replace(@"\", null);

            Debug.Log(jsonPlaylist);
            
            var request = WebServices.Post("playlists", jsonPlaylist);
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Create Playlist", request.error, "Ok");
            }
            else
            {
                Playlist playlist = JsonUtility.FromJson<Playlist>(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Create Playlist", playlist.ToString(), "Ok");
            }
        }

        private IEnumerator UpdatePlaylistRoutine()
        {
            var getRequest = WebServices.Get($"playlists/{updatePlaylistId.text}");
            yield return getRequest.SendWebRequest();
        
            if (getRequest.isNetworkError | getRequest.responseCode >= 300)
            {
                Debug.LogError(getRequest.downloadHandler.text);
                EditorUtility.DisplayDialog("Update Playlist", getRequest.error, "Ok");
            }
            else
            {
                Playlist originalPlaylist = JsonUtility.FromJson<Playlist>(getRequest.downloadHandler.text);
        
                string updatedName = string.IsNullOrEmpty(updatePlaylistName.text)
                    ? originalPlaylist.name
                    : updatePlaylistName.text;
 
                Debug.Log(createPlaylistLevels.text);
            
                var levelRequest = WebServices.Get("levels/");
                yield return levelRequest.SendWebRequest();
            
                Level[] levels = JsonHelper.FromJson<Level>(levelRequest.downloadHandler.text);

                List<string> ids = new List<string>();
                foreach (var level in levels)
                {
                    ids.Add(level._id);
                }

                string[] idsArray = ids.ToArray();
        
                Playlist newPlaylist = new Playlist(updatedName,
                    idsArray);
        
                string jsonPlaylist = JsonUtility.ToJson(newPlaylist);
        
                var request = WebServices.Put($"playlists/{updatePlaylistId.text}", jsonPlaylist);
                yield return request.SendWebRequest();
        
                if (request.isNetworkError | request.responseCode >= 300)
                {
                    Debug.LogError(request.downloadHandler.text);
                    EditorUtility.DisplayDialog("Update Playlist", request.error, "Ok");
                }
                else
                {
                    Playlist playlist = JsonUtility.FromJson<Playlist>(request.downloadHandler.text);
                    EditorUtility.DisplayDialog("Update Playlist", playlist.ToString(), "Ok");
                }
            }
        }

        private IEnumerator DeletePlaylistRoutine()
        {
            var request = WebServices.Delete($"playlists/{deletePlaylistId.text}");
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Delete Playlist", request.error, "Ok");
            }
            else
            {
                EditorUtility.DisplayDialog("Delete Playlist", request.downloadHandler.text, "Ok");
            }
        }
    }
}