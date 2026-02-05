using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class OnClickThreeD : MonoBehaviour
{
    [Header("")]
    public UnityEvent onClick;
    public UnityEvent onDoubleClick;

    private Camera mainCamera;

    private InputManager inputManager;

    [Header("Click Waiting")]

    [SerializeField] private float waitForClickTime = 0.3f;
    private bool waitingForSecondClick;
    private bool oneClick;

    private void Awake()
    {
        //Sets variables.
        mainCamera = Camera.main;
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        // Subscribes events on enable.
        inputManager.Enable();
        inputManager.Camera.MouseClick.performed += SingleClickDetected;
        inputManager.Camera.DoubleMouseClick.performed += DoubleClickDetected;
    }

    private void OnDisable()
    {
        //Unsubscribes events on disable.
        inputManager.Camera.MouseClick.performed -= SingleClickDetected;
        inputManager.Camera.DoubleMouseClick.performed -= DoubleClickDetected;
        inputManager.Disable();
    }

    private void ExecuteClick(UnityEvent thisEvent)
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
                if (!oneClick) return;
                CameraMovement.instance.target = hit.transform;
                oneClick = false;
            }
        }
    }

    private void SingleClickDetected(InputAction.CallbackContext context)
    {
        // starts a waiting process to check if there is no double click.
        if (!waitingForSecondClick) StartCoroutine(WaitForSecondClick());
    }

    private void DoubleClickDetected(InputAction.CallbackContext context)
    {
        // Sets the waiter to false and then executes the ExecuteClick function with the right event.
        waitingForSecondClick = false;
        ExecuteClick(onDoubleClick);
        Debug.Log("Double Click");
    }

    private IEnumerator WaitForSecondClick()
    {
        // Sets the waiter to true before the timer.
        waitingForSecondClick = true;
        // Timer to check if a second click happenes.
        yield return new WaitForSeconds(waitForClickTime);

        //If second click did happen it set the waiter to false witch means we need to stop running the code.
        // How ever if this didn't happen we execute the ExecuteClick function with the right event.
        if (!waitingForSecondClick) yield break;
        oneClick = true;
        ExecuteClick(onClick);
        Debug.Log("SingleClick");

        // Reset the waiter to false
        waitingForSecondClick = false;
    }
}