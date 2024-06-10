using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public void PlayButtonPressed()
    {
        SceneManager.LoadScene(1);
    }
    public void ExitButtonPressed()
    {
        Application.Quit();
    }
}
