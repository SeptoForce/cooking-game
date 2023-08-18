using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class GameOptionsUI : MonoBehaviour
{
    public static GameOptionsUI Instance { get; private set; }
    
    [SerializeField] private Button soundEffectsButton;
    [SerializeField] private Button musicButton;
    [SerializeField] private Button closeButton;
    [SerializeField] private TextMeshProUGUI soundEffectsText;
    [SerializeField] private TextMeshProUGUI musicText;
    
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
    }
    
    private void Start()
    {
        soundEffectsButton.onClick.AddListener(OnSoundEffectsButtonClicked);
        musicButton.onClick.AddListener(OnMusicButtonClicked);
        closeButton.onClick.AddListener(OnCloseButtonClicked);
        
        Hide();
        
        KitchenGameManager.Instance.OnGameUnpaused += Hide;
        
        UpdateVisuals();
    }

    private void OnCloseButtonClicked()
    {
        Hide();
    }

    private void OnMusicButtonClicked()
    {
        MusicManager.Instance.ScrollVolume();
        UpdateVisuals();
    }

    private void OnSoundEffectsButtonClicked()
    {
        SoundManager.Instance.ScrollVolume();
        UpdateVisuals();
    }
    
    private void UpdateVisuals()
    {
        soundEffectsText.text = $"Sound Effects: {(int)(SoundManager.Instance.GetVolume() * 10)}";
        musicText.text = $"Music: {(int)(MusicManager.Instance.GetVolume() * 10)}";
    }
    
    public void Show()
    {
        gameObject.SetActive(true);
    }
    
    public void Hide()
    {
        gameObject.SetActive(false);
    }

    private void OnDestroy()
    {
        soundEffectsButton.onClick.RemoveListener(OnSoundEffectsButtonClicked);
        musicButton.onClick.RemoveListener(OnMusicButtonClicked);
        closeButton.onClick.RemoveListener(OnCloseButtonClicked);
        KitchenGameManager.Instance.OnGameUnpaused -= Hide;
    }
}
