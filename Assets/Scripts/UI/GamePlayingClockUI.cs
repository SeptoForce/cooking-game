using UnityEngine;
using UnityEngine.UI;

public class GamePlayingClockUI : MonoBehaviour
{
    [SerializeField] private Image clockTimer;
    
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
        if (KitchenGameManager.Instance.IsGamePlaying())
        {
            Show();
        }
        else
        {
            Hide();
        }
    }

    private void Update()
    {
        clockTimer.fillAmount = KitchenGameManager.Instance.GetGamePlayingTimerNormalized();
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
