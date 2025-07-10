using UnityEngine;

public class Game : MonoBehaviour
{
    [SerializeField] private GridManager gridManager;
    [SerializeField] private Camera mainCamera;
    [SerializeField] private GameObject districtPrefab;

    private Vector3 prefabSize; 
    private const int width = 8;
    private const int height = 8;
    private const float offset = 1.1f;
    void Start()
    {

    }


    void initializeGrid()
    {

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
