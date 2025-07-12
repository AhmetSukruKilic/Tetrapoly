using System;
using UnityEngine;


public class Car : MonoBehaviour
{   
    [SerializeField] private GridManager gridManager;
    private const int INITIALMONEY = 1000;
    internal const int FUELCAPACITY = 18;

    private int currentMoney;
    private string ownerName;
    private MovementState movementState;
    private static int carCount = 0;
    internal Vector3 initialPosition;
    internal int currentFuel = 0;
    private void Start()
    {
        currentMoney = INITIALMONEY;
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
    internal void ReverseMovementState(MovementState state)
    {
        if (state == MovementState.UpMove)
        {
            movementState = MovementState.DownMove;
        }
        else if (state == MovementState.DownMove)
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
