using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI labelRecipesDeliveredText;
    
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += OnStateChange;
        Hide();
    }
    
    private void OnDisable()
    {
        KitchenGameManager.Instance.OnStateChange -= OnStateChange;
    }

    private void OnStateChange()
    {
        if (KitchenGameManager.Instance.IsGameOver())
        {
            recipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDelivered().ToString();
            labelRecipesDeliveredText.text = DeliveryManager.Instance.GetRecipesDelivered() == 1 ? "Recipe Delivered" : "Recipes Delivered";
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Hide()
    {
        gameObject.SetActive(false);
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
}
