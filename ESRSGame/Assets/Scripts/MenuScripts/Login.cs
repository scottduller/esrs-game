using System;
using Api.Services;
using TMPro;
using UnityEditor;
using UnityEngine;
using UnityEngine.Serialization;

namespace MenuScripts
{
    public class Login : MonoBehaviour
    {
        public UserServices userServices;
        public AppSceneManager appSceneManager;
        public int mainMenuSceneNumber = 3;
        private TMP_InputField _username, _password;
        private void Awake()
        {
            try
            {
                _username =  transform.Find("MenuItems/Username").GetComponent<TMP_InputField>();
                _password =  transform.Find("MenuItems/Password").GetComponent<TMP_InputField>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void LoginUser()
        {
            if (!string.IsNullOrEmpty(_username.text) || !string.IsNullOrEmpty(_password.text))
            {
                userServices.LoginUser(_username.text, _password.text,Result);
            }
            else
            {
                EditorUtility.DisplayDialog("Login User", "Fields are Empty", "Ok");
            }
            _password.text = string.Empty;
            
        }

        private void Result(int flag)
        {
            switch (flag)
            {
                case 0:
                    appSceneManager.loadScene(mainMenuSceneNumber);
                    break;
                case 1: 
                    Debug.Log("Invalid User Credentials");
                    break;
                default:
                    Debug.Log("Unkown Flag");
                    break;
                    
            }
            
        }
    }
}
