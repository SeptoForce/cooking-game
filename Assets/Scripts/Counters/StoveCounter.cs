using System;
using System.Collections.Generic;
using UnityEngine;

public class StoveCounter : BaseCounter,IHasProgress
{
    public enum State
    {
        Idle,
        Frying,
        Fried,
        Burned
    }

    [SerializeField] private List<FryingRecipeSO> fryingRecipes;
    [SerializeField] private List<BurningRecipeSO> burningRecipes;
    
    public event Action<State> OnStateChanged;
    public event Action<float> OnProgressChanged;
    
    private float _fryingTimer;
    private float _burningTimer;
    private FryingRecipeSO _fryingRecipeSoWithInput;
    private BurningRecipeSO _burningRecipeSoWithInput;
    private State _state = State.Idle;

    private void Update()
    {
        if (!HasKitchenObject())
        {
            return;
        }

        // state machine
        switch (_state)
        {
            case State.Idle:
                break;
            case State.Frying:
                _fryingTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(_fryingTimer / _fryingRecipeSoWithInput.fryingTimerMax);
                if (_fryingTimer >= _fryingRecipeSoWithInput.fryingTimerMax)
                {
                    _state = State.Fried;
                    OnStateChanged?.Invoke(_state);
                    _fryingTimer = 0;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_fryingRecipeSoWithInput.output, this);
                    _burningTimer = 0;
                    _burningRecipeSoWithInput = GetBurningRecipeSOWithInput(_fryingRecipeSoWithInput.output);
                }
                break;
            case State.Fried:
                _burningTimer += Time.deltaTime;
                OnProgressChanged?.Invoke(_burningTimer / _burningRecipeSoWithInput.burningTimerMax);
                if (_burningTimer >= _burningRecipeSoWithInput.burningTimerMax)
                {
                    _state = State.Burned;
                    OnStateChanged?.Invoke(_state);
                    _burningTimer = 0;
                    GetKitchenObject().DestroySelf();
                    KitchenObject.SpawnKitchenObject(_burningRecipeSoWithInput.output, this);
                }
                break;
            case State.Burned:
                break;
        }
    }

    public override void Interact(Player player)
    {
        // !counter && player <- put object on counter
        if (!HasKitchenObject() && player.HasKitchenObject())
        {
            if (!HasRecipeWithInput(player.GetKitchenObject().GetKitchenObjectSO()))
            {
                return;
            }

            KitchenObject kitchenObject = player.GetKitchenObject();
            kitchenObject.SetKitchenObjectParent(this);

            _fryingRecipeSoWithInput = GetFryingRecipeSOWithInput(GetKitchenObject().GetKitchenObjectSO());

            _fryingTimer = 0;
            _state = State.Frying;
            OnStateChanged?.Invoke(_state);
        }
        // !counter && !player <- do nothing
        else if (!HasKitchenObject() && !player.HasKitchenObject())
        {

        }
        // counter && !player <- take object from counter
        else if (HasKitchenObject() && !player.HasKitchenObject())
        {
            _state = State.Idle;
            _fryingTimer = 0;
            _burningTimer = 0;
            OnProgressChanged?.Invoke(0);
            OnStateChanged?.Invoke(_state);
            GetKitchenObject().SetKitchenObjectParent(player);
        }
        // counter && player <- if player has plate, add object from counter to player's plate
        else if (HasKitchenObject() && player.HasKitchenObject())
        {
            if (player.GetKitchenObject().TryGetPlate(out PlateKitchenObject plateKitchenObject))
            {
                if (plateKitchenObject.TryAddIngredient(GetKitchenObject().GetKitchenObjectSO()))
                {
                    GetKitchenObject().DestroySelf();
                    
                    _state = State.Idle;
                    _fryingTimer = 0;
                    _burningTimer = 0;
                    OnProgressChanged?.Invoke(0);
                    OnStateChanged?.Invoke(_state);
                }
            }
        }
    }

    private bool HasRecipeWithInput(KitchenObjectSO input)
    {
        return fryingRecipes.Exists(x => x.input == input);
    }

    private KitchenObjectSO GetOutputForInput(KitchenObjectSO input)
    {
        return fryingRecipes.Find(x => x.input == input)?.output;
    }

    private FryingRecipeSO GetFryingRecipeSOWithInput(KitchenObjectSO input)
    {
        return fryingRecipes.Find(x => x.input == input);
    }

    private BurningRecipeSO GetBurningRecipeSOWithInput(KitchenObjectSO input)
    {
        return burningRecipes.Find(x => x.input == input);
    }
}