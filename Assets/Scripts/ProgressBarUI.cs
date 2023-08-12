using UnityEngine;
using UnityEngine.UI;

public class ProgressBarUI : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    [SerializeField] private Image barImage;

    private void Start()
    {
        barImage.fillAmount = 0;
        cuttingCounter.onCuttingProgressChanged += OnCuttingProgressChanged;
        
        Hide();
    }

    private void OnCuttingProgressChanged(float progressNormalized)
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
