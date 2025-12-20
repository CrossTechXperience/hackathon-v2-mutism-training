using UnityEngine;
using System.IO;
using UnityEngine.Networking;
using System.Collections;

public class AudioPlayback : MonoBehaviour
{
    public AudioSource audioSource;

    [Range(0f, 1f)]
    public float volume = 1f;

    public IEnumerator PlayAudio(string fileName)
    {
        string filePath = Path.Combine(
            Application.persistentDataPath,
            "RecordedAudio",
            fileName + ".wav"
        );

        if (!File.Exists(filePath))
        {
            Debug.LogError("AudioPlayback: File not found - " + filePath);
            yield break;
        }

        using (UnityWebRequest www =
               UnityWebRequestMultimedia.GetAudioClip("file://" + filePath, AudioType.WAV))
        {
            yield return www.SendWebRequest();

            if (www.result != UnityWebRequest.Result.Success)
            {
                Debug.LogError("AudioPlayback: Failed to load - " + www.error);
                yield break;
            }

            AudioClip clip = DownloadHandlerAudioClip.GetContent(www);
            audioSource.clip = clip;
            audioSource.volume = volume;
            audioSource.Play();

            // WAIT until audio finishes
            yield return new WaitForSeconds(clip.length);
        }
    }

    public void StopAudio()
    {
        if (audioSource.isPlaying)
            audioSource.Stop();
    }
}
