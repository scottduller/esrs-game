using System;
using System.Collections;
using System.Collections.Generic;
using Assets.Scripts;
using TMPro;
using UnityEngine;

public class ShowCurrentUserScript : MonoBehaviour
{
    private TMP_Text _logInText;
    private string _baseText = "logged in: ";
    
    private void Awake()
    {
        _logInText = transform.GetComponent<TMP_Text>();
    }

    public void Start()
    {
        try
        {
            _logInText.text = _baseText + GlobalValues.currentUser.username;
        }
        catch (Exception e)
        {
            _logInText.text = "No User Is Stored In Global Values ERROR";
            Console.WriteLine(e);
            throw;
        }
        
    }
}
