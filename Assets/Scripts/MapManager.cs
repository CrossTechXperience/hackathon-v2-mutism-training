using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Button Island02;
    void Start()
    {
        // Disable Island02 button if there are no audios;
        List<string> audioNames = AudioFileUtils.GetWavFiles();
        Island02.interactable = audioNames.Count > 0;
    }

}
