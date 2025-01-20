using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.UI;

public class DragAndDrop : MonoBehaviour
{
    Vector3 offset;
    RectTransform rt;
    Image wordBoxImage;
    GameObject filledInputBox;
    bool matchedWithPair = false;

    public static event Action<GameObject, GameObject> OnInputBoxFilled;
    public static event Action<GameObject, GameObject> OnInputBoxExited;

    // Start is called before the first frame update
    void Start()
    {
        InputBoxAvailability.OnListShare += EndDrag;
        InputBoxAvailability.OnCorrectSolutions += PairMatched;
        rt = gameObject.GetComponent<RectTransform>();
        wordBoxImage = gameObject.transform.GetChild(0).gameObject.GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginDrag()
    {
        // Stops all dragging functions once a pair has been found.
        if(!matchedWithPair)
        {
            // Marking a box as available once the word leaves it.
            if (filledInputBox != null)
            {
                OnInputBoxExited(gameObject, filledInputBox);
                // Clearing input box so this only gets called when leaving an input box.
                filledInputBox = null;
            }
        }
    }

    public void MouseDrag()
    {
        if(!matchedWithPair)
        {
            //Debug.LogFormat("Object position: {0} Mouse position: {1}", rt.position, Input.mousePosition);
            rt.position = Input.mousePosition;
        }
    }

    void EndDrag(List<GameObject> inputBoxes)
    {
        // Checks if the page the word is a child of is still active. This prevents the word from affecting lists on other pages.
        if(!matchedWithPair && gameObject.transform.parent.gameObject.activeSelf)
        {
            foreach (GameObject inputBox in inputBoxes)
            {
                RectTransform inputBoxRt = inputBox.GetComponent<RectTransform>();

                //Debug.LogFormat("Checking word at position {0} overlaps with input box at position {1}.", rt.position, inputBoxRt.position);
                if (inputBoxRt != null && inputBox.CompareTag("InputBox") && RectTransformExpansion.Overlaps(rt, inputBoxRt))
                {
                    rt.position = inputBoxRt.position;

                    //Debug.LogFormat("Word overlaps with input box at position {0}. New position: {1}. Current word: {2}", inputBoxRt.position, rt.position, gameObject.name);
                    OnInputBoxFilled(gameObject, inputBox);

                    // Saving the input box for when the word leaves it.
                    filledInputBox = inputBox;
                    break;
                }
            }
        }
    }

    public void PairMatched(List<GameObject> correctlyPairedWords)
    {
        foreach(GameObject word in correctlyPairedWords)
        {
            // When pairs are considered solved the word checks if it is in the solved list.
            if (word == gameObject)
            {
                matchedWithPair = true;
                if (wordBoxImage != null) { wordBoxImage.color = Color.green; }
            }
        }
    }

    private void OnDestroy()
    {
        InputBoxAvailability.OnListShare -= EndDrag;
        InputBoxAvailability.OnCorrectSolutions -= PairMatched;
    }
}
