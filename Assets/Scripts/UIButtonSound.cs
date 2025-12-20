using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip clip;
    public void PlaySoundThenLoadScene(string sceneName)
    {
        StartCoroutine(PlayAndLoad(sceneName));
    }

    private IEnumerator PlayAndLoad(string sceneName)
    {
        // Create temporary audio object
        GameObject audioObj = new GameObject("TempAudio");
        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = clip;

        DontDestroyOnLoad(audioObj);  // Keep this audio alive through scene changes
        source.Play();

        yield return new WaitForSeconds(clip.length);

        Destroy(audioObj);
        if (!string.IsNullOrEmpty(sceneName))
            SceneManager.LoadScene(sceneName);
    }
}
