using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DisplayData : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI longitudeDisplay;
    [SerializeField] TextMeshProUGUI latitudeDisplay;

    private void Start()
    {
        Logic.instance.listeners.Add(UpdateData);
    }

    void UpdateData(float _longitude, float _latitude)
    {
        longitudeDisplay.text = "Longitude: " + Mathf.Abs(_longitude).ToString() + "° " + (_longitude < 0 ? "W" : "E");
        latitudeDisplay.text = "Latitude: " + Mathf.Abs(_latitude).ToString() + "° " + (_latitude > 0 ? "N" : "S");
    }
}
