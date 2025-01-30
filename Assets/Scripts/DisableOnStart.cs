using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableOnStart : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        // This class exists so the game objects attached to them can always be initialized before being disabled.
        Debug.LogFormat("Hiding {0} with current status {1}", gameObject.name, gameObject.activeSelf);
        gameObject.SetActive(false);
    }
}
