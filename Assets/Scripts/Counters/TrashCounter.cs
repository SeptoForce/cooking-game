using System;
using UnityEngine;

public class TrashCounter : BaseCounter
{
    
    public static event Action<TrashCounter> OnAnyObjectTrashed; 
    public override void Interact(Player player)
    {
        if (!player.HasKitchenObject()) return;
        KitchenObject kitchenObject = player.GetKitchenObject();
        kitchenObject.DestroySelf();
        OnAnyObjectTrashed?.Invoke(this);
    }
}
