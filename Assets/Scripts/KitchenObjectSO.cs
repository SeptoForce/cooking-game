using UnityEngine;

[CreateAssetMenu(fileName = "New Kitchen Object", menuName = "Kitchen Object")]
public class KitchenObjectSO : ScriptableObject
{
    public GameObject prefab;
    public Sprite sprite;
    public string objectName;
}
