using System.Collections.Generic;
using UnityEngine;


public class Car : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    private const int INITIALMONEY = 1000;
    internal const int FUELCAPACITY = 18;

    internal (int, int) currentCell;
    private int currentMoney;
    private string ownerName;
    private MovementState movementState;
    private List<DistrictCell> boughtDistricts = new();
    internal Vector3 initialPosition;
    internal int currentFuel = 0;

    private static int carCount = 0;

    private void Start()
    {
        currentMoney = INITIALMONEY;
        currentCell = (0, 0); // Starting at cell (0, 0)
        initialPosition = transform.position - new Vector3(240, 0, 0); // Adjust initial position to be slightly above the ground
        movementState = MovementState.UpMove;
        ownerName = "Default Owner" + carCount++;
    }
    internal string GetOwnerName()
    {
        return ownerName;
    }

    internal void SetOwnerName(string name)
    {
        ownerName = name;
    }

    internal MovementState GetMovementState()
    {
        return movementState;
    }
    internal void ReverseMovementState()
    {
        if (movementState == MovementState.UpMove)
        {
            movementState = MovementState.DownMove;
        }
        else if (movementState == MovementState.DownMove)
        {
            movementState = MovementState.UpMove;
        }
    }

    internal void ChargeCar(int rollResult)
    {
        if (currentFuel + rollResult <= FUELCAPACITY)
        {
            currentFuel += rollResult;
        }
        else
        {
            gridManager.MoveCarToRandomJailCell(this);
            currentFuel = 0;
            currentMoney -= 250;
        }
    }

    internal void BuyDistrict(DistrictCell district)
    {
        if (currentMoney >= district.GetPrice() && currentFuel > 0)
        {
            currentMoney -= district.GetPrice();
            boughtDistricts.Add(district);
            district.AddCar(this);
            Debug.Log($"{ownerName} bought {district.GetDistrictName()} for ${district.GetPrice()}");
        }
        else
        {
            Debug.LogWarning($"{ownerName} cannot afford {district.GetDistrictName()} or has no fuel.");
        }
    }
    
    internal void TradeFuelToMoney()
    {
        if (currentFuel > 0)
        {
            int fuelToMoney = currentFuel * 50;
            currentMoney += fuelToMoney;
            currentFuel = 0; // 
            Debug.Log($"{ownerName} traded {fuelToMoney} money for {currentFuel} fuel.");
        }
        else
        {
            Debug.LogWarning($"{ownerName} has no fuel to trade.");
        }
    }
}
