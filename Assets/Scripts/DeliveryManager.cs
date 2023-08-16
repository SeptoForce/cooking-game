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
        foreach (RecipeSO waitingRecipeSO in waitingRecipeSoList)
        {
            if(plateKitchenObject.GetKitchenObjectSoList().Count == waitingRecipeSO.kitchenObjectSOList.Count)
            {
                bool plateContentsMatchRecipe = true;
                foreach (KitchenObjectSO recipeKitchenObjectSo in waitingRecipeSO.kitchenObjectSOList)
                {
                    bool ingredientFound = false;
                    foreach (KitchenObjectSO plateKitchenObjectSo in plateKitchenObject.GetKitchenObjectSoList())
                    {
                        if (plateKitchenObjectSo == recipeKitchenObjectSo)
                        {
                            ingredientFound = true;
                            break;
                        }
                    }
                    if(!ingredientFound)
                    {
                        plateContentsMatchRecipe = false;
                    }
                }
                if(plateContentsMatchRecipe)
                {
                    waitingRecipeSoList.Remove(waitingRecipeSO);
                    OnRecipeDelivered?.Invoke();
                    OnRecipeSuccess?.Invoke();
                    return;
                }
            }
        }
        OnRecipeFailed?.Invoke();
    }
    
    public List<RecipeSO> GetWaitingRecipeSoList()
    {
        return waitingRecipeSoList;
    }
}
