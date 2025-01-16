using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Yarn.Unity;

public class NPCInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    private GameObject _player;
    //public KeyCode _nextkey;


    private DialogueRunner _dialogueRunner;
    //public DialogueNode _startDialogueNode, _currandDialogueNode;

    // Start is called before the first frame update
    void Start()
    {

       // _currandDialogueNode = _startDialogueNode;
        CloseUi();
        _gameManager = GameObject.FindGameObjectWithTag("GameManager").GetComponent<GameManager>();
        _player = _gameManager.GetPlayer();
        _dialogueRunner = _gameManager.GetDialogueRunner();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            CloseUi();
        }
    }

    public void StartInteraction()
    {
        _dialogueRunner.StartDialogue("Test");
    }

    public void CloseUi()
    {
        if (_dialogueRunner != null) _dialogueRunner.Stop();

      //  _gameManager.SetInDialogue(false);

    }
}
