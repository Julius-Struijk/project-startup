using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordDiscovery : MonoBehaviour
{
    [SerializeField] int wordIndex;
    public static event Action<int> AddWord;

    public void ShareWordIndex()
    {
        // Shares the word index so the correct word can be added to the notebook.
        if(AddWord != null) { AddWord(wordIndex); }
    }
}
