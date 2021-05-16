using System;
using Api.Models;
using Assets.Scripts.Api.Models;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using Random = UnityEngine.Random;

/// <summary>
/// static class used to store data between scenes, additionally interacts with playerPrefs
/// </summary>
public static class GlobalValues
{
    public static readonly Random Rnd = new Random();
    public static User currentUser;
    public static Level currentLevel;



    public static void UpdateUser(User user)
    {
        PlayerPrefs.SetString("UserId", user.id);
        currentUser = user;
    }

    public static void LogoutUser()
    {
        PlayerPrefs.DeleteKey("Authorization");
        PlayerPrefs.DeleteKey("UserId");
        currentUser = null;
        try{
            // GlobalValues.updatePlayerPrefs();
            SceneManager.LoadScene ("Login");
        }catch(System.Exception e){
            Debug.LogError(e);
        }
    }
}