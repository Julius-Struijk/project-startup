using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class NotebookControls : MonoBehaviour
{

    [SerializeField] List<GameObject> notebookPages;
    int currentPage = 0;
    int solvedPages = 0;

    private void Start()
    {
        InputBoxAvailability.OnPageSolved += SolvedCheck;
    }

    // Update is called once per frame
    void Update()
    {
        // Navigate the pages of the notebook
        if(Input.GetKeyDown(KeyCode.LeftArrow) || Input.GetKeyDown(KeyCode.A) && currentPage != 0)
        {
            notebookPages[currentPage].SetActive(false);
            currentPage--;
            notebookPages[currentPage].SetActive(true);
        }
        else if (Input.GetKeyDown(KeyCode.RightArrow) || Input.GetKeyDown(KeyCode.D) && currentPage + 1 != notebookPages.Count)
        {
            notebookPages[currentPage].SetActive(false);
            currentPage++;
            notebookPages[currentPage].SetActive(true);
        }
    }

    // Once a page has been fully solved this increments by one. Once it matches the page count, that means that all pages have been solved.
    void SolvedCheck()
    {
        solvedPages++;
        if(solvedPages >= notebookPages.Count) 
        {
            FindAnyObjectByType<GameManager>().SetNotebookFullySolved();
            Debug.Log("Notebook has been fully solved!");
        }
    }

    private void OnDestroy()
    {
        InputBoxAvailability.OnPageSolved -= SolvedCheck;
    }
}
