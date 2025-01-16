using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInteraction : MonoBehaviour
{
    public static event Action<int> AddWord;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.E) && AddWord != null)
        {
            AddWord(0);
        }
    }
}
