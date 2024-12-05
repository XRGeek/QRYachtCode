using UnityEngine;
using System;
using TMPro;

public class DeviceCheck : MonoBehaviour
{
    public GameObject IphoneMessage;
    public TextMeshProUGUI UserAgentMessage;

    private void Start()
    {
        myDebugs(SystemInfo.deviceModel);
        myDebugs(SystemInfo.deviceName);
        myDebugs(SystemInfo.deviceType.ToString());
        if(SystemInfo.operatingSystem=="")
        {
            myDebugs("Null");
        }
        myDebugs(SystemInfo.operatingSystem);
        string userAgent = SystemInfo.operatingSystem.ToLower();

        IphoneMessage.SetActive(userAgent.Contains("iphone"));
        UserAgentMessage.gameObject.SetActive(true);


    }

    public void myDebugs(string s)
    {
        UserAgentMessage.text = UserAgentMessage.text + "\n" + s;
    }
    

}
