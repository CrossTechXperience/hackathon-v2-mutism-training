using UnityEngine;

public class ButtonIdleAnimation : MonoBehaviour
{
    [Header("Idle Pulse Settings")]
    public float idleScale = 1.05f;     // How much it grows
    public float idleSpeed = 2f;        // How fast it pulses
    public bool playOnStart = true;     // Should it pulse automatically?

    private Vector3 originalScale;
    private bool isPulsing;

    private void Start()
    {
        originalScale = transform.localScale;

        if (playOnStart)
            StartPulse();
    }

    private void Update()
    {
        if (!isPulsing)
            return;

        float scale = 1f + Mathf.Sin(Time.time * idleSpeed) * (idleScale - 1f);
        transform.localScale = originalScale * scale;
    }

    public void StartPulse()
    {
        isPulsing = true;
    }

    public void StopPulse()
    {
        isPulsing = false;
        transform.localScale = originalScale;
    }
}
