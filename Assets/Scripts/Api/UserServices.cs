using System;
using System.Collections;
using System.Net.Http;
using System.Text;
using Models;
using UnityEngine;
namespace Api
{
    public class UserServices : MonoBehaviour
    {
        // !HARDCODED! Can be changed to account for text/buttons input
        public string username = "username1";
        public string password = "password1";

        // Takes username password and returns the user (w/ hashed password)
        public void RegisterUser()
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                StartCoroutine(PostRegisterUser());
            }
        }
        
        // Takes username and password and return a signed JWT token for authorisation
        // This is assigned as a bearer token in the WebServices object to be used for all other calls till the game is closed.
        public void LoginUser()
        {
            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(password))
            {
                StartCoroutine(PostLoginUser());
            }
        }

        private IEnumerator PostRegisterUser()
        {
            var user = new User(username, password);

            var request = WebServices.Post("auth/register", JsonUtility.ToJson(user));
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
            }
        }
        
        private IEnumerator PostLoginUser()
        {
            var user = new User(username, password);

            // Delete cookie before requesting a new login
            WebServices.AuthString = null;

            var request = WebServices.Post("auth/login", JsonUtility.ToJson(user));
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                User loggedInUser = JsonUtility.FromJson<User>(request.downloadHandler.text);
                WebServices.AuthString = loggedInUser.token;
                Debug.Log(WebServices.AuthString);
            }
        }
        
        private IEnumerator GetUsers()
        {
            var request = WebServices.Get("users/");
            yield return request.SendWebRequest();

            if (request.isNetworkError)
            {
                Debug.LogError(request.error);
            }
            else
            {
                Debug.Log(request.downloadHandler.text);
                UserList users = JsonUtility.FromJson<UserList>(request.downloadHandler.text);
                foreach (var user in users.users)
                {
                    Debug.Log(user);
                }
            }
        }
    }
}