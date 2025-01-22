using System;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    private DialogueRunner _dialogueRunner;
    public Light light;
    public Camera _camera;
    GameObject currentNPC;

    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        _dialogueRunner = _gameManager._dialogueRunner;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape)) _dialogueRunner.Stop();        

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitinfo) && Input.GetMouseButtonUp(0) && !_dialogueRunner.Dialogue.IsActive)
        {
            if (hitinfo.collider.gameObject.tag == "NPC")
            {
                // Saving the NPC, so we can add it's word after the conversation is over.
                currentNPC = hitinfo.collider.gameObject;
                currentNPC.GetComponent<NPCInteraction>().StartInteraction();
            }
            else if (hitinfo.collider.gameObject.tag == "Item")
            {
                _gameManager._objects.Add(hitinfo.collider.gameObject.name, hitinfo.collider.gameObject);

                Destroy(hitinfo.collider.gameObject);
            }
            else if (hitinfo.collider.gameObject.tag == "WordObject")
            {
                hitinfo.collider.GetComponentInParent<WordDiscovery>().ShareWordIndex();
            }
        }
    }

    public void OnCompleteDialogue()
    {
        if (currentNPC != null)
        {
            // Adding the word attached to the NPC after the conversation is over, if it has one.
            WordDiscovery wordAdder = currentNPC.GetComponent<WordDiscovery>();
            if (wordAdder != null) { wordAdder.ShareWordIndex(); }
            currentNPC = null;
        }

        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponentInChildren<PlayerMovement>().SetEnabledMove(true);
        gameObject.GetComponentInChildren<PlayerLook>().SetEnabledLook(true);
        _dialogueRunner.GetComponentInChildren<Image>().enabled = false;
        _dialogueRunner.GetComponentInChildren<AudioSource>().Stop();
        light.intensity = 130000;
    }

    public void OnStartDialogue()
    {
        Cursor.lockState = CursorLockMode.None;
        gameObject.GetComponentInChildren<PlayerMovement>().SetEnabledMove(false);
        gameObject.GetComponentInChildren<PlayerLook>().SetEnabledLook(false);
        _dialogueRunner.GetComponentInChildren<Image>().enabled = true;
        light.intensity = 50000;
    }
}
