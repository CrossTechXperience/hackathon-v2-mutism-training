using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIFeedback : MonoBehaviour
{
    [Header("UI Image Component")]
    public Image micButtonImage;      // The UI Image component of the mic button

    [Header("Sprites")]
    public Sprite unpressedSprite;         
    public Sprite pressedSprite;    

    [Header("Text")]
    public TextMeshProUGUI recordingText;          

    [Header("Recording Pulse")]
    public float pulseScale = 1.2f;           // Button scale when recording
    public float pulseSpeed = 2f;             // Speed of pulsing

    public bool isRecording = false;
    private Vector3 _originalScale;

    private void Start()
    {
        if (micButtonImage != null && unpressedSprite != null)
            micButtonImage.sprite = unpressedSprite;

        if (recordingText != null)
            recordingText.gameObject.SetActive(false);

        _originalScale = micButtonImage.transform.localScale;
    }

    private void Update()
    {
        // Pulse the button when recording
        if (isRecording && micButtonImage != null)
        {
            float scale = 1 + Mathf.Sin(Time.time * pulseSpeed) * (pulseScale - 1);
            micButtonImage.transform.localScale = _originalScale * scale;
        }
        else if (micButtonImage != null)
        {
            micButtonImage.transform.localScale = _originalScale;
        }
    }

    public void OnRecordingStart()
    {
        Debug.Log("Recording started, showing text!");
        isRecording = true;
        if (micButtonImage != null && pressedSprite != null)
            micButtonImage.sprite = pressedSprite;

        if (recordingText != null)
            recordingText.gameObject.SetActive(true);
    }

    public void OnRecordingStop()
    {
        isRecording = false;

        if (micButtonImage != null)
            micButtonImage.transform.localScale = _originalScale;

        if (micButtonImage != null && unpressedSprite != null)
            micButtonImage.sprite = unpressedSprite;

        if (recordingText != null)
            recordingText.gameObject.SetActive(false);
    }
}
