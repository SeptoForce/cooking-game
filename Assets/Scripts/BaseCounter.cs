using UnityEngine;

public abstract class BaseCounter : MonoBehaviour, IKitchenObjectParent
{
    public abstract void Interact(Player player);
    
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
