using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Utilities;
using System.IO;
/// <summary>
/// static class used to store data between scenes, additionally interacts with playerPrefs
/// </summary>
public static class GlobalValues 
{
    public static bool AvowSnapping; //stores if avowSnapping in enabled
    public static float AvowSnappingOffset; // stores the offset value of avowSnapping

    public static bool ToolTipsEnabled;// stores if tooltips was left enabled or not
    public static bool circuitDisplayAll; // stores if all values should be shown in a diagram 
    public static string workingDirectory; // stores current working directory

    public static string authorName; //store author name, used to 
    
     
     /// <summary>
     /// gets playerprefs values and assigns them.
     /// if no playerpref can be found, creates playerprefs fields.
     /// </summary>
     public static void getPlayerPrefs(){
         if (!PlayerPrefs.HasKey(workingDirectory)){//if player prefs has no stored working directory
             if(!System.IO.Directory.Exists(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/DiagramFiles")) //if no diagramFiles exist 
             Directory.CreateDirectory(System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/DiagramFiles"); // create diagram files directory to store and load files
         }

        workingDirectory  = PlayerPrefs.GetString("workingDirectory",System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyDocuments) + "/DiagramFiles"); //get current directory, if cant be found set new
        authorName  = PlayerPrefs.GetString("author",""); //get author , if no author set, ignore to blank
        switch( PlayerPrefs.GetInt("toolTipsEnables", 1)){//get enabled tool tips, use int to show bool, 1 = true 0=false
            case 0:
                ToolTipsEnabled = false;
                break;
            case 1:
                ToolTipsEnabled = true;
                break;
            default:
                Debug.Log("tool tips enabled invalid number, corrupted player prefs");
                break;
        }
        

    }

//save values to playerprefs
    public static void updatePlayerPrefs(){
        PlayerPrefs.SetString("workingDirectory",workingDirectory);
        PlayerPrefs.SetString("author",authorName);
         if(ToolTipsEnabled)  PlayerPrefs.SetInt("toolTipsEnables", 1);
         else PlayerPrefs.SetInt("toolTipsEnables",0);
    }


//clear player prefs
    public static void clearPlayerPrefs(){
        PlayerPrefs.DeleteAll();
    }


    
}