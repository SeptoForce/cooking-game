using System;
using UnityEngine;

public class CuttingCounterVisual : MonoBehaviour
{
    [SerializeField] private ContainerCounter containerCounter;
    private Animator _animator;
    private static readonly int OpenClose = Animator.StringToHash("OpenClose");

    private void Awake()
    {
        _animator = GetComponent<Animator>();
    }

    private void Start()
    {
        containerCounter.OnPlayerGrabbedObject += OnPlayerGrabbedObject;
    }
    
    private void OnPlayerGrabbedObject(object sender, EventArgs e)
    {
        _animator.SetTrigger(OpenClose);
    }
}
