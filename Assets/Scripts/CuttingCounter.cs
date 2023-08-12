using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CuttingCounter : BaseCounter
{
    [SerializeField] private List<CuttingRecipeSO> cuttingRecipes;

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
        // counter && player <- do nothing
        else if (HasKitchenObject() && player.HasKitchenObject())
        {
            
        }
    }

    public override void InteractAlternate(Player player)
    {
        if (HasKitchenObject())
        {
            KitchenObjectSO kitchenObjectSo = GetOutputForInput(GetKitchenObject().GetKitchenObjectSO());
            if (kitchenObjectSo == null)
            {
                return;
            }
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
}
