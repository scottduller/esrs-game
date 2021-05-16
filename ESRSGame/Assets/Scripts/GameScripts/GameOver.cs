using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class GameOver : MonoBehaviour
{
    public  GameObject GameOverCanvas;

    private void Start()
    {
        Time.timeScale = 1;
    }
    public void GameOverMethod()
    {
        Debug.Log("GAMEOVER");
        GameOverCanvas.SetActive(true);
        Time.timeScale = 0;
    } 

    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void Quit()
    {
        Debug.Log("Quitting");
        Application.Quit();
    }
}
