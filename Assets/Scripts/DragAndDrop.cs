using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DragAndDrop : MonoBehaviour
{
    Vector3 offset;
    RectTransform rt;
    [SerializeField] List<GameObject> inputBoxes;
// Start is called before the first frame update
void Start()
    {
        rt = gameObject.GetComponent<RectTransform>();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void BeginDrag()
    {
        // Calculating offset so the box is grabbed properly.
        offset = rt.position - Input.mousePosition;
        //Debug.LogFormat("Offset is: {0}", offset);
    }

    public void MouseDrag()
    {

        //Debug.LogFormat("Object position: {0} Mouse position: {1}", rt.position, Input.mousePosition);
        rt.position = Input.mousePosition + offset;
    }

    public void EndDrag()
    {
        foreach(GameObject inputBox in inputBoxes)
        {
            RectTransform inputBoxRt = inputBox.GetComponent<RectTransform>();
            // Removing offset so the position of the box can be checked properly.
            rt.position -= offset;

            //Debug.LogFormat("Checking word at position {0} overlaps with input box at position {1}.", rt.position, inputBoxRt.position);
            if (inputBoxRt != null && RectTransformExpansion.Overlaps(rt, inputBoxRt))
            {
                rt.position = inputBoxRt.position;
                //Debug.LogFormat("Word overlaps with input box at position {0}. New position: {1}",inputBoxRt.position, rt.position);
                break;
            }
            else
            {
                rt.position += offset;
            }
        }
    }
}
