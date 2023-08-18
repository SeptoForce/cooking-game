using System;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter, IHasProgress
{
    [SerializeField] private List<CuttingRecipeSO> cuttingRecipes;
    
    public event Action<float> OnProgressChanged;
    
    public event Action OnPlayerCutObject;
    public static event Action<CuttingCounter> OnAnyCut;

    private int _cuttingProgress;
    

    public override void Interact(Player player)
    {
        // !counter && player <- put object on counter
        if (!HasKitchenObject() && player.HasKitchenObject())
        {
            if (!HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                return;
            }
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.SetKitchenObjectParent(this);
            
            _cuttingProgress = 0;
            CuttingRecipeSO cuttingRecipeSoWithInput = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(_cuttingProgress/(float)cuttingRecipeSoWithInput.cuttingProgressMax);
        }
        // !counter && !player <- do nothing
        else if (!HasKitchenObject() && !player.HasKitchenObject())
        {
            
        }
        // counter && !player <- take object from counter
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
        // counter && player <- if player has plate, add object from counter to player's plate
        else if (HasKitchenObject() && player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf();
                }
            }
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject() && HasRecipeWithInput(GetKitchenObject().GetKitchenObjectSO()))
        {
            _cuttingProgress++;
            CuttingRecipeSO cuttingRecipeSoWithInput = GetCuttingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());
            OnProgressChanged?.Invoke(_cuttingProgress/(float)cuttingRecipeSoWithInput.cuttingProgressMax);
            
            OnPlayerCutObject?.Invoke(); // for animation
            OnAnyCut?.Invoke(this); // for sound
            
            if (_cuttingProgress < cuttingRecipeSoWithInput.cuttingProgressMax)
            {
                return;
            }
            
            KitchenObjectSO kitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            GetKitchenObject().DestroySelf();
            KitchenObject.SpawnKitchenObject(kitchenObjectSo,this);
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return cuttingRecipes.Exists(x => x.input == input);
    }
    
    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        return cuttingRecipes.Find(x => x.input == input)?.output;
    }
    
    private CuttingRecipeSO GetCuttingRecipeSOWithInput(KitchenObjectSO input)
    {
        return cuttingRecipes.Find(x => x.input == input);
    }

    
}
