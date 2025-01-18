using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yarn.Unity;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    // Yarn spinner info / tips
    // https://www.youtube.com/watch?v=7nW8VlI3zOs
    // https://www.yarnspinner.dev/blog

    public GameObject _player;
    public DialogueRunner _dialogueRunner;
    public Camera _camera;

    public Dictionary<string, GameObject> _objects = new Dictionary<string, GameObject>();

  
    // public bool inDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        _dialogueRunner.AddFunction<string, bool>("PlayerMetNPC", PlayerMetNPC);
        _dialogueRunner.AddFunction<string, bool>("PlayerHasItem", PlayerHasItem);
        _dialogueRunner.AddFunction<string, bool>("PlayerGifItem", PlayerGifItem);
        StopInteraction();
    }

    public void Update()
    {
        if (_dialogueRunner.Dialogue.IsActive)
        {
            _dialogueRunner.GetComponentInChildren<Image>().enabled = true;
        }
        else
        {
            _dialogueRunner.GetComponentInChildren<Image>().enabled = false;
        }
    }

    public void StartInteraction(string name, Sprite image)
    {
        _dialogueRunner.StartDialogue(name);
        _dialogueRunner.GetComponentInChildren<Image>().sprite = image;
    }

    public void StopInteraction()
    {
        _dialogueRunner.Stop();
    }

    private bool PlayerMetNPC(string NPCName)
    {
        Debug.Log("checking npc " + NPCName);

        if (_objects.ContainsKey(NPCName))
        {
            return true;
        }
        else
        {
            GameObject gameObject = GameObject.FindGameObjectsWithTag("NPC").Where(NPC => NPC.name == NPCName).First();
            _objects.Add(gameObject.name, gameObject);
            return false;
        }
    }
    private bool PlayerHasItem(string item)
    {
        return (_objects.ContainsKey(item));
    }

    private bool PlayerGifItem(string item)
    {
        _objects.Remove(item);
        return true;
    }

}
