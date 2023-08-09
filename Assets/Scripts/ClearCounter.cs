using UnityEngine;

public class ClearCounter : MonoBehaviour
{
    [SerializeField] private so_KitchenObject kitchenObject;
    [SerializeField] private GameObject counterTopPoint;
    
    public void Interact()
    {
        GameObject kitchenObjectTransform = Instantiate(kitchenObject.prefab, counterTopPoint.transform);
        kitchenObjectTransform.transform.localPosition = Vector3.zero;

        kitchenObjectTransform.GetComponent<KitchenObject>().GetKitchenObjectSO();
    }
}
