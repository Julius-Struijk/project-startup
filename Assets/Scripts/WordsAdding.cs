using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WordsAdding : MonoBehaviour
{
    
    [SerializeField] List<GameObject> words;

    // Start is called before the first frame update
    void Start()
    {
        WordDiscovery.AddWord += EnableWord;
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
        WordDiscovery.AddWord -= EnableWord;
    }
}
