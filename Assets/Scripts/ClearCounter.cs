using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObject;
    [SerializeField] private GameObject counterTopPoint;
    
    [SerializeField] private ClearCounter testingClearCounter;
    
    private KitchenObject _kitchenObject;
    
    public void Interact()
    {
        if (_kitchenObject == null)
        {
            GameObject kitchenObjectTransform = Instantiate(kitchenObject.prefab, counterTopPoint.transform);
            kitchenObjectTransform.GetComponent<KitchenObject>().SetClearCounter(this);
        }
        else
        {
            _kitchenObject.SetClearCounter(testingClearCounter);
        }
    }
    
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
