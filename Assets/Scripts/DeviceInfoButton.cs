// Manages IoT devices info

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DeviceInfoButton : MonoBehaviour
{
    public int itemID;
    public string deviceName;
    public string deviceInfo;
    public Sprite deviceSprite;
    public Text deviceNameText;
    public Text deviceInfoText;
    public Image deviceImage;
    public Scrollbar verticalScroll;
    public SettingsManager settingsManager;

    public void SetNewInfoText()
    {
        deviceNameText.text = deviceName;
        deviceInfoText.text = deviceInfo;
        deviceImage.sprite = deviceSprite;
        verticalScroll.value = 1.0f;
        settingsManager.ShowDeviceInfoWindow(true);
    }
}