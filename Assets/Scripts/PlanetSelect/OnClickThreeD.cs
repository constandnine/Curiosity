using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OnClickThreeD : MonoBehaviour
{
    public UnityEvent onClick;
    public UnityEvent onDoubleClick;

    private Camera mainCamera;

    private InputManager inputManager;

    [SerializeField] private float waitForClickTime = 0.3f;
    private bool waitingForSecondClick;
    private void Awake()
    {
        mainCamera = Camera.main;
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.Enable();
        inputManager.Camera.MouseClick.performed += SingleClickDetected;
        inputManager.Camera.DoubleMouseClick.performed += DoubleClickDetected;
    }

    private void OnDisable()
    {
        inputManager.Camera.MouseClick.performed -= SingleClickDetected;
        inputManager.Camera.DoubleMouseClick.performed -= DoubleClickDetected;
        inputManager.Disable();
    }

    private void CheckClick(UnityEvent thisEvent)
    {
        // Shoots the Raycast from the mouse itself and than adds a RaycastHit to it so it can check
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Checks if the object has been hit.
            if (hit.transform == transform)
            {
                Debug.Log($"Hit a planet {hit.transform}", this);
                thisEvent?.Invoke();
            }
        }
    }

    private void SingleClickDetected(InputAction.CallbackContext context)
    {
        if (!waitingForSecondClick) StartCoroutine(WaitForSecondClick());

    }

    private void DoubleClickDetected(InputAction.CallbackContext context)
    {
        waitingForSecondClick = false;
        CheckClick(onDoubleClick);
        Debug.Log("Double Click");
    }

    private IEnumerator WaitForSecondClick()
    {
        waitingForSecondClick = true;
        yield return new WaitForSeconds(waitForClickTime);

        if (!waitingForSecondClick) yield break;
        CheckClick(onClick);
        Debug.Log("SingleClick");

        waitingForSecondClick = false;
    }
}