using UnityEngine;
using TMPro;
using System;


public class DistrictCell : MonoBehaviour
{
    [SerializeField] private TextMeshPro nameLabel;
    [SerializeField] private TextMeshPro priceLabel;
    [SerializeField] private TextMeshPro ownerLabel;
    [SerializeField] private Renderer blockRenderer;

    private DistrictType districtType;
    private Vector3 vectorPosition;
    private (int, int) gridPosition;
    private int areaId;
    private const int MaxCars = 4;
    private readonly Car[] cars = new Car[MaxCars];
    private int carCount = 0;
    private string districtName;
    private int price;
    private Car owner;
    private static int idCount = 0;
    private static System.Collections.Generic.Dictionary<Color, int> colorToId = new();
    public void Init(CellInfo info, Vector3 vectorPosition = default, (int, int) gridPosition = default)
    {
        this.gridPosition = gridPosition;
        this.vectorPosition = vectorPosition;

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

    internal void AddCar(Car car)
    {
        if (carCount < MaxCars)
        {
            cars[carCount++] = car;
            car.currentCell = gridPosition;
            
            if (districtType == DistrictType.Base)
            {
                car.ReverseMovementState();
            }
        }
        else
        {
            Debug.LogWarning("Max number of cars reached for this district.");
        }
    }  

    internal void RemoveCar(Car car)
    {
        for (int i = 0; i < carCount; i++)
        {
            if (cars[i] == car)
            {
                cars[i] = null;
                carCount--;
                // Shift remaining cars
                for (int j = i; j < carCount; j++)
                {
                    cars[j] = cars[j + 1];
                }
                cars[carCount] = null; // Clear last element
                break;
            }
        }
    }
}
