using UnityEngine;
using TMPro;


public class DistrictCell : MonoBehaviour
{
    [SerializeField] private TextMeshPro nameLabel;
    [SerializeField] private TextMeshPro priceLabel;
    [SerializeField] private TextMeshPro ownerLabel;
    [SerializeField] private Renderer blockRenderer;

    private DistrictType districtType;
    private Vector3 position;
    private int areaId;
    private const int MaxCars = 4;
    private Car[] cars = new Car[MaxCars];
    private int carCount = 0;
    private string districtName;
    private int price;
    private Car owner;
    private static int idCount = 0;
    private static System.Collections.Generic.Dictionary<Color, int> colorToId = new();
    public void Init(CellInfo info, Vector3 position = default)
    {
        this.position = position;
        this.districtName = info.name;
        this.price = info.price;
        this.owner = info.owner;
        this.blockRenderer.material.color = info.color;
        this.districtType = info.districtType;

        AssignIdToColor(info.color);

        AssignLabels();

    }

    private void AssignLabels()
    {
        if (districtType == DistrictType.Base)
        {
            UpdateBaseLabels();
        }
        else if (districtType == DistrictType.Jail)
        {
            UpdateJailLabels();
        }
        else if (districtType == DistrictType.City)
        {
            UpdateCityLabels();
        }

    }

    private void AssignIdToColor(Color color)
    {
        if (!colorToId.TryGetValue(color, out int id))
        {
            id = idCount++;
            colorToId[color] = id;
        }
        areaId = id;
    }

    private void UpdateBaseLabels()
    {
        nameLabel.text = "Base";
        priceLabel.text = null;
        ownerLabel.text = null;
    }
    private void UpdateCityLabels()
    {
        nameLabel.text = districtName;
        priceLabel.text = "$" + price;
        ownerLabel.text = "Owner: " + (owner != null ? owner.GetOwnerName() : "None");
    }

    private void UpdateJailLabels()
    {
        nameLabel.text = "Jail";
        priceLabel.text = null;
        ownerLabel.text = null;
    }

    public void SetOwner(Car newOwner, Color ownerColor)
    {
        owner = newOwner;
        UpdateCityLabels();
        blockRenderer.material.color = ownerColor;
    }
}
