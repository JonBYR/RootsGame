using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameLoad : MonoBehaviour
{
    public void LoadScene(string sceneName)
    {
        SceneManager.LoadScene(sceneName);
    }
    public void QuitGame()
    {
        Debug.Log("Quit Application");
        Application.Quit();
    }
    public void Arcade()
    {
        ArcadeWin.collisionOn = true;
        SceneManager.LoadScene("RootsPlayground");
    }
    public void Endless()
    {
        ArcadeWin.collisionOn = false;
        SceneManager.LoadScene("RootsPlayground");
    } 
}
