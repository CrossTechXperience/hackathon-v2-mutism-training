using UnityEngine;

public class AudioRecorderWithName : AudioRecorder
{
    public AudioSaveFileWithName saverWithName;

    public override void StopRecording(bool playImmediately = true)
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
        saverWithName.ShowPopUpMessage();

    }
}
