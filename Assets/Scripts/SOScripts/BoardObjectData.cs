using UnityEngine;

[CreateAssetMenu(fileName = "BoardObjectData", menuName = "Scriptable Objects/BoardObjectData")]
public class BoardObjectData : ScriptableObject
{
    public string objectName;
    public int ID;
    public bool moveable;
    public PieceProperty property;
    public Sprite image;
}

