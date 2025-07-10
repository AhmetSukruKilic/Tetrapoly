using System;
using UnityEngine;
using UnityEngine.PlayerLoop;


public class Car : MonoBehaviour
{
    private string ownerName;
    private MovementState movementState;
    private static int carCount = 0;
    private void Start()
    {
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

    
}
