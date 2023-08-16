using System;
using System.Collections.Generic;
using UnityEngine;

public class DeliveryManager : MonoBehaviour
{
    public event Action OnRecipeSpawned;
    public event Action OnRecipeDelivered;
    public event Action OnRecipeSuccess;
    public event Action OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSo;
    
    private List<RecipeSO> waitingRecipeSoList = new List<RecipeSO>();
    private float spawnRecipeTimer = 0f;
    private float spawnRecipeTimerMax = 5f;
    private int waitingRecipesMax = 5;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        spawnRecipeTimer += Time.deltaTime;
        if (spawnRecipeTimer >= spawnRecipeTimerMax)
        {
            spawnRecipeTimer -= spawnRecipeTimerMax;
            
            if (waitingRecipeSoList.Count < waitingRecipesMax)
            {
                RecipeSO waitingRecipeSo = recipeListSo.recipeSOList[UnityEngine.Random.Range(0, recipeListSo.recipeSOList.Count)];
                waitingRecipeSoList.Add(waitingRecipeSo);
                OnRecipeSpawned?.Invoke();
            }
        }
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
       List<KitchenObjectSO> kitchenObjectsOnPlate = plateKitchenObject.GetKitchenObjectSoList();

       foreach (RecipeSO recipeSo in waitingRecipeSoList)
       {
           for(int i = recipeSo.kitchenObjectSOList.Count-1; i >= 0; i--)
           {
               if(kitchenObjectsOnPlate.Contains(recipeSo.kitchenObjectSOList[i]))
                   kitchenObjectsOnPlate.Remove(recipeSo.kitchenObjectSOList[i]);
           }

           if (kitchenObjectsOnPlate.Count == 0)
           {
               waitingRecipeSoList.Remove(recipeSo);
               OnRecipeDelivered?.Invoke();
               OnRecipeSuccess?.Invoke();
               return;
           }
       }
       Debug.Log("Recipe not found");
       OnRecipeFailed?.Invoke();
    }
    
    public List<RecipeSO> GetWaitingRecipeSoList()
    {
        return waitingRecipeSoList;
    }
}
