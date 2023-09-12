using System;
using System.Collections.Generic;
using UnityEngine;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private BaseCounter selectedCounter;
    [SerializeField] private List<GameObject> selectionVisual;

    private void Start()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.onSelectedCounterChanged += OnSelectedCounterChanged;
        }
        else
        {
            Player.OnAnyPlayerSpawned += OnAnyPlayerSpawned;
        }
    }

    private void OnAnyPlayerSpawned()
    {
        if (Player.LocalInstance != null)
        {
            Player.LocalInstance.onSelectedCounterChanged -= OnSelectedCounterChanged;
            Player.LocalInstance.onSelectedCounterChanged += OnSelectedCounterChanged;
        }
    }

    private void OnDestroy()
    {
        try
        {
            Player.LocalInstance.onSelectedCounterChanged -= OnSelectedCounterChanged;
            Player.OnAnyPlayerSpawned -= OnAnyPlayerSpawned;
        }
        catch (Exception e)
        {
            // ignored
        }
    }

    private void OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        foreach (GameObject visual in selectionVisual)
        {
            visual.SetActive(e.SelectedCounter == selectedCounter);
        }
    }
}