using System;
using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public abstract void Interact(Player player);

    public virtual void InteractAlternate(Player player) { }
    
    public static event Action<BaseCounter> OnAnyObjectPlacedHere;
    
    [SerializeField] private GameObject counterTopPoint;
    
    private KitchenObject _kitchenObject;
    
    public Transform GetKitchenObjectFollowTransform()
    {
        return counterTopPoint.transform;
    }
    
    public KitchenObject GetKitchenObject()
    {
        return _kitchenObject;
    }
    
    public void SetKitchenObject(KitchenObject kitchenObject)
    {
        _kitchenObject = kitchenObject;
        if(_kitchenObject != null)
            OnAnyObjectPlacedHere?.Invoke(this);
    }
    
    public void ClearKitchenObject()
    {
        _kitchenObject = null;
    }
    
    public bool HasKitchenObject()
    {
        return _kitchenObject != null;
    }
}
