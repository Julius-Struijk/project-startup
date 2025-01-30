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
    GameObject currentNPC;
    public static event Action<bool> OnCharacterTalk;
    public Canvas _map;

    void Start()
    {
        gameManager = GameObject.FindAnyObjectByType<GameManager>();
        dialogueRunner = gameManager._dialogueRunner;
    }

    void Update()
    {
        if (Input.GetKeyUp(KeyCode.M) && !dialogueRunner.Dialogue.IsActive)
        {
            Debug.Log("test map " + _map.GetComponent<Canvas>().enabled);
            _map.GetComponent<Canvas>().enabled = !_map.GetComponent<Canvas>().enabled;
        }

        if (Input.GetKeyUp(KeyCode.Escape))
        {
            dialogueRunner.Stop();
            if (OnCharacterTalk != null) { OnCharacterTalk(false); }
        }

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitinfo) && Input.GetMouseButtonUp(0) && !dialogueRunner.Dialogue.IsActive)
        {
            // Timescale is set to 0 so that the game is paused when in the menus. This can be used to prevent the player from talking to NPCs when they're in a menu.
            if (hitinfo.collider.gameObject.tag == "NPC" && Time.timeScale != 0f)
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
                ObjectInteration obj = hitinfo.collider.GetComponentInParent<ObjectInteration>();
                if(obj != null) { obj.ShowItem(); }
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
