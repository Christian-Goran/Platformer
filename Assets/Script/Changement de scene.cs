using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Changementdescene : MonoBehaviour
{
    // Corrected method name and definition

    public void Play()
    {
        SceneManager.LoadScene("SampleScene");
    }
    public void regles()
    {
        SceneManager.LoadScene("Instructions");
    }

    public void GoToMainMenu()
    {
        SceneManager.LoadScene("Scene_Intro");
    }

    public void QuitGame()
    {
        // Quit the application. This will not work in the Unity editor, so include a check for that.
        #if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
        #else
        Application.Quit();
        #endif
    }
}
