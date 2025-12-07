using System.Collections.Generic;
using System.IO;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class AudioSaveFileWithName : AudioSaveFile
{
    [SerializeField] private Button discardAudio;
    [SerializeField] private Button saveAudio;
    [SerializeField] private TMP_InputField audioName;
    [SerializeField] private AudioRecorderWithName recorder;
    [SerializeField] private GameObject playbackButtonPrefab;
    [SerializeField] private Transform playbackButtonParent;
    [SerializeField] private AudioPlayback audioPlayback;
    private GameObject _popUp;

    private void Start()
    {
        // Hide the pop up when the game starts
        _popUp = audioName.gameObject.transform.parent.gameObject;
        _popUp.SetActive(false);
        saveAudio.interactable = false;
        discardAudio.onClick.AddListener(HidePopUpMessage);
        saveAudio.onClick.AddListener(SaveClip);
        audioName.onValueChanged.AddListener(ValidateName);
        List<string> audioNames = AudioFileUtils.GetWavFiles();
        foreach (string audioName in audioNames)
        {
            createButton(audioName);
        }
    }

    public void ShowPopUpMessage()
    {
        _popUp.SetActive(true);
    }

    private void HidePopUpMessage()
    {
        _popUp.SetActive(false);
    }

    private void ValidateName(string text)
    {
        bool isValid = !string.IsNullOrWhiteSpace(text);
        if (isValid)
        {
            string folderPath = Path.Combine(Application.persistentDataPath, _folderName);
            string filePath = Path.Combine(folderPath, text.Trim() + ".wav");
            if (File.Exists(filePath))
            {
                isValid = false;
                Debug.Log("File already exists: " + filePath);
            }
        }
        saveAudio.interactable = isValid;
    }

    private void SaveClip()
    {
        AudioClip clip = recorder.RecordedClip;
        if (clip == null)
        {
            Debug.LogError("AudioSaver: No AudioClip provided!");
            return;
        }
        // Save clip with the correct name
        string clipName = audioName.text.ToString().Trim();
        base.SaveClip(clip, clipName);
        // Instantiate button 
        createButton(clipName);
        // Hide PopUp
        HidePopUpMessage();
    }

    private void createButton(string clipName)
    {
        GameObject newButton = Instantiate(playbackButtonPrefab, playbackButtonParent);
        newButton.GetComponentInChildren<TextMeshProUGUI>().text = clipName;
        // Add a callback to play the correct audio
        var btn = newButton.GetComponentInChildren<Button>();
        btn.onClick.AddListener(() =>
        {
            audioPlayback.PlayAudio(clipName);
        });
    }
}

