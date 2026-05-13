using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BasicPieceContainer : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Image pieceImage;

    public Transform collectionSelectionPanel;

    public string itemName;

    public CollectionItem colItemPrefab;

    void Start()
    {
        dropdown.onValueChanged.AddListener(delegate
        {
            OnDropdownValueChange(dropdown);
        });

        SetDropdown();
    }

    void OnDropdownValueChange(TMP_Dropdown change)
    {
        // 'change.value' is the index (0, 1, 2...) of the selected option
        Debug.Log("Selected Index: " + change.value);

        // To get the actual text of the selected option
        string selectedText = change.options[change.value].text;
        Debug.Log("Selected Text: " + selectedText);
        itemName = selectedText;

        pieceImage.sprite = change.options[change.value].image;
    }

    // might combine this with the draw selector dropdown and basic piece selector drop down
    // not enough time
    public void SetDropdown()
    {
        List<TMP_Dropdown.OptionData> customOptions = new List<TMP_Dropdown.OptionData>();

        List<BoardObjectData> bod = LevelEditorManager.Instance.boardObjectData;
        foreach (BoardObjectData data in bod)
        {
            if (data.property == PieceProperty.Basic)
            {
                TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
                option.text = data.objectName;
                option.image = data.image;
                customOptions.Add(option);
            }
        }

        dropdown.AddOptions(customOptions);
        pieceImage.sprite = customOptions[0].image;
        itemName = customOptions[0].text;
    }

    public void OnClickConfirmSelection()
    {
        HandleAddPiece();
        collectionSelectionPanel.gameObject.SetActive(false);
    }

    public void OnClickCancelSelection()
    {
        collectionSelectionPanel.gameObject.SetActive(false);
    }

    public void OnClickAddPiece()
    {
        collectionSelectionPanel.gameObject.SetActive(true);
    }

    public void OnClickRemovePiece()
    {

    }

    void HandleAddPiece()
    {
        foreach (Transform child in transform)
        {
            CollectionItem item = child.GetComponent<CollectionItem>();
            if (item != null)
            {
                if (item.bod.objectName.Equals(itemName))
                {
                    return;
                }
            }
        }


        CollectionItem cItem = Instantiate(colItemPrefab);
        cItem.transform.SetParent(transform);
        cItem.transform.localPosition = Vector3.zero;
        cItem.SetData(LevelEditorManager.Instance.GetBoardObjectData(itemName), 1);
    }

    public void ClearPieces()
    {
        foreach (Transform child in transform)
        {
            CollectionItem item = child.GetComponent<CollectionItem>();
            if (item != null)
                Destroy(item.gameObject);
        }
    }

    public List<CollectionItem> GetPieces()
    {
        List<CollectionItem> pieces = new List<CollectionItem>();
        foreach (Transform child in transform)
        {
            CollectionItem item = child.GetComponent<CollectionItem>();
            if (item != null)
                pieces.Add(item);
        }

        return pieces;
    }

    // get json list
}
