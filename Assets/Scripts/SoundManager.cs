using System;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    [SerializeField] private AudioClipsRefsSO audioClipsRefsSo;
    
    
    public static SoundManager Instance { get; private set; }
    
    private float _volume = 0.9f;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        _volume = PlayerPrefs.GetFloat("sfxVolume", _volume);
    }

    private void Start()
    {
        DeliveryManager.Instance.OnRecipeSuccess += PlayRecipeSuccessSound;
        DeliveryManager.Instance.OnRecipeFailed += PlayRecipeFailedSound;
        CuttingCounter.OnAnyCut += PlayCutSound;
        Player.Instance.OnPickedUpSomething += PlayPickupSound;
        BaseCounter.OnAnyObjectPlacedHere += PlayPlaceSound;
        TrashCounter.OnAnyObjectTrashed += PlayTrashSound;
    }
    
    private void OnDestroy()
    {
        DeliveryManager.Instance.OnRecipeSuccess -= PlayRecipeSuccessSound;
        DeliveryManager.Instance.OnRecipeFailed -= PlayRecipeFailedSound;
        CuttingCounter.OnAnyCut -= PlayCutSound;
        Player.Instance.OnPickedUpSomething -= PlayPickupSound;
        BaseCounter.OnAnyObjectPlacedHere -= PlayPlaceSound;
        TrashCounter.OnAnyObjectTrashed -= PlayTrashSound;
    }

    public void ScrollVolume()
    {
        _volume += .1f;
        if (_volume > 1f)
        {
            _volume = 0f;
        }
        
        PlayerPrefs.SetFloat("sfxVolume", _volume);
    }
    
    public float GetVolume()
    {
        return _volume;
    }

    private void PlayTrashSound(TrashCounter obj)
    {
        PlaySound(audioClipsRefsSo.trash, obj.transform.position);
    }

    private void PlayPlaceSound(BaseCounter obj)
    {
        PlaySound(audioClipsRefsSo.objectDrop, obj.transform.position);
    }

    private void PlayPickupSound(Player obj)
    {
        PlaySound(audioClipsRefsSo.objectPickup, obj.transform.position);
    }

    private void PlayCutSound(CuttingCounter obj)
    {
        PlaySound(audioClipsRefsSo.chop, obj.transform.position);
    }

    private void PlayRecipeFailedSound()
    {
        PlaySound(audioClipsRefsSo.deliveryFail, DeliveryCounter.Instance.transform.position);
    }

    private void PlayRecipeSuccessSound()
    {
        PlaySound(audioClipsRefsSo.deliverySuccess, DeliveryCounter.Instance.transform.position);
    }

    private void PlaySound(AudioClip audioClip, Vector3 position, float volume = 1f)
    {
        AudioSource.PlayClipAtPoint(audioClip, position, _volume * volume);
    }
    
    private void PlaySound(AudioClip[] audioClipArray, Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipArray[UnityEngine.Random.Range(0, audioClipArray.Length)], position, _volume * volume);
    }
    
    public void PlayFootstepSound(Vector3 position, float volume = 1f)
    {
        PlaySound(audioClipsRefsSo.footstep, position);
    }
}
