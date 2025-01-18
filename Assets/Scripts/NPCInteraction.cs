using UnityEngine;
using UnityEngine.UI;

public class NPCInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    public Image image;
    public string name;    
 
    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
    }

    public void StartInteraction()
    {
        _gameManager.StartInteraction(name, image.sprite);
    }
}
