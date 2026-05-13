using UnityEngine;

[CreateAssetMenu(fileName = "TileData", menuName = "Scriptable Objects/TileData")]
public class TileData : ScriptableObject
{
    public int ID;
    public string tileName;
    public Sprite tileImage;
    public bool spawn = false;
    public bool valid = false;
}
