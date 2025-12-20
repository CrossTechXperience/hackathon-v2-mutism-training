using UnityEngine;
using UnityEngine.Profiling;

public class AudioRecorder : MonoBehaviour
{
    public AudioClip RecordedClip { get; set; }
    public AudioRecorder recorder;
    public AudioSaveFile saver;

    [SerializeField] protected int _maxRecordingLength = 10;    // Max seconds to record
    [SerializeField] protected int _sampleRate = 44100;         // Standard audio quality
    [SerializeField] protected AudioSource playbackSource;

    protected string _microphoneName;
    protected bool _isRecording = false;

    public void StartRecording()
    {
        if (_isRecording) return;

        // Choose the default microphone
        _microphoneName = Microphone.devices.Length > 0 ? Microphone.devices[0] : null;

        if (_microphoneName == null)
        {
            Debug.LogError("No microphone detected!");
            return;
        }

        Debug.Log("Recording Started");
        _isRecording = true;

        // Begin recording
        RecordedClip = Microphone.Start(_microphoneName, false, _maxRecordingLength, _sampleRate);
    }

    public virtual void StopRecording(bool playImmediately = true)
    {
        if (!_isRecording) return;

        _isRecording = false;

        int sampleCount = Microphone.GetPosition(_microphoneName);
        Microphone.End(_microphoneName);
        if (sampleCount <= 0)
        {
            Debug.LogWarning("No samples recorded.");
            return;
        }
        // Stop microphone capture
        
        Debug.Log("Recording stopped. Clip ready to save.");
        float[] samples = new float[sampleCount * RecordedClip.channels];
        RecordedClip.GetData(samples, 0);

        AudioClip trimmedClip = AudioClip.Create(
            RecordedClip.name,
            sampleCount,
            RecordedClip.channels,
            RecordedClip.frequency,
            false
        );
        trimmedClip.SetData(samples, 0);
        RecordedClip = trimmedClip;
        Debug.Log($"Recording stopped. Actual length: {RecordedClip.length:F2} seconds");

        // Play immediately if needed
        if (playImmediately && playbackSource != null)
        {
            playbackSource.clip = RecordedClip;
            playbackSource.Play();
            Debug.Log("Playing recorded clip immediately!");
        }

        // Save
        SaveRecording();
    }
    void SaveRecording(string fileName = "MyRecordedAudio")
    {
        //saver.SaveClip(recorder.RecordedClip, "MyRecordedAudio");
        if (recorder != null && saver != null && RecordedClip != null)
        {
            saver.SaveClip(recorder.RecordedClip, fileName);
            Debug.Log("Recording saved as " + fileName);
        }
        else
        {
            Debug.LogWarning("Cannot save: AudioRecorder, AudioSaver, or RecordedClip is missing.");
        }
    }
}
