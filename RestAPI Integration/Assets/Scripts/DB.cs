using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DB : MonoBehaviour
{
    public static DB instance;
    [SerializeField] List<Texture> textures;

    private void Awake()
    {
        instance = this;
    }

    public Texture RandomTexture()
    {
        return textures[Random.Range(0, textures.Count)];
    }
}
