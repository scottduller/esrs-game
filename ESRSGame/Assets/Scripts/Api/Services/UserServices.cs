using System;
using System.Collections;
using Api.Services;
using Api.Utils;
using Assets.Scripts.Api.Models;
using UnityEditor;
using UnityEngine;

namespace Assets.Scripts.Api.Services
{
    public class UserServices: MonoBehaviour
    {
        private bool _success;

 
        
    public bool RegisterUser(string registerUsername, string registerPassword)
    {
        this._success = false;
        if (!string.IsNullOrEmpty(registerUsername) && !string.IsNullOrEmpty(registerPassword))
        {
            StartCoroutine(RegisterUserRoutine(registerUsername, registerPassword));
        }
        return this._success;
    }

    public bool LoginUser(string loginUsername, string loginPassword, Action<User> result )
    {
        this._success = false;
        if (!string.IsNullOrEmpty(loginUsername) && !string.IsNullOrEmpty(loginPassword))
        {
            StartCoroutine(LoginUserRoutine(loginUsername,loginPassword, result));
        }

        return this._success;
    }

    public void GetAllUsers()
    {
        this._success = false;
        StartCoroutine(GetAllUsersRoutine());
    }

    public void GetUserById(string userId, Action<User> result)
    {
        if (!string.IsNullOrEmpty(userId))
        {
            StartCoroutine(GetUserByIdRoutine(userId, result));
        }
    }

    private  IEnumerator RegisterUserRoutine(string registerUsername, string registerPassword)
    {
        var user = new User(registerUsername, registerPassword);

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

    private IEnumerator LoginUserRoutine(string loginUsername, string loginPassword, Action<User> result)
    {
        User user = new User(loginUsername, loginPassword);

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
            result(loggedInUser);
            WebServices.AuthString = loggedInUser.token;
            // EditorUtility.DisplayDialog("Login User", loggedInUser.ToString(), "Ok");
            
        }
    }

    private IEnumerator GetUserByIdRoutine(string userId, Action<User> result)
    {
        var request = WebServices.Get($"users/{userId}");
        yield return request.SendWebRequest();

        if (request.isNetworkError | request.responseCode >= 300)
        {
            Debug.LogError(request.downloadHandler.text);
            EditorUtility.DisplayDialog("Get User By Id", request.error, "Ok");
            result(null);
        }
        else
        {
            User user = JsonUtility.FromJson<User>(request.downloadHandler.text);

            // EditorUtility.DisplayDialog("Get User By Id", user.ToString(), "Ok");
            this._success = true;
            result(user);
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

            string outStr = "";
            foreach (var user in users)
            {
                outStr += user + Environment.NewLine + "-------------------" + Environment.NewLine;
            }

            EditorUtility.DisplayDialog("Get All Users", outStr, "Ok");
        }
    }
    }
}