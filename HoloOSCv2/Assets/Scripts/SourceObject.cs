﻿using UnityEngine;
using TMPro;
using Microsoft.MixedReality.Toolkit.UI;

public class SourceObject : MonoBehaviour
{
    int id = 0;
    const string azimuth = "/MultiEncoder/azimuth";
    const string elevation = "/MultiEncoder/elevation";
    ToolTip toolTip;

    Transform trans;
    GameObject handler;
    OSCOutput output;

    // Start is called before the first frame update
    void Start()
    {
        trans = GetComponent<Transform>().transform;
        handler = GameObject.FindGameObjectWithTag("OSCHandler");
        output = handler.GetComponent<OSCOutput>();

        AddToolTip();
}
    public float  GetElevation() {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        float angle = eulerAngles.x;
        angle = angle > 180 ? angle - 360 : angle;
        return angle *= -1;
    }
    public float GetAzimuth() {
        Vector3 eulerAngles = transform.rotation.eulerAngles;
        float angle = eulerAngles.y;
        angle = angle > 180 ? angle - 360 : angle;
        return angle *= -1;
    }
    public void sendMessageToOSCHandler() {
        string[] data = new string[2];

        data[0] = azimuth + GetID().ToString();
        data[1] = GetAzimuth().ToString();
        output.SendMessage("SendOSCMessageToClient", data);

        data[0] = elevation + GetID().ToString();
        data[1] = GetElevation().ToString();
        output.SendMessage("SendOSCMessageToClient", data);
    }
    public int GetID() {
        return id;
    }
    public void SetID(int id) {
        this.id = id;
    }
    
    public void AddToolTip() {
        int channel = id + 1;
        toolTip = this.transform.GetChild(0).gameObject.GetComponent<ToolTip>();
        toolTip.ToolTipText = channel.ToString();
        toolTip.transform.localScale = new Vector3(6.0f, 6.0f, 0.1f);
    }
}