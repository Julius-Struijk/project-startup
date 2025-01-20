using UnityEngine;
using Yarn.Unity;

public class PlayerInteraction : MonoBehaviour
{
    private GameManager _gameManager;
    private DialogueRunner _dialogueRunner;
    public Camera _camera;
    void Start()
    {
        _gameManager = GameObject.FindAnyObjectByType<GameManager>();
        _dialogueRunner = _gameManager._dialogueRunner;
    }

    void Update()
    {
        
        if (_dialogueRunner.Dialogue.IsActive)
        {
            Cursor.lockState = CursorLockMode.None;
            gameObject.GetComponentInChildren<PlayerMovement>().enabled = false;
            gameObject.GetComponentInChildren<PlayerLook>().enabled = false;

            if (Input.GetKeyUp(KeyCode.Escape))
            {
                _gameManager.StopInteraction();
            }
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
            gameObject.GetComponentInChildren<PlayerMovement>().enabled = true;
            gameObject.GetComponentInChildren<PlayerLook>().enabled = true;
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
}
