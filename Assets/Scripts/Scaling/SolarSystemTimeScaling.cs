using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystemTimeScaling : MonoBehaviour
{
    [System.Serializable]
    public class PlanetScales
    {
        public GameObject planet;
        public float rotationsPerMinute;
        public float daysPerSecond;
    }

    public PlanetScales[] defaultScales;

    Slider slider;
    [SerializeField]
    TextMeshProUGUI sliderText;
    void Start()
    {
        slider = GetComponent<Slider>();

        for (int i = 0; i < defaultScales.Length; i++)
        {
            if(defaultScales[i].planet.GetComponent<PlanetAxisRotation>() != null)
            {
                defaultScales[i].rotationsPerMinute = defaultScales[i].planet.GetComponent<PlanetAxisRotation>().rotationsPerMinute / 40;
                defaultScales[i].planet.GetComponent<PlanetAxisRotation>().rotationsPerMinute = defaultScales[i].rotationsPerMinute * slider.value;
            }

            if(defaultScales[i].planet.GetComponent<PlanetOrbitSimulator>()  != null)
            {
                defaultScales[i].daysPerSecond = defaultScales[i].planet.GetComponent<PlanetOrbitSimulator>().DaysPerSecond / 40;
                defaultScales[i].planet.GetComponent<PlanetOrbitSimulator>().DaysPerSecond = defaultScales[i].daysPerSecond * slider.value;
            }
        }
    }

    public void OnSliderUpdate()
    {
        for (int i = 0; i < defaultScales.Length; i++)
        {
            if (defaultScales[i].planet.GetComponent<PlanetAxisRotation>() != null)
            {
                defaultScales[i].planet.GetComponent<PlanetAxisRotation>().rotationsPerMinute = defaultScales[i].rotationsPerMinute * slider.value;
            }
            if (defaultScales[i].planet.GetComponent<PlanetOrbitSimulator>() != null)
            {
                defaultScales[i].planet.GetComponent<PlanetOrbitSimulator>().DaysPerSecond = defaultScales[i].daysPerSecond * slider.value;
            }
        }

        sliderText.text = slider.value.ToString("F1");
    }

    public void SetPlanetTimeScaling(float timescale)
    {
        for (int i = 0; i < defaultScales.Length; i++)
        {
            if (defaultScales[i].planet.GetComponent<PlanetAxisRotation>() != null)
            {
                defaultScales[i].planet.GetComponent<PlanetAxisRotation>().rotationsPerMinute = defaultScales[i].rotationsPerMinute * timescale;
            }
            if (defaultScales[i].planet.GetComponent<PlanetOrbitSimulator>() != null)
            {
                defaultScales[i].planet.GetComponent<PlanetOrbitSimulator>().DaysPerSecond = defaultScales[i].daysPerSecond * timescale;
            }
        }

        sliderText.text = timescale.ToString("F1");
    }
}
