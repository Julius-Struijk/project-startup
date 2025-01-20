using System;
using UnityEngine;
using UnityEngine.UI;
using Yarn.Unity;

public class PlayerInteraction : MonoBehaviour
{
	public static event Action<int> AddWord;
    private GameManager _gameManager;
    private DialogueRunner _dialogueRunner;
    public Light light;
    public Camera _camera;

    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        _dialogueRunner = _gameManager._dialogueRunner;
    }

    void Update()
    {
        if (_dialogueRunner.Dialogue.IsActive && Input.GetKeyUp(KeyCode.Escape))
        {
            _gameManager.StopInteraction();
        }

        if (Physics.Raycast(_camera.ScreenPointToRay(Input.mousePosition), out RaycastHit hitinfo) && !_dialogueRunner.Dialogue.IsActive)
        {
            if (hitinfo.collider.gameObject.tag == "NPC" && Input.GetMouseButtonUp(0))
            {

                hitinfo.collider.GetComponentInParent<NPCInteraction>().StartInteraction();
            }

            if (hitinfo.collider.gameObject.tag == "Item" && Input.GetMouseButtonUp(0))
            {
                _gameManager._objects.Add(hitinfo.collider.gameObject.name, hitinfo.collider.gameObject);

                Destroy(hitinfo.collider.gameObject);
            }
        }
    }

    public void OnCompleteDialogue()
    {
        Cursor.lockState = CursorLockMode.Locked;

        gameObject.GetComponentInChildren<PlayerMovement>().SetEnabledMove(true);
        gameObject.GetComponentInChildren<PlayerLook>().SetEnabledLook(true);

        _dialogueRunner.GetComponentInChildren<Image>().enabled = false;
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
