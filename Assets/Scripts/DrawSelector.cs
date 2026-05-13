using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DrawSelector : MonoBehaviour
{
    public TMP_Dropdown dropdown;
    public Image drawImage;

    public string itemName;

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

        drawImage.sprite = change.options[change.value].image;
    }

    public void Toggle(bool value)
    {
        gameObject.SetActive(value);
    }

    public void ToggleDropdown(bool value)
    {
        Debug.Log("Toggle Dropdown " + value);
        dropdown.gameObject.SetActive(value);
    }

    public void SetDropdown()
    {
        List<TMP_Dropdown.OptionData> customOptions = new List<TMP_Dropdown.OptionData>();
        List<TileData> td = LevelEditorManager.Instance.tileData;
        foreach (TileData data in td)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = data.tileName;
            option.image = data.tileImage;
            customOptions.Add(option);
        }

        List<BoardObjectData> bod = LevelEditorManager.Instance.boardObjectData;
        foreach (BoardObjectData data in bod)
        {
            TMP_Dropdown.OptionData option = new TMP_Dropdown.OptionData();
            option.text = data.objectName;
            option.image = data.image;
            customOptions.Add(option);
        }

        dropdown.AddOptions(customOptions);
        drawImage.sprite = customOptions[0].image;
        itemName = customOptions[0].text;
    }
}
