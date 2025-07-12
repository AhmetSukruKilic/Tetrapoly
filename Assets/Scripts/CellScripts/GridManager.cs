using Unity.VisualScripting;
using UnityEngine;


public class GridManager : MonoBehaviour
{
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

        district.Init(info, position);

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

    internal DistrictCell ReturnRandomJailCell()
    {
        if (jailCells[0] == (0, 0) && jailCells[1] == (0, 0))
        {
            Debug.LogWarning("No jail cells assigned.");
            return null;
        }
        // Randomly select one of the two jail cells
        int randomIndex = Random.Range(0, 2);
        (int z, int x) = jailCells[randomIndex];
        return districtCells[z][x];
    }
    
    internal void MoveCarToRandomJailCell(Car car)
    {
        DistrictCell jailCell = ReturnRandomJailCell();
        if (jailCell != null)
        {
            jailCell.AddCar(car);
            car.transform.position = jailCell.transform.position + car.initialPosition;
        }
        else
        {
            Debug.LogWarning("No jail cell available to move the car to.");
        }
    }
}
