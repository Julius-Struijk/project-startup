using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputBoxAvailability : MonoBehaviour
{
    [SerializeField] List<GameObject> inputBoxes;
    [SerializeField] UDictionary<GameObject, GameObject> wordPairsSolution;
    List<GameObject> correctlyPairedWords;
    int correctPairsCount = 0;
    int unsolvedPairsOnPage = 3;

    public static event Action<List<GameObject>> OnCorrectSolutions;
    public static event Action OnPageSolved;
    public static event Action<List<GameObject>> OnListShare;

    private void Start()
    {
        correctlyPairedWords = new List<GameObject>();

        DragAndDrop.OnInputBoxFilled += InputBoxEnter;
        DragAndDrop.OnInputBoxExited += InputBoxExit;
        EndDrag.OnDragEnd += ListShare;
        Debug.Log("Starting page.");
    }

    private void Update()
    {
        if(correctPairsCount == 3 && OnCorrectSolutions != null)
        {
            OnCorrectSolutions(correctlyPairedWords);
            // Detracts solved pairs on the page, to see how many are left.
            unsolvedPairsOnPage -= correctPairsCount;
            correctPairsCount = 0;
            Debug.Log("3 pairs are correct!");

            if(unsolvedPairsOnPage <= 0 && OnPageSolved != null) 
            {
                OnPageSolved();
                Debug.LogFormat("Page {0} is fully solved.", gameObject.name);
            }
        }
    }

    void InputBoxEnter(GameObject word, GameObject filledInputBox) 
    {
        // Checking if the page is active to see if input box is for the page.
        if(gameObject.activeSelf) 
        {
            // This means that once a input box has been filled, another word can't enter it.
            inputBoxes.Remove(filledInputBox);
            for (int i = 0; i < wordPairsSolution.Count; i++)
            {
                // Increase the solution counter if the word pair matches the one in the dictionary.
                if (wordPairsSolution.Keys[i] == filledInputBox && wordPairsSolution.Values[i] == word)
                {
                    // Add the word to a list so it knows which words are correct.
                    correctlyPairedWords.Add(word);
                    correctPairsCount++;
                    Debug.LogFormat("Correct pair count increased to: {0}", correctPairsCount);
                }
            }
        }
    }

    void InputBoxExit(GameObject word, GameObject exitedInputBox)
    {
        if(gameObject.activeSelf)
        {
            // Clearing up the availability of an input box once the word leaves.
            inputBoxes.Add(exitedInputBox);

            for (int i = 0; i < wordPairsSolution.Count; i++)
            {
                // Decrease the solution counter if a matching word pair is separated.
                if (wordPairsSolution.Keys[i] == exitedInputBox && wordPairsSolution.Values[i] == word)
                {
                    correctlyPairedWords.Remove(word);
                    correctPairsCount--;
                    Debug.LogFormat("Correct pair count decreased to: {0}", correctPairsCount);
                }
            }
        }

    }

    // Shares a page's unique input boxes with all words only if it's currently active.
    void ListShare() { if(OnListShare != null && gameObject.activeSelf) { OnListShare(inputBoxes); } 
    
    }

    private void OnDestroy()
    {
        DragAndDrop.OnInputBoxFilled -= InputBoxEnter;
        DragAndDrop.OnInputBoxExited -= InputBoxExit;
        EndDrag.OnDragEnd -= ListShare;
    }
}
