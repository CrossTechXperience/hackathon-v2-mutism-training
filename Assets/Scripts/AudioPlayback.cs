using UnityEngine;
using System.IO;

public class AudioPlayback : MonoBehaviour
{
    [Header("Audio Player")]
    public AudioSource audioSource;     // Assign a GameObject with AudioSource

    [Header("Volume Control")]
    [Range(0f, 1f)]
    public float volume = 1f;

    /// <summary>
    /// Load and play a WAV file from persistent data path
    /// </summary>
    /// <param name="fileName">Name of the file without extension</param>
    public void PlayAudio(string fileName)
    {
        string filePath = Path.Combine(Application.persistentDataPath, "RecordedAudio", fileName + ".wav");

        if (!File.Exists(filePath))
        {
            Debug.LogError("AudioPlayback: File not found - " + filePath);
            return;
        }

        // Load audio data
        StartCoroutine(LoadAndPlay(filePath));
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }

    private System.Collections.IEnumerator LoadAndPlay(string path)
    {
        // UnityWebRequest works cross-platform to load local files
        using (var www = UnityEngine.Networking.UnityWebRequestMultimedia.GetAudioClip("file://" + path, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityEngine.Networking.UnityWebRequest.Result.Success)
            {
                Debug.LogError("AudioPlayback: Failed to load - " + www.error);
            }
            else
            {
                AudioClip clip = UnityEngine.Networking.DownloadHandlerAudioClip.GetContent(www);
                audioSource.clip = clip;
                audioSource.volume = volume;
                audioSource.Play();
            }
        }
    }
}
