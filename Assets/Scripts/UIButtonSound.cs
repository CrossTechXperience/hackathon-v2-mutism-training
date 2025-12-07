using UnityEngine;
using UnityEngine.SceneManagement;

public class UIButtonSound : MonoBehaviour
{
    public AudioClip clip;
    public void PlaySoundThenLoadScene(string sceneName)
    {
        StartCoroutine(PlayAndLoad(sceneName));
    }

    private System.Collections.IEnumerator PlayAndLoad(string sceneName)
    {
        // Create temporary audio object
        GameObject audioObj = new GameObject("TempAudio");
        AudioSource source = audioObj.AddComponent<AudioSource>();
        source.clip = clip;

        DontDestroyOnLoad(audioObj);  // Keep this audio alive through scene changes
        source.Play();

        yield return new WaitForSeconds(clip.length);

        Destroy(audioObj);

        SceneManager.LoadScene(sceneName);
    }
}
