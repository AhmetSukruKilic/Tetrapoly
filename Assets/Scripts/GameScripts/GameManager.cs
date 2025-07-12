using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject districtPrefab;
    [SerializeField] private RollTheDice rollTheDice;
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
        rollTheDice.assignPlayers(cars);
    }

    private void AdjustCamera()
    {
        float cameraHeight = Mathf.Max(prefabSize.x, prefabSize.z) * (width + height) / 8 * 3;
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
