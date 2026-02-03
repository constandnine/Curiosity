using Unity.VisualScripting;
using UnityEngine;

public class PlanetOrbitSimulator : MonoBehaviour
{
    [SerializeField]
    float orbitSpeed;
    [SerializeField]
    float speedupScale;
    [SerializeField]
    GameObject sun;

    private void Start()
    {
        // Sets the speedupscale by multiplying the days you enterd by the amount of seconds in a day
        speedupScale *= 86400;
    }

    void Update()
    {
        SimulateOrbit();
    }

    void SimulateOrbit()
    {
        //Makes the planet rotate around the suns y axis
        transform.RotateAround(sun.transform.position, Vector3.up, -(orbitSpeed * speedupScale )* Time.deltaTime);
    }
}
