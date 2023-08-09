using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SelectedCounterVisual : MonoBehaviour
{
    [SerializeField] private ClearCounter selectedCounter;
    [SerializeField] private GameObject selectionVisual;
    
    private void Start()
    {
        Player.Instance.onSelectedCounterChanged += OnSelectedCounterChanged;
    }

    private void OnSelectedCounterChanged(object sender, Player.SelectedCounterChangedEventArgs e)
    {
        if(e.SelectedCounter == selectedCounter)
        {
            selectionVisual.SetActive(true);
        }
        else
        {
            selectionVisual.SetActive(false);
        }
    }
}
