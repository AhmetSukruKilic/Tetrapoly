using System;
using TMPro;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject districtPrefab;
    [SerializeField] private RollTheDice rollTheDice;
    [SerializeField] private TextMeshProUGUI carFuelText;
    [SerializeField] private TextMeshProUGUI carMoneyText;
    [SerializeField] private TextMeshProUGUI carNameText;

    [SerializeField] private Car[] cars;

    private Vector3 prefabSize;
    private const int width = 8;
    private const int height = 8;
    private const float offset = 1.1f;
    internal DistrictCell[][] districtCells;

    private void Start()
    {
        districtCells = gridManager.districtCells;
        prefabSize = GetPrefabBounds();
        AdjustCamera();
        rollTheDice.AssignPlayers(cars);
    }

    private void AdjustCamera()
    {
        float cameraHeight = Mathf.Max(prefabSize.x, prefabSize.z) * (width + height) / 8 * 3.2f;
        mainCamera.transform.position = new Vector3(
            (width - 1.5f) * prefabSize.x / 2f,
            cameraHeight,
            (height - 1.5f) * prefabSize.z / 2f
        );
        mainCamera.transform.LookAt(new Vector3(
            (width - 1.5f) * prefabSize.x / 2f,
            0,
            (height - 1.5f) * prefabSize.z / 2f
        ));
    }

    Vector3 GetPrefabBounds()
    {
        GameObject temp = Instantiate(districtPrefab);
        Renderer rend = temp.transform.Find("DistrictBlock").GetComponent<Renderer>();
        Vector3 size = rend.bounds.size * offset;
        Destroy(temp);
        return size;
    }


    internal void MoveCarWithDirectionArrows(Car playerCar, MovementDirection direction)
    {
        if (playerCar == null) return;

        Debug.Log($"Moving car {playerCar.GetOwnerName()} in direction {direction}");

        if (playerCar.currentFuel <= 0)
        {
            Debug.LogWarning($"Car {playerCar.GetOwnerName()} has no fuel to move.");
            return;
        }

        playerCar.currentFuel -= 1; // Decrease fuel by 1 for each move
        switch (direction)
        {   // (z,x) coordinates
            case MovementDirection.Up:
                gridManager.MoveCarToSpesificCell(playerCar, playerCar.currentCell.Item1 + 1, playerCar.currentCell.Item2);
                break;
            case MovementDirection.Down:
                gridManager.MoveCarToSpesificCell(playerCar, playerCar.currentCell.Item1 - 1, playerCar.currentCell.Item2);
                break;
            case MovementDirection.Left:
                gridManager.MoveCarToSpesificCell(playerCar, playerCar.currentCell.Item1, playerCar.currentCell.Item2 - 1);
                break;
            case MovementDirection.Right:
                gridManager.MoveCarToSpesificCell(playerCar, playerCar.currentCell.Item1, playerCar.currentCell.Item2 + 1);
                break;
        }
        ReloadFuelText(playerCar);
    }

    internal void ReloadAllTexts(Car car)
    {
        ReloadFuelText(car);
        ReloadCarNameText(car);
        ReloadMoney(car);
    }

    internal void ReloadMoney(Car car)
    {
        carMoneyText.text = "Money:" + car.GetCurrentMoney();
    }

    private void ReloadCarNameText(Car car)
    {
        carNameText.text = car.GetOwnerName();
    }

    internal void ReloadFuelText(Car car)
    {
        carFuelText.text = "Fuel:" + car.currentFuel.ToString() + "/" + Car.FUELCAPACITY.ToString();
    }
}
