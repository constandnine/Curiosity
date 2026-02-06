using UnityEngine;

public class PlanetAxisRotation : MonoBehaviour
{
    public float rotationsPerMinute;
    [SerializeField]
    float axialTilt;
    [SerializeField]
    Transform planet;

    private void Start()
    {
        transform.rotation = Quaternion.Euler(transform.rotation.x, transform.rotation.y, -axialTilt);
    }

    bool checkStart = false;
    bool checkEnd = false;

    void Update()
    {
        planet.localRotation = Quaternion.Euler(planet.rotation.x, Time.time * (rotationsPerMinute * 6), planet.rotation.z);
 
        if (planet.localEulerAngles.y > -15 && !checkStart)
        {
            if (!checkStart)
            {
                checkStart = true;
            }
        }
        if (planet.localEulerAngles.y > -15 && planet.localEulerAngles.y < 15 && checkEnd)
        {
            checkEnd = false;
            checkStart = false;
            Debug.Log("AxisRotation: " + Time.time);
        }

        if(planet.localEulerAngles.y >= 15 && checkStart == true)
        {
            checkEnd = true;
        }
    }
}
