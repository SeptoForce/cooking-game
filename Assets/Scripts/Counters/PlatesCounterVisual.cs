using System;
using System.Collections.Generic;
using UnityEngine;

public class PlatesCounterVisual : MonoBehaviour
{
    [SerializeField] private Transform counterTopPoint;
    [SerializeField] private Transform platesVisualPrefab;
    [SerializeField] private PlatesCounter platesCounter;
    
    private List<GameObject> _platesVisuals = new List<GameObject>();

    private void Start()
    {
        platesCounter.OnPlateSpawned += OnPlateSpawned;
        platesCounter.OnPlateRemoved += OnPlateRemoved;
    }

    private void OnPlateRemoved()
    {
        GameObject plateVisual = _platesVisuals[_platesVisuals.Count - 1];
        _platesVisuals.Remove(plateVisual);
        Destroy(plateVisual);
    }

    private void OnPlateSpawned()
    {
        Transform plateVisual = Instantiate(platesVisualPrefab, counterTopPoint);
        float plateOffsetY = 0.1f;
        plateVisual.localPosition = new Vector3(0, plateOffsetY * _platesVisuals.Count, 0);
        _platesVisuals.Add(plateVisual.gameObject);
    }
}
