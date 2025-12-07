using UnityEngine;
using UnityEngine.EventSystems;

public class MicButtonInteraction : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    [SerializeField] private AudioRecorder _audioRecorder;
    [SerializeField] private UIFeedback _uiFeedback;

    public void OnPointerDown(PointerEventData eventData)
    {
        // User starts pressing the mic button
        _audioRecorder.StartRecording();
        _uiFeedback.OnRecordingStart();
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        // User releases the mic button
        _audioRecorder.StopRecording();
        _uiFeedback.OnRecordingStop();
    }
}
