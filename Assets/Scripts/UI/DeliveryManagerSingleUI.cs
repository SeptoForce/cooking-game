using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class DeliveryManagerSingleUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipeNameText;
    [SerializeField] private GameObject iconContainer;
    [SerializeField] private GameObject iconTemplate;

    private void Awake()
    {
        iconTemplate.SetActive(false);
    }

    public void SetRecipeSO(RecipeSO recipeSo)
    {
        recipeNameText.text = recipeSo.recipeName;
        
        foreach( Transform child in iconContainer.transform)
        {
            if(child == iconTemplate.transform) continue;
            Destroy(child.gameObject);
        }

        foreach (KitchenObjectSO kitchenObjectSo in recipeSo.kitchenObjectSOList)
        {
            GameObject iconGameObject = Instantiate(iconTemplate, iconContainer.transform);
            iconGameObject.SetActive(true);
            iconGameObject.GetComponent<Image>().sprite = kitchenObjectSo.sprite;
        }
    }
}
