using UnityEngine;

public class PlanetTargetRotation : MonoBehaviour
{
    [SerializeField]
    Vector3 newRotation;

    public void SetPlanetRotation()
    {
        transform.localEulerAngles = newRotation;
    }
}
