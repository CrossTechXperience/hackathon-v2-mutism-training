using UnityEngine;

public class UIButtonSound : MonoBehaviour
{
    public AudioSource audioSource;
    public AudioClip clip;

    public void PlayClickSound()
    {
        audioSource.PlayOneShot(clip);
    }
}
