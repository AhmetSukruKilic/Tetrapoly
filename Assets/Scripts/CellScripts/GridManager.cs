using UnityEngine;


public class GridManager : MonoBehaviour
{   
    private Vector3 prefabSize; 
    private const int width = 8;
    private const int height = 8;
    private const float offset = 1.1f; 
    internal DistrictCell[][] districtCells = new DistrictCell[height][];
    public GameObject districtPrefab;
    public Camera mainCamera;

    void Start()
    {
        prefabSize = GetPrefabBounds();
        SpawnGrid();
        AdjustCamera();
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

        district.Init(info, position);

        districtCells[z][x] = district;
    }


    private void AdjustCamera()
    {
        float cameraHeight = Mathf.Max(prefabSize.x, prefabSize.z) * 2.7f * (width + height) / 8f;
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

}
