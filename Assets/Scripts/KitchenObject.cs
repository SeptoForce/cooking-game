using UnityEngine;
using UnityEngine.Serialization;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private KitchenObjectSO kitchenObjectSo;
    
    private ClearCounter _clearCounter;
    
    public KitchenObjectSO GetKitchenObjectSO()
    {
        return kitchenObjectSo;
    }
    
    public void SetClearCounter(ClearCounter clearCounter)
    {
        if(_clearCounter != null)
        {
            _clearCounter.ClearKitchenObject();
        }
    
        _clearCounter = clearCounter;

        if (_clearCounter.HasKitchenObject())
        {
            Debug.LogError("ClearCounter already has a kitchen object!");
        }
        else
        {
            _clearCounter.SetKitchenObject(this);
        
            transform.parent = clearCounter.GetKitchenObjectFollowTransform();
            transform.localPosition = Vector3.zero;
        }
    }
    
    public ClearCounter GetClearCounter()
    {
        return _clearCounter;
    }
}
