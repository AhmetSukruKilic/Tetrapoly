using Unity.VisualScripting;
using UnityEngine;


public class GridManager : MonoBehaviour
{
    [SerializeField]private MovementArrowManager movementArrowManager;

    private Vector3 prefabSize;
    private const int width = 8;
    private const int height = 8;
    private const float offset = 1.1f;
    internal DistrictCell[][] districtCells = new DistrictCell[height][];
    private readonly (int, int)[] jailCells = new (int, int)[2];
    public GameObject districtPrefab;
    public Camera mainCamera;

    void Start()
    {
        prefabSize = GetPrefabBounds();
        SpawnGrid();
    }

    void SpawnGrid()
    {
        CellInfo[] cellInfosList = CellInfo.InitializeCellInfos();
        int index = 0;
        for (int x = 0; x < width; x += 2)
        {
            for (int z = 0; z < height; z += 2)
            {
                for (int dx = 0; dx < 2; dx++)
                {
                    for (int dz = 0; dz < 2; dz++)
                    {
                        if (index >= cellInfosList.Length) continue;

                        int globalX = x + dx;
                        int globalZ = z + dz;

                        if (globalX >= width || globalZ >= height) continue;

                        CellInfo info = cellInfosList[index];

                        CreateDistrictCell(globalX, globalZ, info, prefabSize);

                        index++;
                    }
                }
            }
        }

    }

    private void CreateDistrictCell(int x, int z, CellInfo info, Vector3 prefabSize)
    {
        Vector3 position = new Vector3(
            x * prefabSize.x,
            0,
            z * prefabSize.z
        );

        if (districtCells[z] == null)
        {
            districtCells[z] = new DistrictCell[width];
        }

        GameObject cellObj = Instantiate(districtPrefab, position, Quaternion.identity, transform);
        DistrictCell district = cellObj.GetComponent<DistrictCell>();

        if (info.districtType == DistrictType.Jail)
        {
            AddJailCells(z, x);
        }

        districtCells[z][x] = district;

        district.Init(info, position, (z, x));

    }

    private void AddJailCells(int z, int x)
    {
        if (jailCells[0] == (0, 0))
        {
            jailCells[0] = (z, x);
        }
        else if (jailCells[1] == (0, 0))
        {
            jailCells[1] = (z, x);
        }
        else
        {
            Debug.LogWarning("Jail cells already assigned.");
        }
    }

    Vector3 GetPrefabBounds()
    {
        GameObject temp = Instantiate(districtPrefab);
        Renderer rend = temp.transform.Find("DistrictBlock").GetComponent<Renderer>();
        Vector3 size = rend.bounds.size * offset;
        Destroy(temp);
        return size;
    }

    private (int, int) ReturnRandomJailCellCoordinates()
    {
        if (jailCells[0] == (0, 0) && jailCells[1] == (0, 0))
        {
            Debug.LogWarning("No jail cells assigned.");
            return (0, 0);
        }
        // Randomly select one of the two jail cells
        int randomIndex = Random.Range(0, 2);
        return jailCells[randomIndex];
    }

    internal void MoveCarToRandomJailCell(Car car)
    {
        (int, int) jailCell = ReturnRandomJailCellCoordinates();
        MoveCarToSpesificCell(car, jailCell.Item1, jailCell.Item2);
    }
    
    internal void MoveCarToSpesificCell(Car car, int z, int x)
    {
        if (x < 0 || x >= width || z < 0 || z >= height)
        {
            Debug.LogError("Invalid cell coordinates.");
            return;
        }

        var oldCell = car.currentCell;
        districtCells[oldCell.Item1][oldCell.Item2].RemoveCar(car);
        var newCell = districtCells[z][x];

        newCell.AddCar(car);
        ChangeCarPosition(car, newCell);

        movementArrowManager.SetArrowVisibility();
        
        Debug.Log($"Car {car.GetOwnerName()} moved to cell ({z}, {x})");
    }

    private void ChangeCarPosition(Car car, DistrictCell districtCell)
    {
        if (car == null || districtCell == null) return;

        car.transform.position = districtCell.transform.position + car.initialPosition;
    }
}
