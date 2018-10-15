using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIScript : MonoBehaviour {

	public void ButtonContinue()
    {
        //return to main menu
        SceneManager.LoadScene("Menu");
    }

    public void NextLevel()
    {
        if(SceneManager.GetActiveScene().buildIndex == 6)
            SceneManager.LoadScene(0);
        else
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
}
