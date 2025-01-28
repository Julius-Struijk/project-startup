using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class MainMenu : MonoBehaviour
{
    public AudioSource mainMenuMusic;
    public string gameSceneName;

    void Start()
    {
        if (!Cursor.visible || Cursor.lockState == CursorLockMode.Locked)
        {
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
    }

    public void StartGame()
    {
        Time.timeScale = 0f;

        if (mainMenuMusic != null)
        {
            mainMenuMusic.Stop();
        }
        else
        {
            SceneManager.LoadScene(gameSceneName);
        }
    }

    public void QuitGame()
    {
        Console.WriteLine("Outta here");
        Application.Quit();
    }
}
