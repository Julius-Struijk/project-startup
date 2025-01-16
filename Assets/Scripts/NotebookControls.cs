using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class NotebookControls : MonoBehaviour
{

    // Update is called once per frame
    void Update()
    {
        // Toggles the visibility of every notebook object.
        if(Input.GetKeyDown(KeyCode.Tab))
        {
            for (int i = 0; i < gameObject.transform.childCount; i++)
            {
                GameObject child = gameObject.transform.GetChild(i).gameObject;
                child.SetActive(!child.activeSelf);
            }
        }
    }
}
