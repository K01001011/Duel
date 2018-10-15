using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour {

	public void StartGame()
    {
        Debug.Log("STARTING GAME");
        SceneManager.LoadScene(1);
    }

    public void LevelSelect()
    {
        Debug.Log("LEVEL SELECT");
        SceneManager.LoadScene(2);
    }

    public void Shop()
    {
        Debug.Log("SHOP");
        SceneManager.LoadScene(3);
    }

    public void AboutPage()
    {
        Debug.Log("ABOUT");
        SceneManager.LoadScene(7);
    }

    public void QuitGame()
    {
        Debug.Log("QUIT");
        Application.Quit();
    }
}
