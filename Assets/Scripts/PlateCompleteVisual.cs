using System;
using System.Collections.Generic;
using UnityEngine;

public class PlateCompleteVisual : MonoBehaviour
{
    [Serializable]
    public struct KitchenObjectSO_GameObject
    {
        public KitchenObjectSO kitchenObjectSo;
        public GameObject gameObject;
    }
    
    [SerializeField] private PlateKitchenObject _plateKitchenObject;
    [SerializeField] private List<KitchenObjectSO_GameObject> _kitchenObjectSoGameObjectList = new List<KitchenObjectSO_GameObject>();

    private void Start()
    {
        _plateKitchenObject.OnIngredientAdded += OnIngredientAdded;
        
        foreach (KitchenObjectSO_GameObject kitchenObjectSoGameObject in _kitchenObjectSoGameObjectList)
        {
            kitchenObjectSoGameObject.gameObject.SetActive(false);
        }
    }

    private void OnDestroy()
    {
        _plateKitchenObject.OnIngredientAdded -= OnIngredientAdded;
    }

    private void OnIngredientAdded(KitchenObjectSO kitchenObjectSo)
    {
        GameObject kitchenObject = _kitchenObjectSoGameObjectList.Find(x => x.kitchenObjectSo == kitchenObjectSo).gameObject;
        kitchenObject.SetActive(true);
    }
}
