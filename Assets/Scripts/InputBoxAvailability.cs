using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class InputBoxAvailability : MonoBehaviour
{
    [SerializeField] List<GameObject> inputBoxes;
    // This is to share the list of input boxes with all the words. Using delegates allows for only one list to exist instead of each word having its own one.
    public static event Action<List<GameObject>> OnDragEnd;

    private void Start()
    {
        DragAndDrop.OnInputBoxFilled += InputBoxCheck;
    }

    public void DragEnd()
    {
        if(OnDragEnd != null) { OnDragEnd(inputBoxes); }
    }

    void InputBoxCheck(GameObject word, GameObject filledInputBox) 
    {
        
    }

    private void OnDestroy()
    {
        DragAndDrop.OnInputBoxFilled -= InputBoxCheck;
    }
}
