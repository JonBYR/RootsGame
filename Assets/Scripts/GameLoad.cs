using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class GameLoad : MonoBehaviour
{
    public TMP_Text highScoreText;
    public TMP_Text endlessText;
    private void Start()
    {
        highScoreText.text = ("Arcade High Score: " + PlayerPrefs.GetInt("ArcadeHighScore", 0));
        endlessText.text = ("Endless High Score: " + PlayerPrefs.GetInt("EndlessHighScore", 0));
    }
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
        TIleInteraction.arcade = true;
        SceneManager.LoadScene("RootsPlayground");
    }
    public void Endless()
    {
        ArcadeWin.collisionOn = false;
        TIleInteraction.arcade = false;
        SceneManager.LoadScene("RootsPlayground");
    } 
}
