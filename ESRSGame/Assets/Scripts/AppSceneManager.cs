using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
/// <summary>
/// class in charge of sceneManagement and interfacing with GlobalValues script
/// </summary>
public class AppSceneManager : MonoBehaviour
{
    /// <summary>
    /// on scene load, get GlobalValues to get playerprefs values
    /// </summary>
    private void Awake() {
        // GlobalValues.getPlayerPrefs(); 
    }

    /// <summary>
    /// load scene of the scene int in the project build order
    /// </summary>
    /// <param name="sceneInt"></param>
    public  void LoadScene(int sceneInt){
        try{
            // GlobalValues.updatePlayerPrefs();
            SceneManager.LoadScene (sceneInt);
        }catch(System.Exception e){
            Debug.LogError(e);
        }
    }
    
    public  void LoadScene(string sceneStr){
        try{
            // GlobalValues.updatePlayerPrefs();
            SceneManager.LoadScene (sceneStr);
            Time.timeScale = 1;
        }catch(System.Exception e){
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// exit app
    /// </summary>
    public void ExitApp(){
        Application.Quit();
    }

    /// <summary>
    /// used to wipe playerprefs
    /// </summary>
    public void WipePlayerPref(){
        // GlobalValues.clearPlayerPrefs();
    }

    public void ReloadScene()
    {
        Time.timeScale = 1;

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Logout()
    {
        Time.timeScale = 1;

        GlobalValues.LogoutUser();
    }
    
    


    
}