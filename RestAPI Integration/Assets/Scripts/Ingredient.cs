using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ingredient : MonoBehaviour
{
    public int id;
    public string itemName;
    public string description;
    public string cost;
    public Texture thumnail;

    public Ingredient(int id, string itemName, string description, string cost, string thumbnailURL)
    {
        this.id = id;
        this.itemName = name;
        this.description = description;
        this.cost = cost;
        this.thumnail = DB.instance.RandomTexture();
    }


}
