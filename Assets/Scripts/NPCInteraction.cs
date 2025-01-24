using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    private WordsAdding wordAdder;
    public AudioClip audioClip;
    public Image image;   
 
    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        wordAdder = gameObject.GetComponent<WordsAdding>();
    }

    public void StartInteraction()
    {
        _gameManager.StartInteraction(image.sprite, audioClip, this.name);
    }
}
