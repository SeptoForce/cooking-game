using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManagerUI : MonoBehaviour
{
    [SerializeField] private GameObject container;
    [SerializeField] private GameObject recipeTemplate;

    private void Awake()
    {
        recipeTemplate.SetActive(false);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSpawned += InstanceOnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered += InstanceOnRecipeDelivered;
        UpdateVisual();
    }
    
    private void OnDestroy()
    {
        DeliveryManager.Instance.OnRecipeSpawned -= InstanceOnRecipeSpawned;
        DeliveryManager.Instance.OnRecipeDelivered -= InstanceOnRecipeDelivered;
    }

    private void InstanceOnRecipeDelivered()
    {
        UpdateVisual();
    }

    private void InstanceOnRecipeSpawned()
    {
        UpdateVisual();
    }

    private void UpdateVisual()
    {
        foreach (Transform child in container.transform)
        {
            if(child == recipeTemplate.transform) continue;
            Destroy(child.gameObject);
        }

        foreach (RecipeSO recipeSo in DeliveryManager.Instance.GetWaitingRecipeSoList()) 
        {
            GameObject recipeGameObject = Instantiate(recipeTemplate, container.transform);
            recipeGameObject.SetActive(true);
            recipeGameObject.GetComponent<DeliveryManagerSingleUI>().SetRecipeSO(recipeSo);
        }
    }
}
