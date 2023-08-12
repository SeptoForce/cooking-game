using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ContainerCounter : BaseCounter
{
    public event EventHandler OnPlayerGrabbedObject;

    [SerializeField] private KitchenObjectSO kitchenObject;
    
    public override void Interact(Player player)
    {
        // !counter && player <- put object on counter
        if (!HasKitchenObject() && player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.SetKitchenObjectParent(this);
        }
        // !counter && !player <- create object on counter
        else if (!HasKitchenObject() && !player.HasKitchenObject())
        {
            GameObject kitchenObjectTransform = Instantiate(kitchenObject.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        // counter && !player <- take object from counter
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
        // counter && player <- 
        else if (HasKitchenObject() && player.HasKitchenObject())
        {
        }
    }
}
