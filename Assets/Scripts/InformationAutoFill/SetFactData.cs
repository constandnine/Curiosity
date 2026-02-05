using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class SetFactData : MonoBehaviour
{
    public FactData factData;

    [Header("UI")]

    public TextMeshProUGUI planetName;
    public TextMeshProUGUI factDescription;

    public Image factImage;

    private void Start()
    {
        // prevents NullReference if no factData was found.
        if (factData == null)
        {
            Debug.LogError("No FactData assigned", this);
            return;
        }

        SetData();
    }

    public void SetData()
    {
        // Stops the entire process if there is no planet name or fact description.
        if (factData.name == null || factData.factDescription == null)
        {
            Debug.LogError($"You are missing crucial information in {factData} check wether you have filled in both the planet name and the fact description", this);
            return;
        }

        // Sets the data from factData to a visual TextMeshPro component
        planetName.text = factData.name;
        factDescription.text = factData.factDescription;

        // Checks if there is a sprite to fill the factImage to be filled, otherwise it will return.
        if (factImage == null)
        {
            Debug.LogWarning($"No image found in {factData} check if this was intended", this);
            return;
        }

        // Sets the sprite from factData to a image.
        factImage.sprite = factData.factImage;
    }
}
