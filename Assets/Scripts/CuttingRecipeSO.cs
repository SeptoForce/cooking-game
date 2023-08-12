using UnityEngine;

[CreateAssetMenu(fileName = "CuttingRecipe", menuName = "ScriptableObjects/CuttingRecipe", order = 1)]
public class CuttingRecipeSO : ScriptableObject
{
    public KitchenObjectSO input;
    public KitchenObjectSO output;
    public int cuttingProgressMax;
}
