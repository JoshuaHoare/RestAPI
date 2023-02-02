using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
using UnityEngine.UI;

public class IngredientsManager : MonoBehaviour
{
    public static IngredientsManager instance;
    [SerializeField] ScrollRect scrollRect;
    [SerializeField] bool update;
    [SerializeField] Texture tmp;

    [SerializeField] IngredientItem itemTemplate;

    private void Awake()
    {
        instance = this;

        if (!scrollRect)
            scrollRect = gameObject.GetComponent<ScrollRect>();
    }

    private void Update()
    {
        if (update)
        {
            update = false;
            APICall(Reload);
        }
    }
    
    void Reload(IngredientsAPI ingredients)
    {
        if (true)
        {
            Clear();

            Debug.Log(ingredients);
            foreach (IngredientAPI ingredient in ingredients.ingredients)
            {
                IngredientItem item = Instantiate(itemTemplate, scrollRect.content).GetComponent<IngredientItem>();
                item.Init(DB.instance.RandomTexture(), ingredient.name, ingredient.id.ToString());
            }
        }
    }

    //Clear out all ingredients
    void Clear()
    {
        List<Transform> children = new List<Transform>();

        foreach (Transform child in scrollRect.content)
        {
            if (child.name != "Hidden")
                children.Add(child);
        }

        foreach (Transform child in children)
        {
            DestroyImmediate(child.gameObject);
        }
    }

    #region API

    public void APICall(System.Action<IngredientsAPI> callback)
    {
        StartCoroutine(ProcessRequest(callback));
    }

    private IEnumerator ProcessRequest(System.Action<IngredientsAPI> callback)
    {
        string url = "https://bulkapi.azurewebsites.net/api/ingredients/all";

        using (UnityWebRequest request = UnityWebRequest.Get(url))
        {
            yield return request.SendWebRequest();

            IngredientsAPI ingredients = new IngredientsAPI();

            if (request.isNetworkError || request.isHttpError)
                Debug.LogWarning("Failed");
            else
            {
                Debug.Log(request.downloadHandler.text);
                ingredients = JsonUtility.FromJson<IngredientsAPI>("{\"ingredients\":" + request.downloadHandler.text + "}");
            }

            callback.Invoke(ingredients);

        }
    }

    #endregion


}

[System.Serializable]
public class IngredientsAPI
{
    public IngredientAPI[] ingredients; 
}

[System.Serializable]
public class IngredientAPI
{
    public int id;
    public string name;
    public string source;
}

