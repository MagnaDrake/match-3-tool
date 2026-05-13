using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class CollectionGoal : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public TMP_InputField amountInput;
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
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = data.objectName;
            option.image = data.image;
            customOptions.Add(option);
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
        if (amountInput.text.Length <= 0)
            return;
        bool exist = false;
        foreach (Transform child in transform)
        {
            CollectionItem item = child.GetComponent<CollectionItem>();
            if (item != null)
            {
                if (item.bod.objectName.Equals(itemName))
                {
                    exist = true;
                    item.UpdateAmount(int.Parse(amountInput.text));
                }
            }
        }

        if (!exist)
        {
            int target = int.Parse(amountInput.text);
            CollectionItem cItem = Instantiate(colItemPrefab);
            cItem.transform.SetParent(transform);
            cItem.transform.localPosition = Vector3.zero;
            cItem.SetData(LevelEditorManager.Instance.GetBoardObjectData(itemName), target);
        }
    }

    public void ClearGoals()
    {
        foreach (Transform child in transform)
        {
            CollectionItem item = child.GetComponent<CollectionItem>();
            if (item != null)
                Destroy(item.gameObject);
        }
    }

    public List<CollectionItem> GetGoals()
    {
        List<CollectionItem> goals = new List<CollectionItem>();
        foreach (Transform child in transform)
        {
            CollectionItem item = child.GetComponent<CollectionItem>();
            if (item != null)
                goals.Add(item);
        }
        return goals;
    }

    // get json list
}
