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
        if(addedCounter == words.Count && OnAllWordsAdded != null) 
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
