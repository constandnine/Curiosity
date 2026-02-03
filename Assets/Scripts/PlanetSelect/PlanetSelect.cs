using UnityEngine;
using UnityEngine.Events;

public class PlanetSelect : MonoBehaviour
{
    public UnityEvent OnPlanetSelected;

    /// <summary>
    /// Invokes a event when a planet gets selected.
    /// </summary>
    public void PlanetSelected()
    {
        OnPlanetSelected?.Invoke();
    }
}