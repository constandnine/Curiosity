using UnityEngine;

public class ViewNavigation : MonoBehaviour
{
    public Transform CameraPosition;

    public void MoveCamera()
    {
        SmoothCameraSwitch.instance.SmoothCameraRotation(CameraPosition);
        Debug.Log($"Move camera towards {CameraPosition}", this);
    }
}