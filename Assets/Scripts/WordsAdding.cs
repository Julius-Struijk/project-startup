using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordsAdding : MonoBehaviour
{
    
    [SerializeField] List<GameObject> words;
    bool allWordsAdded = false;
    public static event Action OnAllWordsAdded;

    // Start is called before the first frame update
    void Start()
    {
        WordDiscovery.AddWord += EnableWord;
        PauseGame.OnCheckWordsStatus += CheckWordsAdded;
        Debug.LogFormat("Starting {0}", gameObject.name);

        // I put this here so that this script will always be initialized before the game object is disabled.
        //Debug.LogFormat("Hiding {0} with current status {1}", gameObject.name, gameObject.activeSelf);
        gameObject.SetActive(false);
    }

    void EnableWord(List<int> indexesToAdd)
    {
        if(!allWordsAdded)
        {
            foreach (int index in indexesToAdd)
            {
                if (index >= 0 && index < words.Count)
                {
                    words[index].SetActive(true);
                    Debug.LogFormat("Added word: {0}", words[index].name);
                }
            }
        }
    }

    void CheckWordsAdded()
    {
        int addedCounter = 0;
        foreach(GameObject word in words)
        {
            // If even one word isn't active, that means that not all words have been added, so it quits the loop.
            if(!word.activeSelf) { break; }
            else { addedCounter++; }
        }
        if(addedCounter == words.Count && OnAllWordsAdded != null && gameObject.CompareTag("Notebook")) 
        {
            Debug.Log("All words have been added!");
            allWordsAdded = true;
            OnAllWordsAdded();
        }
    }

    private void OnDestroy()
    {
        WordDiscovery.AddWord -= EnableWord;
        PauseGame.OnCheckWordsStatus -= CheckWordsAdded;
    }
}
