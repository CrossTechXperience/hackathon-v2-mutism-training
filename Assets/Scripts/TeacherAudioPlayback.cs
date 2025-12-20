using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum TeacherDialogue
{
    Greeting,
    HomeworkQuestion,
    IncorrectAnswerResponse
}

[RequireComponent(typeof(AudioSource))]
public class TeacherAudioPlayback : MonoBehaviour
{
    private AudioSource audioSource;
    private Queue<AudioClip> audioQueue = new Queue<AudioClip>();
    private Coroutine playbackCoroutine;
    [SerializeField] private AudioClip greetingClip;
    [SerializeField] private AudioClip homeworkQuestionClip;
    [SerializeField] private AudioClip incorrectAnswerClip;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    public void PlayDialogue(TeacherDialogue option)
    {
        AudioClip clip = option switch
        {
            TeacherDialogue.Greeting => greetingClip,
            TeacherDialogue.HomeworkQuestion => homeworkQuestionClip,
            TeacherDialogue.IncorrectAnswerResponse => incorrectAnswerClip,
            _ => null
        };

        if (clip == null)
            return;

        audioQueue.Enqueue(clip);

        // Start playback if not already running
        if (playbackCoroutine == null)
        {
            playbackCoroutine = StartCoroutine(PlayQueue());
        }
    }

    private IEnumerator PlayQueue()
    {
        while (audioQueue.Count > 0)
        {
            AudioClip clip = audioQueue.Dequeue();
            audioSource.clip = clip;
            audioSource.Play();

            yield return new WaitForSeconds(clip.length);
        }

        playbackCoroutine = null;
    }

}
