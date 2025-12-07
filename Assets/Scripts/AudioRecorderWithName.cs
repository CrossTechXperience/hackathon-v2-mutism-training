using UnityEngine;

public class AudioRecorderWithName : AudioRecorder
{
    public AudioSaveFileWithName saverWithName;

    public override void StopRecording(bool playImmediately = true)
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
                base.playbackSource.clip = RecordedClip;
                playbackSource.Play();
                Debug.Log("Playing recorded clip immediately!");
            }
            saverWithName.ShowPopUpMessage();
        }
        else
        {
            Debug.LogWarning("Recorded clip has no samples yet.");
        }
    }
}
