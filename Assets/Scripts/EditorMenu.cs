using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EditorMenu : MonoBehaviour
{
    public LevelGrid grid;
    public DrawSelector drawSelector;
    public CollectionGoal collectionGoal;
    public BasicPieceContainer pieceContainer;
    public SavePanel savePanel;

    public TMP_InputField scoreGoal;
    public TMP_InputField moveLimit;
    public TMP_InputField levelName;


    public Transform toolSelections;

    private EditorMode _mode;
    public EditorMode mode
    {
        get { return _mode; }
        set
        {
            _mode = value;
            OnChangeEditorMode(value);
        }
    }

    void Start()
    {
        toolSelections.gameObject.SetActive(false);
        collectionGoal.gameObject.SetActive(false);
        pieceContainer.gameObject.SetActive(false);
        scoreGoal.transform.parent.gameObject.SetActive(false);
        moveLimit.transform.parent.gameObject.SetActive(false);
        savePanel.transform.parent.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void Init(int width, int height)
    {
        gameObject.SetActive(true);
        grid.Init(width, height);
        // todo default all menu values
        toolSelections.gameObject.SetActive(true);
        collectionGoal.gameObject.SetActive(true);
        pieceContainer.gameObject.SetActive(true);
        scoreGoal.transform.parent.gameObject.SetActive(true);
        moveLimit.transform.parent.gameObject.SetActive(true);
        savePanel.transform.parent.gameObject.SetActive(true);
        mode = EditorMode.Draw;
        levelName.text = "level_" + Random.Range(1000, 4000);
    }

    public void OnClickBackToMenu()
    {
        gameObject.SetActive(false);
        grid.Reset();
        collectionGoal.ClearGoals();
        pieceContainer.ClearPieces();
        // lazy reset
        SceneManager.LoadScene("Menu");
    }

    public void ChangeEditorMode(int value)
    {
        mode = (EditorMode)value;
    }

    public void OnChangeEditorMode(EditorMode _mode)
    {
        if (mode == EditorMode.Erase)
        {
            drawSelector.Toggle(false);
        }
        else
        {
            drawSelector.Toggle(true);
        }
    }

    public void OnTileClick(Tile t)
    {
        switch (mode)
        {
            case EditorMode.Erase:
                t.RemoveObjectFromTile();
                t.ChangeTileProperty(LevelEditorManager.Instance.GetTileData(TilePropertyID.Inactive));
                break;
            case EditorMode.Draw:
                HandleDrawTile(t);
                break;
        }
    }

    void HandleDrawTile(Tile t)
    {
        if (LevelEditorManager.Instance.IsTileObject(drawSelector.itemName))
        {
            BoardObjectData bod = LevelEditorManager.Instance.GetBoardObjectData(drawSelector.itemName);
            t.AddObjectToTile(bod);
        }
        else if (LevelEditorManager.Instance.IsTileProperty(drawSelector.itemName))
        {
            TileData td = LevelEditorManager.Instance.GetTileData(drawSelector.itemName);
            if ((TilePropertyID)td.ID == TilePropertyID.Ice)
            {
                t.SetFrozen();
            }
            else
            {
                t.ChangeTileProperty(td);
            }
        }
        else
        {
            Debug.Log("Invalid Draw Item");
        }
    }

    public string GetLevelName()
    {
        return levelName.text;
    }

    public int GetScoreGoal()
    {
        return scoreGoal.text.Length > 0 ? int.Parse(scoreGoal.text) : 0;
    }

    public int GetMoveLimit()
    {
        return moveLimit.text.Length > 0 ? int.Parse(moveLimit.text) : 0;
    }

    public List<CollectionItem> GetCollectionGoals()
    {
        return collectionGoal.GetGoals();
    }

    public List<CollectionItem> GetBasicPieces()
    {
        return pieceContainer.GetPieces();
    }

    public List<Tile> GetTiles()
    {
        return grid.GetTiles();
    }
}

public enum EditorMode
{
    Draw,
    Erase,
}
