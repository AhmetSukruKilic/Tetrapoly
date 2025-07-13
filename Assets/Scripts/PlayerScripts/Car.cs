using System;
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
    private static int carCount = 0;
    internal Vector3 initialPosition;
    internal int currentFuel = 0;
    
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
}
