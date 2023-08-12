using UnityEngine;

public class ClearCounter : BaseCounter
{
    
    public override void Interact(Player player)
    {
        // !counter && player <- put object on counter
        if (!HasKitchenObject() && player.HasKitchenObject())
        {
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
}
