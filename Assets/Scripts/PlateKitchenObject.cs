using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateKitchenObject : KitchenObject
{
    
    public event Action<KitchenObjectSO> OnIngredientAdded;
    private List<KitchenObjectSO> _kitchenObjectSoList = new List<KitchenObjectSO>();
    [SerializeField] private List<KitchenObjectSO> _validKitchenObjectSoList = new List<KitchenObjectSO>();

    public bool TryAddIngredient(KitchenObjectSO kitchenObjectSo)
    {
        if(_kitchenObjectSoList.Contains(kitchenObjectSo) || !_validKitchenObjectSoList.Contains(kitchenObjectSo))
            return false;
        _kitchenObjectSoList.Add(kitchenObjectSo);
        OnIngredientAdded?.Invoke(kitchenObjectSo);
        return true;
    }
    
    public List<KitchenObjectSO> GetKitchenObjectSoList()
    {
        return _kitchenObjectSoList;
    }
}
