using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GPTEntry : MonoBehaviour
{
    public TextMeshProUGUI data;

    public void SetResponse(string data)
    {
        this.data.text = data.StartsWith(" ") ? data.Substring(1) : data;
    }
}
