using UnityEngine;

public class ViewNavigation : MonoBehaviour
{
    //REMOVE LATER
    public Camera Camera;
    public Transform CameraPosition;

    public void MoveCamera()
    {
        Camera.transform.position = CameraPosition.position;
    }
}
