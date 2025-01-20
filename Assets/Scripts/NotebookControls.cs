using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NotebookControls : MonoBehaviour
{
    [SerializeField] List<GameObject> notebookPages;
    int currentPage = 0;

    // Update is called once per frame
    void Update()
    {
        // Navigate the pages of the notebook
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && currentPage != 0)
        {
            Debug.Log("Switching pages left.");
            notebookPages[currentPage].SetActive(false);
            currentPage--;
            notebookPages[currentPage].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && currentPage + 1 != notebookPages.Count)
        {
            Debug.Log("Switching pages right.");
            notebookPages[currentPage].SetActive(false);
            currentPage++;
            notebookPages[currentPage].SetActive(true);
        }
    }
}
