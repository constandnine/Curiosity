using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class CameraMovement : MonoBehaviour
{
    public static CameraMovement instance;

    private InputManager inputManager;

    [SerializeField] private Camera mainCamera;

    [Header("Rotation")]

    [SerializeField] private float deadZone;
    [SerializeField] private float rotationSpeed;

    public Transform target;

    private bool rotating;

    [Header("Zooming")]

    [SerializeField] private float zoomSpeed;

    [Header("Panning")]

    [SerializeField] private float panningSpeed;

    private bool panning;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }

        else if (instance != this)
        {
            Destroy(gameObject);
        }

        inputManager = new InputManager();
    }

    private void OnEnable()
    {
        inputManager.Enable();

        inputManager.Camera.RightMouseButton.performed += context => rotating = true;
        inputManager.Camera.RightMouseButton.canceled += context => rotating = false;

        inputManager.Camera.MidleMouseButton.performed += context => panning = true;
        inputManager.Camera.MidleMouseButton.canceled += context => panning = false;
    }

    private void OnDisable()
    {
        inputManager.Camera.RightMouseButton.performed -= context => rotating = true;
        inputManager.Camera.RightMouseButton.canceled -= context => rotating = false;

        inputManager.Camera.MidleMouseButton.performed += context => panning = true;
        inputManager.Camera.MidleMouseButton.canceled += context => panning = false;


        inputManager.Disable();
    }

    public void Update()
    {
        RotateCamera();
        CameraZoom();
        CameraPanning();
    }

    private void RotateCamera()
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
        Vector2 delta = inputManager.Camera.MidleMouseScroll.ReadValue<Vector2>();
        float scroll = delta.y;

        if (scroll != 0)
        {
            mainCamera.fieldOfView -= scroll * zoomSpeed;
        }
    }

    public void CameraPanning()
    {
        if (!panning) return;

        // Read horizontal mouse movement
        Vector2 delta = inputManager.Camera.MouseDrag.ReadValue<Vector2>();
        int direction = 0;

        if (delta.x > deadZone) direction = 1;
        else if (delta.x < -deadZone) direction = -1;

        if (direction != 0)
        {
            // Rotate around the target horizontally
            transform.RotateAround(transform.position, Vector3.up, direction * panningSpeed * Time.deltaTime);
        }
    }
}
