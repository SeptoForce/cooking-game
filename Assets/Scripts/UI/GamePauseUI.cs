using System;
using UnityEngine;
using UnityEngine.UI;

public class GamePauseUI : MonoBehaviour
{
    [SerializeField] private Button resumeButton;
    [SerializeField] private Button mainMenuButton;

    private void Awake()
    {
        resumeButton.onClick.AddListener(() => { KitchenGameManager.Instance.TogglePauseGame(); }
        );
        mainMenuButton.onClick.AddListener(() => { Loader.Load(Loader.Scene.MainMenu); }
        );
    }

    private void Start()
    {
        KitchenGameManager.Instance.OnGamePaused += Show;
        KitchenGameManager.Instance.OnGameUnpaused += Hide;
        
        Hide();
    }
    
    private void OnDisable()
    {
        KitchenGameManager.Instance.OnGamePaused -= Show;
        KitchenGameManager.Instance.OnGameUnpaused -= Hide;
    }

    private void Show()
    {
        gameObject.SetActive(true);
    }
    
    private void Hide()
    {
        gameObject.SetActive(false);
    }
}
