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
    [SerializeField] private TTSManager _ttsManager;
    [SerializeField] private SceneTransitionManager sceneManager;
    private int _selectedAudio;
    private List<string> audioNames = new();
    private TextMeshProUGUI _playButtonLabel;
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
        _ttsManager.SynthesizeAndPlay("Bonjour Clément.As - tu fini tes devoirs ?");
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
        if (audioNames.Count == 0)
            return;
        audioPlayback.PlayAudio(audioNames[_selectedAudio]);
        if (audioNames[_selectedAudio].ToLower() == "yes")
        {
            // WIN
            sceneManager.LoadScene("RewardScene");
        }
    }
}
