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
        // counter empty and player not empty
        if (!HasKitchenObject() && player.HasKitchenObject())
        {
            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.SetKitchenObjectParent(this);
        }
        // counter empty and player empty
        else if (!HasKitchenObject() && !player.HasKitchenObject())
        {
            GameObject kitchenObjectTransform = Instantiate(kitchenObject.prefab);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetKitchenObjectParent(player);
            OnPlayerGrabbedObject?.Invoke(this, EventArgs.Empty);
        }
        //counter not empty
        else
        {
            GetKitchenObject().SetKitchenObjectParent(player);
        }
    }
}
