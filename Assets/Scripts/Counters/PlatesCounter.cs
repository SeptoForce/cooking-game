using System;
using UnityEngine;

public class PlatesCounter : BaseCounter
{
    public event Action OnPlateSpawned;
    public event Action OnPlateRemoved;
    
    [SerializeField] private KitchenObjectSO plateKitchenObjectSO;
    private float _spawnPlateTimer = 0f;
    private float _spawnPlateTimerMax = 4f;
    private int _platesSpawnAmount = 0;
    private int _platesSpawnAmountMax = 4;

    private void Update()
    {
        _spawnPlateTimer += Time.deltaTime;
        if (_spawnPlateTimer >= _spawnPlateTimerMax)
        {
            _spawnPlateTimer = 0f;
            if (_platesSpawnAmount < _platesSpawnAmountMax)
            {
                _platesSpawnAmount++;
                OnPlateSpawned?.Invoke();
            }
        }
    }

    public override void Interact(Player player)
    {
        // !counter && player <- put object on counter
        if (!HasKitchenObject() && player.HasKitchenObject())
        {
            
        }
        // !counter && !player <- generate object on counter and give it to player
        else if (!HasKitchenObject() && !player.HasKitchenObject())
        {
            if(_platesSpawnAmount > 0)
            {
                _platesSpawnAmount--;
                KitchenObject.SpawnKitchenObject(plateKitchenObjectSO, player);
                OnPlateRemoved?.Invoke();
            }
        }
        // counter && !player <- do nothing
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            
        }
        // counter && player <- do nothing
        else if (HasKitchenObject() && player.HasKitchenObject())
        {
            
        }
    }
}
