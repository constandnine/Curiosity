using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    private InputManager inputManager;

    [Header("Rotation")]

    [SerializeField] private float deadZone;
    [SerializeField] private float rotationSpeed;

    public Transform target;

    private bool rotating;

    private void Awake()
    {
        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.Enable();

        inputManager.Camera.RightMouseButton.performed += ctx => rotating = true;
        inputManager.Camera.RightMouseButton.canceled += ctx => rotating = false;
    }

    private void OnDisable()
    {
        inputManager.Camera.RightMouseButton.performed -= ctx => rotating = true;
        inputManager.Camera.RightMouseButton.canceled -= ctx => rotating = false;


        inputManager.Disable();
    }

    public void Update()
    {
        if (!rotating) return;

        Debug.Log("Executed");
        float delta = inputManager.Camera.MouseDrag.ReadValue<Vector2>().x;
        Debug.Log(delta);
        int direction = 0;

        if (delta > deadZone) direction = 1;
        else if (delta < -deadZone) direction = -1;
        Debug.Log(direction);
        if (direction != 0)
        {
            transform.RotateAround(target.position, Vector3.up, direction * rotationSpeed * Time.deltaTime);
        }
    }

    public void CameraZoom()
    {

    }

    public void CameraPanning()
    {

    }
}
