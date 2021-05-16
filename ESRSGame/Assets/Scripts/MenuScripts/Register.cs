using System;
using Api.Services;
using Assets.Scripts.Api.Services;
using Assets.Scripts.Api.Models;
using TMPro;
using UnityEditor;
using UnityEngine;

namespace MenuScripts
{
    public class Register : MonoBehaviour
    {
        public UserServices userServices;
        public AppSceneManager appSceneManager;
        private TMP_InputField _username, _password, _confirmPassword;
        private void Awake()
        {
            try
            {
                _username =  transform.Find("MenuItems/Username").GetComponent<TMP_InputField>();
                _password =  transform.Find("MenuItems/Password").GetComponent<TMP_InputField>();
                _confirmPassword =  transform.Find("MenuItems/ConfirmPassword").GetComponent<TMP_InputField>();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
        }

        public void RegisterUser()
        {
            if (!string.IsNullOrEmpty(_username.text) || !string.IsNullOrEmpty(_password.text))
            {
                if (_password.text == _confirmPassword.text)
                {

                        userServices.RegisterUser(_username.text, _password.text, (User user) => {if (user != null) appSceneManager.LoadScene(1);});
                }
                else
                {
                    EditorUtility.DisplayDialog("Register User", "Passwords Do Not Match", "Ok");
                }


            }
            else
            {
                EditorUtility.DisplayDialog("Register User", "Fields are Empty", "Ok");
            }
            _password.text = string.Empty;
            _confirmPassword.text = string.Empty;
        }
    }
}
