using System;
using System.Collections;
using System.Collections.Generic;
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
        string userToken = PlayerPrefs.GetString("Authorization");
        _logInText.text = _baseText + userToken;
    }
}
