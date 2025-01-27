using UnityEngine;
using UnityEngine.UI;

public class ObjectInteration : MonoBehaviour
{
    public Canvas item;
    public Sprite image;
    public bool sowing;

    void Update()
    {
        if (sowing && (Input.GetKeyUp(KeyCode.Escape) || Input.GetMouseButtonUp(0)))
        {
            item.GetComponent<Canvas>().enabled = false;
            sowing = false;
        }
    }

    public void ShowItem()
    {
        Debug.Log("sowing item");
        item.GetComponentInChildren<Image>().sprite = image;
        item.GetComponent<Canvas>().enabled = true;
        sowing = true;
    }
}
