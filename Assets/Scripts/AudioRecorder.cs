using UnityEngine;
using UnityEngine.Profiling;

public class AudioRecorder : MonoBehaviour
{
    public AudioClip RecordedClip { get; private set; }
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

        // Stop microphone capture
        Microphone.End(_microphoneName);
        Debug.Log("Recording stopped. Clip ready to save.");

        // Check that RecordedClip exists and has samples
        if (RecordedClip != null && RecordedClip.samples > 0)
        {
            if (playImmediately && playbackSource != null)
            {
                playbackSource.clip = RecordedClip;
                playbackSource.Play();
                Debug.Log("Playing recorded clip immediately!");
            }
            SaveRecording();
        }
        else
        {
            Debug.LogWarning("Recorded clip has no samples yet.");
        }
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
