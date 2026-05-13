using System.Collections.Generic;
using UnityEngine;

public class LevelEditorManager : MonoBehaviour
{
    public static LevelEditorManager Instance { get; private set; }

    public List<BoardObjectData> boardObjectData = new List<BoardObjectData>();

    public List<TileData> tileData = new List<TileData>();

    public BoardObject boPrefab;

    public EditorMenu levelEditor;

    Queue<BoardObject> boPool = new Queue<BoardObject>();

    private void Awake()
    {
        // 2. Ensure only one instance exists
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); // Delete duplicates
        }
        else
        {
            Instance = this;
            // 3. (Optional) Persist across scenes
            DontDestroyOnLoad(gameObject);
        }
    }

    public BoardObject GetBoardObject()
    {
        if (boPool.Count <= 0)
        {
            return Instantiate(boPrefab);
        }
        BoardObject bo = boPool.Dequeue();
        bo.gameObject.SetActive(true);
        return bo;
    }

    public void RecycleBoardObject(BoardObject bod)
    {
        boPool.Enqueue(bod);
        bod.transform.SetParent(transform);
        bod.Reset();
    }

    public TileData GetTileData(string name)
    {
        foreach (TileData td in tileData)
        {
            if (td.tileName.Equals(name))
                return td;
        }

        return null;
    }

    public TileData GetTileData(TilePropertyID id)
    {
        foreach (TileData td in tileData)
        {
            if ((TilePropertyID)td.ID == id)
                return td;
        }

        return null;
    }

    public BoardObjectData GetBoardObjectData(string name)
    {
        foreach (BoardObjectData bo in boardObjectData)
        {
            if (bo.objectName.Equals(name))
                return bo;
        }

        return null;
    }

    public BoardObjectData GetBoardObjectData(ObjectID id)
    {
        foreach (BoardObjectData bo in boardObjectData)
        {
            if ((ObjectID)bo.ID == id)
                return bo;
        }
        return null;
    }

    public bool IsTileProperty(string name)
    {
        bool res = false;
        foreach (TileData t in tileData)
        {
            if (name.Equals(t.tileName))
                res = true;
        }

        return res;
    }

    public bool IsTileObject(string name)
    {
        bool res = false;
        foreach (BoardObjectData b in boardObjectData)
        {
            if (name.Equals(b.objectName))
                res = true;
        }

        return res;
    }

    public void OnTileClick(Tile t)
    {
        levelEditor.OnTileClick(t);
    }
}

