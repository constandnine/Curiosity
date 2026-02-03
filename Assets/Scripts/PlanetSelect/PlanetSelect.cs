using UnityEngine;
using UnityEngine.Events;
using UnityEngine.InputSystem;

public class PlanetSelect : MonoBehaviour
{
    public UnityEvent OnCLick;
    public UnityEvent OnPlanetSelected;

    private Camera mainCamera;

    private InputManager inputManager;

    private void Awake()
    {
        mainCamera = Camera.main;
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.Enable();
        inputManager.Camera.MouseClick.performed += OnClick;
    }

    private void OnDisable()
    {
        inputManager.Disable();
        inputManager.Camera.MouseClick.performed -= OnClick;
    }

    public void OnClick(InputAction.CallbackContext context)
    {
        // Checks if context has been performed.
        if (!context.performed) return;

        // Shoots the Raycast from the mouse itself and than adds a 
        Ray ray = mainCamera.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            // Checks if the object has been hit.
            if (hit.transform == transform)
            {
                Debug.Log($"Hit a planet {hit.transform}", this);
            }
        }
    }
}