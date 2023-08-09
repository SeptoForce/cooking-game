using UnityEngine;

public class KitchenObject : MonoBehaviour
{
    [SerializeField] private so_KitchenObject kitchenObjectSO;
    
    public so_KitchenObject GetKitchenObjectSO()
    {
        return kitchenObjectSO;
    }
}
