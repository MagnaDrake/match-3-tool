using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created

    public TMP_InputField widthInput;
    public TMP_InputField heightInput;
    public RectTransform initMenu;
    public EditorMenu editor;


    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnClickCreateLevel()
    {
        gameObject.SetActive(false);
        initMenu.gameObject.SetActive(true);
    }

    public void OnClickInitConfirm()
    {
        if (widthInput.text.Length <= 0 || heightInput.text.Length <= 0)
            return;
        int width = int.Parse(widthInput.text);
        int height = int.Parse(heightInput.text);
        editor.Init(width, height);
        gameObject.SetActive(false);
        initMenu.gameObject.SetActive(false);
    }

    public void OnClickInitCancel()
    {
        gameObject.SetActive(true);
        initMenu.gameObject.SetActive(false);
    }

    public void OnClickLoadLevel()
    {
    }
}
