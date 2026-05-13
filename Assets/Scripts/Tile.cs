using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Tile : MonoBehaviour, IPointerDownHandler
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public int posX;
    public int posY;
    public int ID;
    public bool valid; // valid true = tile is displayed as a valid tile in the grid
    public bool spawn;
    public BoardObject bo;
    public Image tileImage;

    public void Init(int row, int column)
    {
        posX = column;
        posY = row;
        valid = true;
        spawn = false;

        TileData td = LevelEditorManager.Instance.GetTileData("Active");
        if (td != null)
        {
            ID = td.ID;
            tileImage.sprite = td.tileImage;
        }
    }

    public void OnPointerDown(PointerEventData eventData)
    {
        LevelEditorManager.Instance.OnTileClick(this);
    }

    public void ChangeTileProperty(TileData td)
    {
        if (td != null)
        {
            ID = td.ID;
            tileImage.sprite = td.tileImage;
            valid = td.valid;
            spawn = td.spawn;
        }
    }

    public void SetFrozen()
    {
        if (bo != null)
        {
            bo.SetFrozen(true);
        }
    }

    public void AddObjectToTile(BoardObjectData bod)
    {
        if (!valid)
            return;
        if (bo == null)
        {
            bo = LevelEditorManager.Instance.GetBoardObject();
            bo.transform.SetParent(transform);
            bo.transform.localPosition = Vector3.zero;
        }
        bo.SetData(bod);
    }

    public void RemoveObjectFromTile()
    {
        if (bo != null)
        {
            LevelEditorManager.Instance.RecycleBoardObject(bo);
            bo = null;
        }
    }
}
