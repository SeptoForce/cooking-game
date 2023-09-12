using System;
using System.Collections.Generic;
using Unity.Netcode;
using UnityEngine;

public class DeliveryManager : NetworkBehaviour
{
    public event Action OnRecipeSpawned;
    public event Action OnRecipeDelivered;
    public event Action OnRecipeSuccess;
    public event Action OnRecipeFailed;
    public static DeliveryManager Instance { get; private set; }
    
    [SerializeField] private RecipeListSO recipeListSo;
    
    private List<RecipeSO> _waitingRecipeSoList = new List<RecipeSO>();
    private float _spawnRecipeTimer = 2f;
    private float _spawnRecipeTimerMax = 5f;
    private int _waitingRecipesMax = 5;
    private int _recipesDelivered = 0;
    
    private void Awake()
    {
        Instance = this;
    }

    private void Update()
    {
        if(!IsServer) return;
        
        _spawnRecipeTimer += Time.deltaTime;
        if (_spawnRecipeTimer >= _spawnRecipeTimerMax)
        {
            _spawnRecipeTimer -= _spawnRecipeTimerMax;
            
            if (_waitingRecipeSoList.Count < _waitingRecipesMax)
            {
                int generatedIndex = UnityEngine.Random.Range(0, recipeListSo.recipeSOList.Count);
                SpawnNewWaitingRecipeClientRpc(generatedIndex);
            }
        }
    }
    
    [ClientRpc]
    private void SpawnNewWaitingRecipeClientRpc(int waitingRecipeSoIndex)
    {
        RecipeSO waitingRecipeSo = recipeListSo.recipeSOList[waitingRecipeSoIndex];
        _waitingRecipeSoList.Add(waitingRecipeSo);
        OnRecipeSpawned?.Invoke();
    }

    public void DeliverRecipe(PlateKitchenObject plateKitchenObject)
    {
        foreach (RecipeSO waitingRecipeSO in _waitingRecipeSoList)
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
                    int waitingRecipeSoIndex = _waitingRecipeSoList.IndexOf(waitingRecipeSO);
                    DeliverCorrectRecipeServerRpc(waitingRecipeSoIndex);
                    return;
                }
            }
        }
        DeliverIncorrectRecipeServerRpc();
    }
    
    [ServerRpc(RequireOwnership = false)]
    private void DeliverIncorrectRecipeServerRpc()
    {
        DeliverIncorrectRecipeClientRpc();
    }
    
    [ClientRpc]
    private void DeliverIncorrectRecipeClientRpc()
    {
        OnRecipeFailed?.Invoke();
    }

    [ServerRpc(RequireOwnership = false)]
    private void DeliverCorrectRecipeServerRpc(int waitingRecipeSoIndex)
    {
        DeliverCorrectRecipeClientRpc(waitingRecipeSoIndex);
    }
    
    [ClientRpc]
    private void DeliverCorrectRecipeClientRpc(int waitingRecipeSoIndex)
    {
        _waitingRecipeSoList.RemoveAt(waitingRecipeSoIndex);
        OnRecipeDelivered?.Invoke();
        OnRecipeSuccess?.Invoke();
        _recipesDelivered++;
    }

    public List<RecipeSO> GetWaitingRecipeSoList()
    {
        return _waitingRecipeSoList;
    }
    
    public int GetRecipesDelivered()
    {
        return _recipesDelivered;
    }
}
