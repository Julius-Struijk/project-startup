using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    bool talking = false;
    bool allWordsAdded = false;
    public GameObject pauseMenu;         
    [SerializeField] List<GameObject> otherUIToDisable;    
    public string mainMenuSceneName;
    [SerializeField] KeyCode pauseButton;
    [SerializeField] bool enableOtherUI;

    // This is allows for a more efficient way to check if all words have been added.
    public static event Action OnCheckWordsStatus;

    void Start()
    {
        PlayerInteraction.OnCharacterTalk += TalkingToCharacter;
        WordsAdding.OnAllWordsAdded += AllWordsAdded;

        if (otherUIToDisable != null && enableOtherUI)
        {
            foreach(GameObject UI in otherUIToDisable)
            {
                UI.SetActive(true);
            }
        }
    }

    void Update()
    {
        // If the player is talking to a character, they can't open the UI menu's.
        if (Input.GetKeyDown(pauseButton) && !talking)
        {
            // Prevents the notebook specifically from being opened if all words haven't been added.
            if(pauseButton != KeyCode.Tab || allWordsAdded)
            {
                if (isPaused)
                {
                    ResumeGame();
                }
                else
                {
                    Pause();
                }
            }
            else if(OnCheckWordsStatus != null) { OnCheckWordsStatus(); }
        }

        // Just added this to help with testing, if it's still here, please remove it.
        if (Input.GetKeyDown(KeyCode.P))
        {
            Debug.Log("Going back to main menu.");
            BackToMainMenu();
        }
    }

    void Pause()
    {
        Time.timeScale = 0f;
        isPaused = true;

        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(true);
        }

        if (otherUIToDisable != null)
        {
            foreach (GameObject UI in otherUIToDisable)
            {
                UI.SetActive(false);
            }
        }
    }

    void ResumeGame()
    {
        Time.timeScale = 1f;
        isPaused = false;

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (otherUIToDisable != null && enableOtherUI)
        {
            foreach (GameObject UI in otherUIToDisable)
            {
                UI.SetActive(true);
            }
        }
    }

    public void ResumeGameFromButton()
    {
        ResumeGame();
    }

    public void BackToMainMenu()
    {
        Time.timeScale = 1f;
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        SceneManager.LoadScene(mainMenuSceneName);
    }

    public void QuitGame()
    {
        Console.WriteLine("Outta here");
        Application.Quit();
    }

    void TalkingToCharacter(bool pTalking)
    {
        talking = pTalking;
    }

    void AllWordsAdded() { allWordsAdded = true; }

    private void OnDestroy()
    {
        PlayerInteraction.OnCharacterTalk -= TalkingToCharacter;
        WordsAdding.OnAllWordsAdded -= AllWordsAdded;
    }
}
