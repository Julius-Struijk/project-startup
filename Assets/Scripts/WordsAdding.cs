using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordsAdding : MonoBehaviour
{
    public static event Action<int> AddWord;
    [SerializeField] List<GameObject> words;

    // Start is called before the first frame update
    void Start()
    {
        AddWord += EnableWord;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void EnableWord(int index)
    {
        if(index < words.Count)
        words[index].SetActive(true);
    }

    private void OnDestroy()
    {
        AddWord -= EnableWord;
    }
}
