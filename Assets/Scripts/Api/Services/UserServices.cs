using System;
using System.Collections;
using Api.Models;
using Api.Utils;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace Api.Services
{
    public class UserServices : MonoBehaviour
    {
        public TMP_InputField registerUsername, registerPassword;
        
        public TMP_InputField loginUsername, loginPassword;
        
        public TMP_InputField userId;

        public void RegisterUser()
        {
            if (!string.IsNullOrEmpty(registerUsername.text) && !string.IsNullOrEmpty(registerPassword.text))
            {
                StartCoroutine(RegisterUserRoutine());
            }
        }

        public void LoginUser()
        {
            if (!string.IsNullOrEmpty(loginUsername.text) && !string.IsNullOrEmpty(loginPassword.text))
            {
                StartCoroutine(LoginUserRoutine());
            }
        }

        public void GetAllUsers()
        {
            StartCoroutine(GetAllUsersRoutine());
        }

        public void GetUserById()
        {
            if (!string.IsNullOrEmpty(userId.text))
            {
                StartCoroutine(GetUserByIdRoutine());
            }
        }

        private IEnumerator RegisterUserRoutine()
        {
            var user = new User(registerUsername.text, registerPassword.text);

            var request = WebServices.Post("auth/register", JsonUtility.ToJson(user));
            yield return request.SendWebRequest();

            Debug.Log(request.responseCode);

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Register User", request.error, "Ok");
            }
            else
            {
                User registeredUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Register User", registeredUser.ToString(), "Ok");
            }
        }

        private IEnumerator LoginUserRoutine()
        {
            var user = new User(loginUsername.text, loginPassword.text);

            // Delete cookie before requesting a new login
            WebServices.AuthString = null;

            var request = WebServices.Post("auth/login", JsonUtility.ToJson(user));
            yield return request.SendWebRequest();

            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Login User", request.error, "Ok");
            }
            else
            {
                User loggedInUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                WebServices.AuthString = loggedInUser.token;
                EditorUtility.DisplayDialog("Login User", loggedInUser.ToString(), "Ok");
            }
        }

        private IEnumerator GetUserByIdRoutine()
        {
            var request = WebServices.Get($"users/{userId.text}");
            yield return request.SendWebRequest();
    
            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get User By Id", request.error, "Ok");
            }
            else
            {
                User user = JsonUtility.FromJson<User>(request.downloadHandler.text);

                EditorUtility.DisplayDialog("Get User By Id", user.ToString(), "Ok");
            }
        }
        
        private IEnumerator GetAllUsersRoutine()
        {
            var request = WebServices.Get("users/");
            yield return request.SendWebRequest();
    
            if (request.isNetworkError | request.responseCode >= 300)
            {
                Debug.LogError(request.downloadHandler.text);
                EditorUtility.DisplayDialog("Get All Users", request.error, "Ok");
            }
            else
            {
                User[] users = JsonHelper.FromJson<User>(request.downloadHandler.text);

                string outStr ="";
                foreach (var user in users)
                {
                    outStr += user + Environment.NewLine + "-------------------" + Environment.NewLine;
                }
                EditorUtility.DisplayDialog("Get All Users", outStr, "Ok");
            }
        }
    }
}