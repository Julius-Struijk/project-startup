using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    private WordsAdding wordAdder;
    public AudioClip audioClip;
    public Image image;
    public float sizeScale;
 
    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        wordAdder = gameObject.GetComponent<WordsAdding>();
    }

    public void StartInteraction()
    {
        NextDilegeaNode();
        _gameManager.StartInteraction(image.sprite, audioClip, sizeScale, this.name);
    }

    static public void NextDilegeaNode()
    {

    }
}
