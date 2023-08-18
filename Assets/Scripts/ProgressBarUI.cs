using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private GameObject hasProgressGameObject;
    [SerializeField] private Image barImage;
    
    private IHasProgress _counterWithProgress;

    private void Start()
    {
        barImage.fillAmount = 0;
        
        _counterWithProgress = hasProgressGameObject.GetComponent<IHasProgress>();
        if (_counterWithProgress == null)
        {
            Debug.LogError("IHasProgress not found on " + hasProgressGameObject.name);
            return;
        }
        
        _counterWithProgress.OnProgressChanged += OnProgressChanged;
        
        Hide();
    }
    
    private void OnDestroy()
    {
        if (_counterWithProgress != null)
        {
            _counterWithProgress.OnProgressChanged -= OnProgressChanged;
        }
    }

    private void OnProgressChanged(float progressNormalized)
    {
        barImage.fillAmount = progressNormalized;

        if (progressNormalized > 0 && progressNormalized < 1)
        {
            Show();
        }
        else
        {
            Hide(); 
        } 
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
