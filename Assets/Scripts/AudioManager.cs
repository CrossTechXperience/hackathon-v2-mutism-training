using UnityEngine;

public class AudioManager : MonoBehaviour
{
    // Singleton (easy access from any script)
    public static AudioManager Instance;

    [Header("Audio Source Settings")]
    [SerializeField] private AudioSource _soundSource;

    [Header("Sounds")]
    [SerializeField] private AudioClip[] _nonSliceableHitSound;
    [SerializeField] private AudioClip[] _sliceableHitSound;

    //[SerializeField] private AudioClip[] _enemyHitSounds;
    //[SerializeField] private AudioClip[] _enemySpawnSounds;
    //[SerializeField] private AudioClip[] _babySpawnSounds;
    //[SerializeField] private AudioClip[] _babyDeathSounds;
    //[SerializeField] private AudioClip[] sceneAnimationSounds;

    private void Awake()
    {
        // Setup Singleton
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    /// <summary>
    /// Plays a random sound from the given audio clips.
    /// </summary>
    private void PlayRandomSound(AudioClip[] soundPool)
    {
        if (soundPool == null || soundPool.Length == 0)
            return;

        int randomIndex = Random.Range(0, soundPool.Length);
        _soundSource.PlayOneShot(soundPool[randomIndex]);
    }

    public void NonSliceableHitSound()
    {
        PlayRandomSound(_nonSliceableHitSound);
    }

    public void SliceableHitSound()
    {
        PlayRandomSound(_sliceableHitSound);
    }

    //public void PlayEnemyHitSound()
    //{
    //    PlayRandomSound(_enemyHitSounds);
    //}

    //public void PlayEnemySpawnSound()
    //{
    //    PlayRandomSound(_enemySpawnSounds);
    //}

    //public void PlayBabySpawnSound()
    //{
    //    PlayRandomSound(_babySpawnSounds);
    //}

    //public void PlayBabyDeathSound()
    //{
    //    PlayRandomSound(_babyDeathSounds);
    //}

    //public void PlaySceneAnimationSound()
    //{
    //    PlayRandomSound(sceneAnimationSounds);
    //}
}
