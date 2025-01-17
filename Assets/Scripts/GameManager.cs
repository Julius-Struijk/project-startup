using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Numerics;
using UnityEditor;
using UnityEditor.PackageManager;
using UnityEngine;
using Yarn;
using Yarn.Unity;
using static UnityEditor.Progress;

public class GameManager : MonoBehaviour
{
    // Yarn spinner info / tips
    // https://www.youtube.com/watch?v=7nW8VlI3zOs
    // https://www.yarnspinner.dev/blog

    public GameObject _player;
    public DialogueRunner _dialogueRunner;
    public Camera _camera;
    public Dictionary<string, GameObject> _objects = new Dictionary<string, GameObject>();

    public bool on;
    // public bool inDialogue = false;
    // Start is called before the first frame update
    void Start()
    {
        _dialogueRunner.AddFunction<string, bool>("PlayerMetNPC", PlayerMetNPC);
        _dialogueRunner.AddFunction<string, bool>("PlayerHasItem", PlayerHasItem);
        _dialogueRunner.AddFunction<string, bool>("PlayerGifItem", PlayerGifItem);
    }

    // Update is called once per frame
    void Update()
    {
        on = _dialogueRunner.Dialogue.IsActive;

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitinfo) && !_dialogueRunner.Dialogue.IsActive)
        {
            if (hitinfo.collider.gameObject.tag == "NPC" && Input.GetMouseButtonUp(0))
            {
                hitinfo.collider.GetComponentInParent<NPCInteraction>().StartInteraction();
            }

            if (hitinfo.collider.gameObject.tag == "Item" && Input.GetMouseButtonUp(0))
            {
                _objects.Add(hitinfo.collider.gameObject.name, hitinfo.collider.gameObject);

                Destroy(hitinfo.collider.gameObject);
            }
        }
    }

    public GameObject GetPlayer()
    {
        return _player;
    }
    
    public DialogueRunner GetDialogueRunner()
    {
        return _dialogueRunner;
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
