using UnityEngine;

public class MovementArrowManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject rightArrowPrefab;
    [SerializeField] private GameObject leftArrowPrefab;
    [SerializeField] private GameObject topArrowPrefab;
    [SerializeField] private GameObject bottomArrowPrefab;
    [SerializeField] private Car playerCar;

    private void Start()
    {
        SetArrowVisibility();
    }
    
    internal void SetArrowVisibility()
    {
        rightArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.UpMove);
        topArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.UpMove);

        leftArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.DownMove);
        bottomArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.DownMove);
    }
}
