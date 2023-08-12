using UnityEngine;

public class TrashCounter : BaseCounter
{
    
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;
        KitchenObject kitchenObject = player.GetKitchenObject();
        kitchenObject.DestroySelf();
    }
}
