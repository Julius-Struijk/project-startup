using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class WordDiscovery : MonoBehaviour
{
    public static event Action<int> AddWord;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E) && AddWord != null)
        {
            AddWord(0);
        }
    }
}
