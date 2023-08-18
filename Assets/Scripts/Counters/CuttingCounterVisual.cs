using System;
using UnityEngine;

public class ContainerCounterVisual : MonoBehaviour
{
    [SerializeField] private CuttingCounter cuttingCounter;
    private Animator _animator;
    private static readonly int Cut = Animator.StringToHash("Cut");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        cuttingCounter.OnPlayerCutObject += OnPlayerCutObject;
    }

    private void OnDisable()
    {
        cuttingCounter.OnPlayerCutObject -= OnPlayerCutObject;
    }

    private void OnPlayerCutObject()
    {
        _animator.SetTrigger(Cut);
    }
}
