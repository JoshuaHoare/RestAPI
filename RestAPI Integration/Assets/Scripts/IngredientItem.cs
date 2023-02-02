using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class IngredientItem : MonoBehaviour
{
    [SerializeField] RawImage displayThumbnail;
    [SerializeField] TextMeshProUGUI displayName;
    [SerializeField] TextMeshProUGUI displayPrice;

    public IngredientItem(Texture thumbnail, string name, string price)
    {
        displayThumbnail.material.mainTexture = thumbnail;
        displayName.text = name;
        displayPrice.text = price;
    }

    public void Init(Texture thumbnail, string name, string price)
    {
        displayThumbnail.texture = thumbnail;
        displayName.text = name;
        displayPrice.text = price;
    }
}
