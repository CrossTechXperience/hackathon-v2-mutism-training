using System.Collections;
using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AnswerSelector : MonoBehaviour
{
    [SerializeField] private AudioPlayback audioPlayback;
    [SerializeField] private Button leftArrow;
    [SerializeField] private Button playAudio;
    [SerializeField] private Button rightArrow;
    [SerializeField] private GameObject winningParticles;
    // [SerializeField] private TTSManager _ttsManager;
    [SerializeField] private TeacherAudioPlayback teacher;
    [SerializeField] private SceneTransitionManager sceneManager;
    [SerializeField] public AudioSource audioSource;
    [SerializeField] public AudioClip correctAnswerSound;

    private int _selectedAudio;
    private List<string> audioNames = new();
    private TextMeshProUGUI _playButtonLabel;

    private static readonly HashSet<string> validAnswers =
    new HashSet<string> { "yes", "no", "ja", "nee", "oui", "non" };

    void Start()
    {
        _selectedAudio = 0;
        leftArrow.interactable = false;
        rightArrow.interactable = false;
        playAudio.interactable = false;
        _playButtonLabel = playAudio.GetComponentInChildren<TextMeshProUGUI>();
        audioNames = AudioFileUtils.GetWavFiles();
        if (audioNames.Count > 0)
        {
            playAudio.interactable = true;
            UpdateButtonName();
        }
        else
        {
            _playButtonLabel.text = "No audios";
        }
        if (audioNames.Count > 1)
        {
            leftArrow.interactable = true;
            rightArrow.interactable = true;
        }
        playAudio.onClick.AddListener(PlaySelectedAudio);
        leftArrow.onClick.AddListener(() => ChangeSelection(-1));
        rightArrow.onClick.AddListener(() => ChangeSelection(+1));
        teacher.PlayDialogue(TeacherDialogue.Greeting);
        teacher.PlayDialogue(TeacherDialogue.HomeworkQuestion);
        //_ttsManager.SynthesizeAndPlay("As - tu fini tes devoirs?");
        //_ttsManager.SynthesizeAndPlay("Bonjour Clèment");
        //_ttsManager.SynthesizeAndPlay("C'est bien, mais parlons des devoirs.");
        audioSource.clip = correctAnswerSound;
    }

    private void ChangeSelection(int delta)
    {
        if (audioNames.Count == 0)
            return;
        _selectedAudio = (_selectedAudio + delta + audioNames.Count) % audioNames.Count;
        UpdateButtonName();
    }

    private void UpdateButtonName()
    {
        if (audioNames.Count == 0)
            return;
        _playButtonLabel.text = audioNames[_selectedAudio];
    }

    private void PlaySelectedAudio()
    {
        StartCoroutine(PlaySelectedAudioCoroutine());
    }

    private IEnumerator PlaySelectedAudioCoroutine()
    {
        if (audioNames.Count == 0)
            yield break;

        // Wait for selected audio to finish
        yield return StartCoroutine(
            audioPlayback.PlayAudio(audioNames[_selectedAudio])
        );

        if (validAnswers.Contains(audioNames[_selectedAudio].ToLower()))
        {
            Instantiate(winningParticles, this.transform);
            audioSource.Play();
            yield return new WaitForSeconds(2f);
            sceneManager.LoadScene("RewardScene");
        }
        else
        {
            teacher.PlayDialogue(TeacherDialogue.IncorrectAnswerResponse);
            teacher.PlayDialogue(TeacherDialogue.HomeworkQuestion);
        }
    }
}
