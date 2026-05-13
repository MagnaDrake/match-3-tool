using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;

public class FileIO : MonoBehaviour
{
    public TextMeshProUGUI saveLocation;
    public void Save()
    {
        SaveData save = TryCreateSave();
        string json = JsonUtility.ToJson(save, true);

        string path = Application.persistentDataPath + "/" + save.levelName + ".json";

        File.WriteAllText(path, json);
        saveLocation.text = "Game saved to: " + path;
        Debug.Log(saveLocation.text);
    }

    public SaveData TryCreateSave()
    {
        SaveData save = new SaveData();
        EditorMenu editor = LevelEditorManager.Instance.levelEditor;

        save.levelName = editor.GetLevelName();
        save.moveLimit = editor.GetMoveLimit();
        AssignTiles(save, editor);
        AssignGoals(save, editor);
        AssignBasicPieces(save, editor);

        return save;
    }

    void AssignBasicPieces(SaveData save, EditorMenu editor)
    {
        List<BasicPieceStruct> basicPieces = new List<BasicPieceStruct>();
        List<CollectionItem> items = editor.GetBasicPieces();
        if (items != null && items.Count > 0)
        {
            foreach (CollectionItem item in items)
            {
                BasicPieceStruct piece = new BasicPieceStruct();
                piece.objectName = item.bod.objectName;
                piece.id = item.bod.ID;
                basicPieces.Add(piece);
            }
        }

        save.pieces = basicPieces.ToArray();
    }

    void AssignGoals(SaveData save, EditorMenu editor)
    {
        save.goals.scoreGoal = editor.GetScoreGoal();
        List<CollectionGoalStruct> collectionGoals = new List<CollectionGoalStruct>();
        List<CollectionItem> items = editor.GetCollectionGoals();
        if (items != null && items.Count > 0)
        {
            foreach (CollectionItem item in items)
            {
                CollectionGoalStruct goal = new CollectionGoalStruct();
                goal.objectName = item.bod.objectName;
                goal.target = item.amount;
                goal.id = item.bod.ID;
                collectionGoals.Add(goal);
            }
        }

        save.goals.collectionGoals = collectionGoals.ToArray();

    }

    void AssignTiles(SaveData save, EditorMenu editor)
    {
        List<TileInfoData> tiles = new List<TileInfoData>();
        List<Tile> items = editor.GetTiles();
        if (items != null && items.Count > 0)
        {
            foreach (Tile item in items)
            {
                TileInfoData ts = new TileInfoData();
                ts.ID = item.ID;
                ts.valid = item.valid;
                ts.posX = item.posX;
                ts.posY = item.posY;
                ts.spawn = item.spawn;

                if (item.bo != null)
                {
                    ts.item = new BoardObjectStruct();
                    ts.item.moveable = item.bo.moveable;
                    ts.item.ID = item.bo.ID;
                    ts.item.objectName = item.bo.objectName;
                    ts.item.frozen = item.bo.frozen;
                }
                else
                {
                    ts.item = null;
                }
                tiles.Add(ts);
            }
        }

        save.tiles = tiles.ToArray();
    }

    public void CopyToClipboard()
    {

    }
}

[System.Serializable]
public class SaveData
{
    public string levelName;
    public int moveLimit;
    public LevelGoals goals = new LevelGoals();
    public TileInfoData[] tiles;
    public BasicPieceStruct[] pieces;
}

[System.Serializable]
public class LevelGoals
{
    public int scoreGoal;
    public CollectionGoalStruct[] collectionGoals;

}

[System.Serializable]
public struct CollectionGoalStruct
{
    public string objectName;
    public int id;
    public int target;
}

[System.Serializable]
public class TileInfoData
{
    public int ID;
    public int posX;
    public int posY;
    public bool valid;
    public bool spawn;
    public BoardObjectStruct item;
}

[System.Serializable]
public class BoardObjectStruct
{
    public int ID;
    public string objectName;
    public bool moveable;
    public bool frozen;
}


[System.Serializable]
public struct BasicPieceStruct
{
    public string objectName;
    public int id;
}
