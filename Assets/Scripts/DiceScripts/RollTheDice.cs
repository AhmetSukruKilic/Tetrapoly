using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RollTheDice : MonoBehaviour
{
    [SerializeField] private GameObject dicePrefab1;
    [SerializeField] private GameObject dicePrefab2;
    [SerializeField] private GameObject diceButton;
    [SerializeField] private TextMeshProUGUI rollButtonText;
    [SerializeField] private TextMeshProUGUI diceResultFuelText;
    [SerializeField] private MovementArrowManager movementArrowManager;
    private Car currentPlayer;
    private bool hasRolled = false;

    private Quaternion initialRotation1;
    private Quaternion initialRotation2;
    private Queue<Car> playersQueue = new Queue<Car>();

    void Start()
    {
        initialRotation1 = dicePrefab1.transform.rotation;
        initialRotation2 = dicePrefab2.transform.rotation;
        diceResultFuelText.text = "Fuel = " + 0 + "/" + Car.FUELCAPACITY.ToString();
    }

    internal void AssignPlayers(Car[] players)
    {
        foreach (Car player in players)
        {
            playersQueue.Enqueue(player);
        }
    }

    public void OnDiceButtonClicked()
    {
        if (!hasRolled)
        {
            if (playersQueue.Count > 0)
            {
                currentPlayer = playersQueue.Dequeue();

                int rollResult = RollDice(currentPlayer);
                currentPlayer.ChargeCar(rollResult);
                movementArrowManager.SetPlayerCar(currentPlayer);

                hasRolled = true;
                rollButtonText.text = "Next"; // update button text
            }
            else
            {
                Debug.LogWarning("No players in the queue to roll the dice.");
            }
        }
        else
        {
            ReloadFuelText(playersQueue.Peek());
            playersQueue.Enqueue(currentPlayer);
            currentPlayer = null;

            movementArrowManager.SetPlayerCar(null);
            hasRolled = false;
            rollButtonText.text = "Roll"; // reset button text
        }
    }
    
    public int RollDice(Car car)
    {
        int rollResult1 = Random.Range(1, 7); // returns 1–6
        switch (rollResult1)
        {
            case 1:
                dicePrefab1.transform.rotation = initialRotation1 * Quaternion.Euler(270, 0, 0);
                break;
            case 2:
                dicePrefab1.transform.rotation = initialRotation1;
                break;
            case 3:
                dicePrefab1.transform.rotation = initialRotation1 * Quaternion.Euler(0, 0, 270);
                break;
            case 4:
                dicePrefab1.transform.rotation = initialRotation1 * Quaternion.Euler(0, 0, 90);
                break;
            case 5:
                dicePrefab1.transform.rotation = initialRotation1 * Quaternion.Euler(180, 0, 0);
                break;
            case 6:
                dicePrefab1.transform.rotation = initialRotation1 * Quaternion.Euler(90, 0, 0);
                break;
            default:
                UnityEngine.Debug.LogError("Invalid roll.");
                break;
        }

        // For the second dice, we can use the same logic or modify it as needed
        int rollResult2 = Random.Range(1, 7); // returns 1–6    
        switch (rollResult2)
        {
            case 1:
                dicePrefab2.transform.rotation = initialRotation2 * Quaternion.Euler(270, 0, 0);
                break;
            case 2:
                dicePrefab2.transform.rotation = initialRotation2;
                break;
            case 3:
                dicePrefab2.transform.rotation = initialRotation2 * Quaternion.Euler(0, 0, 270);
                break;
            case 4:
                dicePrefab2.transform.rotation = initialRotation2 * Quaternion.Euler(0, 0, 90);
                break;
            case 5:
                dicePrefab2.transform.rotation = initialRotation2 * Quaternion.Euler(180, 0, 0);
                break;
            case 6:
                dicePrefab2.transform.rotation = initialRotation2 * Quaternion.Euler(90, 0, 0);
                break;
            default:
                UnityEngine.Debug.LogError("Invalid roll for second dice.");
                break;
        }

        int rollResult = rollResult1 + rollResult2; // Sum of both dice rolls

        if (rollResult > Car.FUELCAPACITY - car.currentFuel)
        {
            diceResultFuelText.text = "Fuel = " + 0.ToString() + "/" + Car.FUELCAPACITY.ToString();
        }
        else
        {
            diceResultFuelText.text = "Fuel = " + (rollResult + car.currentFuel).ToString() + "/" + Car.FUELCAPACITY.ToString();
        }
        return rollResult;
    }

    public void ReloadFuelText(Car car)
    {
        diceResultFuelText.text = "Fuel = " + car.currentFuel.ToString() + "/" + Car.FUELCAPACITY.ToString();
    }
    

    
}
