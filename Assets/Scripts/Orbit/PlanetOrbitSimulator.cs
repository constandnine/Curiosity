using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms;

public class PlanetOrbitSimulator : MonoBehaviour
{
    [SerializeField]
    float dayPerOrbit;
    public float DaysPerSecond;
    [SerializeField]
    GameObject sun;
    [SerializeField]
    bool staticOrbit;
    [SerializeField]
    OrbitVisualizer visualizer;
    [SerializeField]
    RotateAxis rotationAxis = RotateAxis.Y;

    private void Start()
    {
        if (staticOrbit)
        {
            if (visualizer)
            {
                visualizer.SetVisualOrbit();
            }
        }
    }

    void Update()
    {
        if (!staticOrbit)
        {
            if (visualizer)
            {
                visualizer.SetVisualOrbit();
            }
        }
        SimulateOrbit();

        if (!staticOrbit)
        {
            if (visualizer)
            {
                visualizer.SetVisualOrbit();
            }
        }
    }

    bool check = false;
    void SimulateOrbit()
    {
        float dayPerSecondInSeconds = DaysPerSecond * 86400f;
        float degreesPerDay = 360f / dayPerOrbit;
        float degreesPerSecond = (degreesPerDay / 86400f) * dayPerSecondInSeconds;

        //Makes the planet rotate around the suns y axis
        if(rotationAxis == RotateAxis.Y)
        {
            transform.RotateAround(sun.transform.position, Vector3.up, -degreesPerSecond * Time.deltaTime);

        }
        else if (rotationAxis == RotateAxis.X)
        {
            transform.RotateAround(sun.transform.position, Vector3.right, -degreesPerSecond * Time.deltaTime);
        }
        else if (rotationAxis == RotateAxis.Z)
        {
            transform.RotateAround(sun.transform.position, Vector3.forward, -degreesPerSecond * Time.deltaTime);
        }

        if (transform.position.x > 0 && !check)
        {
            check = true;
            //Debug.Log("Planet time" + Time.time * 2);
        }
    }

    [System.Serializable]
    public enum RotateAxis
    {
        Y, 
        X,
        Z
    }
}
