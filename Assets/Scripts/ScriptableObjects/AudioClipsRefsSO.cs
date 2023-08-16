using UnityEngine;

[CreateAssetMenu(fileName = "AudioClipsRefsSO", menuName = "ScriptableObjects/AudioClipsRefsSO")]
public class AudioClipsRefsSO : ScriptableObject
{
    public AudioClip[] chop;
    public AudioClip[] deliverySuccess;
    public AudioClip[] deliveryFail;
    public AudioClip[] footstep;
    public AudioClip[] objectDrop;
    public AudioClip[] objectPickup;
    public AudioClip[] stoveSizzle;
    public AudioClip[] trash;
    public AudioClip[] warning;
}
