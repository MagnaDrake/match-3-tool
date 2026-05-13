using UnityEngine;

public class SavePanel : MonoBehaviour
{
    public FileIO fileIO;
    void Start()
    {
        gameObject.SetActive(false);
    }

    public void OnClickExportLevel()
    {
        gameObject.SetActive(true);
    }

    public void OnClickExportJson()
    {
        fileIO.Save();
    }

    public void OnClickCancel()
    {
        gameObject.SetActive(false);
    }
}
