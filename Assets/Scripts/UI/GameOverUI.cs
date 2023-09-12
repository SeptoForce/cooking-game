using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOverUI : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI recipesDeliveredText;
    [SerializeField] private TextMeshProUGUI labelRecipesDeliveredText;
    [SerializeField] private Button restartButton;
    
    private void Start()
    {
        KitchenGameManager.Instance.OnStateChange += OnStateChange;
        
        restartButton.onClick.AddListener(OnRestartButtonClicked);
        
        Hide();
    }

    private void OnRestartButtonClicked()
    {
        Loader.Load(Loader.Scene.GameScene);
    }

    private void OnDestroy()
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
