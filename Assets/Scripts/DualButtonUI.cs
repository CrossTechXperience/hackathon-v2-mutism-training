using UnityEngine;
using UnityEngine.UI;

public class DualButtonUI : MonoBehaviour
{
    [Header("Buttons")]
    [SerializeField] private Button playbackButton;
    [SerializeField] private Button deleteButton;

    public Button PlayButton => playbackButton;
    public Button DeleteButton => deleteButton;
}