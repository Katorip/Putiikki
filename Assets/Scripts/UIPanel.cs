using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIPanel : MonoBehaviour
{
    private void OnEnable()
    {
        ClickManager.CloseAllPanels += ClosePanel;
    }

    public void OpenPanel()
    {
        ClickManager.CallCloseAllPanels();
        gameObject.SetActive(true);
    }

    public void ClosePanel()
    {
        gameObject.SetActive(false);
    }

    private void OnDisable()
    {
        ClickManager.CloseAllPanels -= ClosePanel;
    }
}
