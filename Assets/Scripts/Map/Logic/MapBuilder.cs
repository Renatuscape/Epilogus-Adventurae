using UnityEngine;

public class MapBuilder : MonoBehaviour
{
    [Header("Prefab References")]
    public GameObject tilePrefab;

    [Header("Grid Settings")]
    public Vector3 gridOffset = Vector3.zero;

    private GameObject[,] tileObjects;

    public void BuildMap()
    {
        if (MapController.blueprint?.Layout == null)
        {
            Debug.LogError("Blueprint Layout is null!");
            return;
        }

        int width = MapController.blueprint.Layout.GetLength(0);
        int height = MapController.blueprint.Layout.GetLength(1);

        tileObjects = new GameObject[width, height];

        for (int x = 0; x < width; x++)
        {
            for (int y = height - 1; y >= 0; y--)
            {
                Vector3 position = new Vector3(x, height - 1 - y, 0) + gridOffset;

                if (tilePrefab != null)
                {
                    GameObject tileObj = Instantiate(tilePrefab, position, Quaternion.identity, this.transform);
                    tileObj.name = $"Tile_{x}_{y}";
                    tileObjects[x, y] = tileObj;
                    tileObj.GetComponent<TilePrefab>().Initialise(MapController.blueprint.Layout[x, y]);
                }
            }
        }
    }

    // Optional: Method to update a specific tile
    public void UpdateTile(int x, int y)
    {
        if (tileObjects?[x, y] != null)
        {
            Destroy(tileObjects[x, y]);

            Vector3 position = new Vector3(x, y, 0) + gridOffset;

            if (tilePrefab != null)
            {
                tileObjects[x, y] = Instantiate(tilePrefab, position, Quaternion.identity, this.transform);
                tileObjects[x, y].name = $"Tile_{x}_{y}";
            }
        }
    }
}
