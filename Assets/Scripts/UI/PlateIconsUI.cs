using System;
using UnityEngine;
using UnityEngine.Serialization;

public class PlateIconsUI : MonoBehaviour
{
    [SerializeField] private PlateKitchenObject plateKitchenObject;
    [SerializeField] private Transform iconTemplate;

    private void Awake()
    {
        iconTemplate.gameObject.SetActive(false);
        UpdateVisual();
    }
    private void Start()
    {
        plateKitchenObject.OnIngredientAdded += OnIngredientAdded;
    }
    
    private void OnDestroy()
    {
        plateKitchenObject.OnIngredientAdded -= OnIngredientAdded;
    }

    private void OnIngredientAdded(KitchenObjectSO obj)
    {
        UpdateVisual();
    }
    
    private void UpdateVisual()
    {
        foreach (Transform child in transform)
        {
            if (child != iconTemplate)
            {
                Destroy(child.gameObject);
            }
        }
        
        foreach (KitchenObjectSO kitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
        {
            Transform iconTransform = Instantiate(iconTemplate, transform);
            iconTransform.gameObject.SetActive(true);
            iconTransform.GetComponent<PlateIconSingleUI>().SetKitchenObjectSO(kitchenObjectSo);
        }   
    }
}
