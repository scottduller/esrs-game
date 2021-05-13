using System.Collections;
using System.Collections.Generic;
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
    public void loadScene(int sceneInt){
        try{
            // GlobalValues.updatePlayerPrefs();
            SceneManager.LoadScene (sceneInt);
        }catch(System.Exception e){
            Debug.LogError(e);
        }
    }

    /// <summary>
    /// exit app
    /// </summary>
    public void exitApp(){
        Application.Quit();
    }

    /// <summary>
    /// used to wipe playerprefs
    /// </summary>
    public void wipePlayerPref(){
        // GlobalValues.clearPlayerPrefs();
    }


    
}