using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter selectedCounter;
    [SerializeField] private List<GameObject> selectionVisual;
    
    private void Start()
    {
        Player.Instance.onSelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnDestroy()
    {
        Player.Instance.onSelectedCounterChanged -= OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        foreach (GameObject visual in selectionVisual)
        {
            visual.SetActive(e.SelectedCounter == selectedCounter);
        }
    }
}
