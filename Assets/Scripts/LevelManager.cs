using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour {

    public void LevelOne()
    {
        Debug.Log("LEVEL ONE");
        SceneManager.LoadScene(4);
    }

    public void LevelTwo()
    {
        Debug.Log("LEVEL TWO");
        SceneManager.LoadScene(5);
    }

    public void LevelThree()
    {
        Debug.Log("LEVEL THREE");
        SceneManager.LoadScene(6);
    }

    public void MainMenu()
    {
        Debug.Log("MAIN MENU");
        SceneManager.LoadScene(0);
    }
}
