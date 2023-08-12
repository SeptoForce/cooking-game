using UnityEngine;

[CreateAssetMenu(fileName = "BurningRecipe", menuName = "ScriptableObjects/BurningRecipe", order = 1)]
public class BurningRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public float burningTimerMax;
}
