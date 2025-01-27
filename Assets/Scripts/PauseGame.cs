using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseGame : MonoBehaviour
{
    private bool isPaused = false;
    bool talking = false;
    bool allWordsAdded = false;
    public GameObject pauseMenu;         
    public GameObject otherUIToDisable;    
    public string mainMenuSceneName;
    [SerializeField] KeyCode pauseButton;
    [SerializeField] bool enableOtherUI;

    // This is allows for a more efficient way to check if all words have been added.
    public static event Action OnCheckWordsStatus;

    void Start()
    {
        PlayerInteraction.OnCharacterTalk += TalkingToCharacter;
        WordsAdding.OnAllWordsAdded += AllWordsAdded;

        if (pauseMenu != null)
        {
            pauseMenu.SetActive(false);
        }

        if (otherUIToDisable != null && enableOtherUI)
        {
            otherUIToDisable.SetActive(true);
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
            otherUIToDisable.SetActive(false);
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
            otherUIToDisable.SetActive(true);
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
