
using TMPro;
using UnityEngine;
using UnityEngine.InputSystem;

public class Interaction : MonoBehaviour
{
    public float checkRate = 0.05f;
    private float lastCheckTime;
    public float maxCheckDistance;
    public LayerMask layerMask;

    public GameObject curInteractGameObject;
    private IInteractable curInteractable;

    public GameObject promptPanel;
    public TextMeshProUGUI promptText;
    private Camera _camera;

    void Start()
    {
        _camera = Camera.main;
    }

    void Update()
    {
        if (Time.time - lastCheckTime > checkRate)
        {
            lastCheckTime = Time.time;

            Ray[] rays = new Ray[5]
            {
            new Ray(transform.position, transform.forward),
            new Ray(transform.position, transform.forward),
            new Ray(transform.position, transform.right),
            new Ray(transform.position, transform.right),
            new Ray(transform.position, transform.up)
            };

            RaycastHit hit;

            for (int i = 0; i < rays.Length; i++)
            {
                if (Physics.Raycast(rays[i], out hit, maxCheckDistance, layerMask))
                {
                    if (hit.collider.gameObject != curInteractGameObject)
                    {
                        curInteractGameObject = hit.collider.gameObject;
                        curInteractable = hit.collider.GetComponent<IInteractable>();
                        SetPromptText();
                    }
                }
                else
                {
                    curInteractGameObject = null;
                    curInteractable = null;
                    promptPanel.SetActive(false);
                }
            }
        }
    }

    private void SetPromptText()
    {
        promptPanel.SetActive(true);
        promptText.text = curInteractable.GetInteractPrompt();
    }

    public void OnInteractInput(InputAction.CallbackContext context)
    {
        if (context.phase == InputActionPhase.Started && curInteractable != null)
        {
            curInteractable.OnInteract();
            curInteractGameObject = null;
            curInteractable = null;
            promptPanel.SetActive(false);
        }
    }
}
