using UnityEngine;
using UnityEngine.UI;

public class BoardObject : MonoBehaviour
{
    public Image objectImage;
    public Image frozenImage;

    public int ID;
    public string objectName;
    public bool moveable;
    public bool frozen;

    public void Reset()
    {
        transform.localPosition = Vector3.zero;
        gameObject.SetActive(false);
    }

    public void SetData(BoardObjectData data)
    {
        ID = data.ID;
        moveable = data.moveable;
        objectName = data.objectName;
        SetFrozen(false);
        objectImage.sprite = data.image;
    }

    public void SetFrozen(bool value)
    {
        frozen = value;
        frozenImage.gameObject.SetActive(value);
    }
}
