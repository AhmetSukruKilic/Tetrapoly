using UnityEngine;

public class MovementArrowManager : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    [SerializeField] private GameObject rightArrowPrefab;
    [SerializeField] private GameObject leftArrowPrefab;
    [SerializeField] private GameObject topArrowPrefab;
    [SerializeField] private GameObject bottomArrowPrefab;
    [SerializeField] private Car playerCar;
    [SerializeField] private GameManager gameManager;

    private const int width = 8;
    private const int height = 8;

    private void Start()
    {
        SetArrowVisibility();
    }

    internal void SetArrowVisibility()
    {
        if (playerCar == null || gameManager == null) return;

        rightArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.UpMove
                                 && playerCar.currentCell.Item2 < width - 1);
        topArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.UpMove 
                                 && playerCar.currentCell.Item1 < height - 1);

        leftArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.DownMove
                                 && playerCar.currentCell.Item2 > 0);
        bottomArrowPrefab.SetActive(playerCar.GetMovementState() == MovementState.DownMove
                                 && playerCar.currentCell.Item1 > 0);
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) // left click
        {
            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(ray, out RaycastHit hit))
            {
                GameObject clickedObject = hit.collider.gameObject;

                // Walk up the hierarchy to find the root
                Transform current = clickedObject.transform;
                while (current != null)
                {
                    if (current.gameObject == rightArrowPrefab)
                    {
                        Debug.Log($"Moving car {playerCar.GetOwnerName()} to RIGHT");
                        gameManager.MoveCarWithDirectionArrows(playerCar, MovementDirection.Right);
                        break;
                    }
                    else if (current.gameObject == leftArrowPrefab)
                    {
                        Debug.Log($"Moving car {playerCar.GetOwnerName()} to LEFT");
                        gameManager.MoveCarWithDirectionArrows(playerCar, MovementDirection.Left);
                        break;
                    }
                    else if (current.gameObject == topArrowPrefab)
                    {
                        Debug.Log($"Moving car {playerCar.GetOwnerName()} to UP");
                        gameManager.MoveCarWithDirectionArrows(playerCar, MovementDirection.Up);
                        break;
                    }
                    else if (current.gameObject == bottomArrowPrefab)
                    {
                        Debug.Log($"Moving car {playerCar.GetOwnerName()} to DOWN");
                        gameManager.MoveCarWithDirectionArrows(playerCar, MovementDirection.Down);
                        break;
                    }

                    current = current.parent;
                }
            }
        }
    }

    
}
