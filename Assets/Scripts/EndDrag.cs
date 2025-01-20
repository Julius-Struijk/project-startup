using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EndDrag : MonoBehaviour
{
    // This is to notify pages a drag has ended and share their list of input boxes with all the words.
    // This setup allows each word to only mention this script instead of every single page, so it doesn't have to change in the future.
    public static event Action OnDragEnd;

    public void DragEnd()
    {
        if (OnDragEnd != null) { OnDragEnd(); }
    }
}
