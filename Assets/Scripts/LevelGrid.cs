using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LevelGrid : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    public GameObject tilePrefab;
    public GameObject rowPrefab;
    void Start()
    {
        Reset();
    }

    public void Reset()
    {
        foreach (Transform t in transform)
        {
            Destroy(t.gameObject);
        }
    }

    public void Init(int width, int height)
    {
        for (int i = 0; i < height; i++)
        {
            Transform row = Instantiate(rowPrefab).transform;
            row.SetParent(transform);
            for (int j = 0; j < width; j++)
            {
                Transform tile = Instantiate(tilePrefab).transform;
                tile.SetParent(row);
                tile.GetComponent<Tile>().Init(i, j);
            }
        }
        //hack update layout
        GetComponent<VerticalLayoutGroup>().enabled = false;
        GetComponent<VerticalLayoutGroup>().enabled = true;

    }

    public List<Tile> GetTiles()
    {
        List<Tile> tiles = new List<Tile>();
        foreach (Transform row in transform)
        {
            foreach (Transform col in row)
            {
                tiles.Add(col.GetComponent<Tile>());
            }
        }

        return tiles;
    }
}
