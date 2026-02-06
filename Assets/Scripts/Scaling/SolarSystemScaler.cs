using System;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class SolarSystemScaler : MonoBehaviour
{
    [Serializable]
    public class PlanetScale
    {
        public Transform planet;
        public OrbitVisualizer visualizer;
        public float realisticScale;
        public float friendlyScale;
        public float realisticPosition;
        public float friendlyPosition;
        public float realisticLinerenderWidth;
        public float friendlyLinerenderWidth;
        [NonSerialized]
        public bool moved = false;
    }

    [SerializeField] PlanetScale[] planets;

    public void Awake()
    {
        for (int i = 0; i < planets.Length; i++)
        {
            if (planets[i].planet != null)
            {
                planets[i].realisticScale = planets[i].planet.localScale.x;
                planets[i].realisticPosition = planets[i].planet.localPosition.z;
            }

            planets[i].realisticLinerenderWidth = planets[i].visualizer.GetComponent<LineRenderer>().startWidth;

            if (planets[i].friendlyLinerenderWidth == 0)
            {
                planets[i].friendlyLinerenderWidth = planets[i].visualizer.GetComponent<LineRenderer>().startWidth;
            }
        }
    }

    public void OnToggleChanged()
    {
        Toggle toggle = GetComponent<Toggle>();
        if (toggle == null)
        {
            return;
        }

        bool useFriendly = toggle.isOn;

        for (int i = 0; i < planets.Length; i++)
        {
            Transform planetTransform = planets[i].planet;

            if (useFriendly)
            {
                if (planetTransform != null)
                {
                    planetTransform.localScale = Vector3.one * planets[i].friendlyScale;
                }

                if (planets[i].friendlyPosition  != 0)
                {
                    if (planetTransform != null)
                    {
                        planetTransform.position = new Vector3(0, 0, planets[i].friendlyPosition);
                    }
                    planets[i].visualizer.orbitSize = planets[i].friendlyPosition;
                    planets[i].visualizer.GetComponent<LineRenderer>().startWidth = planets[i].friendlyLinerenderWidth;
                    planets[i].visualizer.GetComponent<LineRenderer>().endWidth = planets[i].friendlyLinerenderWidth;
                    planets[i].visualizer.SetVisualOrbit();
                    planets[i].moved = true;
                }
            }
            else
            {
                if (planetTransform != null)
                {
                    planetTransform.localScale = Vector3.one * planets[i].realisticScale;
                }

                if (planets[i].moved)
                {
                    if (planetTransform != null)
                    {
                        planetTransform.position = new Vector3(0, 0, planets[i].realisticPosition);
                    }
                    planets[i].moved = false;
                    planets[i].visualizer.orbitSize = planets[i].realisticPosition;
                    planets[i].visualizer.GetComponent<LineRenderer>().startWidth = planets[i].realisticLinerenderWidth;
                    planets[i].visualizer.GetComponent<LineRenderer>().endWidth = planets[i].realisticLinerenderWidth;
                    planets[i].visualizer.SetVisualOrbit();
                }
            }
        }
    }
}
