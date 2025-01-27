using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;
using System;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager gameManager;
    private DialogueRunner dialogueRunner;
    public Light _light;
    public Camera _camera;
    public GameObject _richard;
    GameObject currentNPC;
    public static event Action<bool> OnCharacterTalk;

    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        dialogueRunner = gameManager._dialogueRunner;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            dialogueRunner.Stop();
            if (OnCharacterTalk != null) { OnCharacterTalk(false); }
        }

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitinfo) && Input.GetMouseButtonUp(0) && !dialogueRunner.Dialogue.IsActive)
        {
            if (hitinfo.collider.gameObject.tag == "NPC")
            {
                // Saving the NPC, so we can add it's word after the conversation is over.
                currentNPC = hitinfo.collider.gameObject;
                currentNPC.GetComponent<NPCInteraction>().StartInteraction();
                // This will prevent the player from opening menu's while talking to NPCs.
                if(OnCharacterTalk != null) { OnCharacterTalk(true); }
            }
            else if (hitinfo.collider.gameObject.tag == "Item")
            {
                gameManager._objects.Add(hitinfo.collider.gameObject.name, hitinfo.collider.gameObject);

                Destroy(hitinfo.collider.gameObject);
            }
            else if (hitinfo.collider.gameObject.tag == "WordObject")
            {
                hitinfo.collider.GetComponentInParent<WordDiscovery>().ShareWordIndex();
                hitinfo.collider.GetComponentInParent<ObjectInteration>().ShowItem();
            }
        }
    }

    public void OnCompleteDialogue()
    {
        if (currentNPC != null)
        {
            Debug.Log($" ncp {currentNPC}");
            // Adding the word attached to the NPC after the conversation is over, if it has one.
            WordDiscovery wordAdder = currentNPC.GetComponent<WordDiscovery>();
            if (wordAdder != null) { wordAdder.ShareWordIndex(); }
            currentNPC = null;
        }

        Cursor.lockState = CursorLockMode.Locked;
        gameObject.GetComponentInChildren<PlayerMovement>().SetEnabledMove(true);
        gameObject.GetComponentInChildren<PlayerLook>().SetEnabledLook(true);
        dialogueRunner.GetComponentInChildren<Image>().enabled = false;
        dialogueRunner.GetComponentInChildren<AudioSource>().Stop();
        _light.intensity = 130000;
        // Reenable the ability to open menu's.
        if (OnCharacterTalk != null) { OnCharacterTalk(false); }
    }

    public void OnStartDialogue()
    {
        Cursor.lockState = CursorLockMode.None;
        gameObject.GetComponentInChildren<PlayerMovement>().SetEnabledMove(false);
        gameObject.GetComponentInChildren<PlayerLook>().SetEnabledLook(false);
        dialogueRunner.GetComponentInChildren<Image>().enabled = true;
        _light.intensity = 50000;
    }
}
