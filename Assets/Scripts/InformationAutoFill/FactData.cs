using UnityEngine;

[CreateAssetMenu(menuName = "planetFacts/NewPlanetFact", fileName = "NewPlanetFact")]
public class FactData : ScriptableObject
{
    [Header("Text")]

    public string planetName;
    public string factDescription;

    [Header("Image")]

    public Sprite factImage;
}