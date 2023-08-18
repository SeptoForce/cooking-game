using System;
using UnityEngine;

public class StoveCounterVisual : MonoBehaviour
{
    
    [SerializeField] private StoveCounter stoveCounter;
    [SerializeField] private GameObject stoveOnVisual;
    [SerializeField] private GameObject particleVisual;

    private void Start()
    {
        stoveCounter.OnStateChanged += OnStateChanged;
    }
    
    private void OnDisable()
    {
        stoveCounter.OnStateChanged -= OnStateChanged;
    }

    private void OnStateChanged(StoveCounter.State stoveState)
    {
        switch (stoveState)
        {
            case StoveCounter.State.Idle:
                stoveOnVisual.SetActive(false);
                particleVisual.SetActive(false);
                break;
            case StoveCounter.State.Frying:
                stoveOnVisual.SetActive(true);
                particleVisual.SetActive(true);
                break;
            case StoveCounter.State.Fried:
                stoveOnVisual.SetActive(true);
                particleVisual.SetActive(true);
                break;
            case StoveCounter.State.Burned:
                stoveOnVisual.SetActive(false);
                particleVisual.SetActive(false);
                break;
        }
    }
}
