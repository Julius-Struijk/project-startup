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

    // This is to share the list of input boxes with all the words. Using delegates allows for only one list to exist instead of each word having its own one.
    public static event Action<List<GameObject>> OnDragEnd;
    public static event Action<List<GameObject>> OnCorrectSolutions;

    private void Start()
    {
        correctlyPairedWords = new List<GameObject>();

        DragAndDrop.OnInputBoxFilled += InputBoxEnter;
        DragAndDrop.OnInputBoxExited += InputBoxExit;
    }

    private void Update()
    {
        if(correctPairsCount == 3 && OnCorrectSolutions != null)
        {
            OnCorrectSolutions(correctlyPairedWords);
            correctPairsCount = 0;
            Debug.Log("3 pairs are correct!");

        }
    }

    public void DragEnd()
    {
        if(OnDragEnd != null) { OnDragEnd(inputBoxes); }
    }

    void InputBoxEnter(GameObject word, GameObject filledInputBox) 
    {
        // This means that once a input box has been filled, another word can't enter it.
        inputBoxes.Remove(filledInputBox);
        for(int i = 0; i < wordPairsSolution.Count; i++)
        {
            // Increase the solution counter if the word pair matches the one in the dictionary.
            if(wordPairsSolution.Keys[i] == filledInputBox && wordPairsSolution.Values[i] == word) 
            {
                // Add the word to a list so it knows which words are correct.
                correctlyPairedWords.Add(word);
                correctPairsCount++;
                Debug.LogFormat("Correct pair count increased to: {0}", correctPairsCount);
            }
        }
    }

    void InputBoxExit(GameObject word, GameObject exitedInputBox)
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

    private void OnDestroy()
    {
        DragAndDrop.OnInputBoxFilled -= InputBoxEnter;
        DragAndDrop.OnInputBoxExited -= InputBoxExit;
    }
}
