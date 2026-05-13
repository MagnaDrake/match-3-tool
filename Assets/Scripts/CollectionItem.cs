using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionItem : MonoBehaviour
{
    public BoardObjectData bod;
    public int amount;
    public Image img;
    public TextMeshProUGUI tmp;



    public void SetData(BoardObjectData b, int a)
    {
        bod = b;
        amount = a;
        img.sprite = b.image;
        tmp.text = amount.ToString();
    }

    public void UpdateAmount(int a)
    {
        amount = a;
        tmp.text = amount.ToString();
        Debug.Log(tmp.text);
    }

    public void RemoveItem()
    {
        Destroy(gameObject);
    }
}
