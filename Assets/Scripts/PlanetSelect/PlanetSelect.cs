using UnityEngine;
using UnityEngine.Events;

public class PlanetSelect : MonoBehaviour
{
    public UnityEvent OnPlanetSelected;

    public void PlanetSelected()
    {
        OnPlanetSelected?.Invoke();
    }
}
