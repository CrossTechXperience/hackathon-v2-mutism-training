using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class MapManager : MonoBehaviour
{
    [SerializeField] private Button Island02;
    [SerializeField] private Button Island03;
    // TODO: These two are repeated in AnswerSelector, there should be in only one place
    private static readonly HashSet<string> stage02validNames =
    new HashSet<string> { "yes", "oui", "ja" };
    private static readonly HashSet<string> stage03validNames =
    new HashSet<string> { "nee", "no", "non" };
    void Start()
    {
        List<string> audioNames = AudioFileUtils.GetWavFiles();

        bool hasPositive = audioNames
        .Select(name => name.Trim().ToLower())
        .Any(name => stage02validNames.Contains(name));

        bool hasNegative = audioNames
            .Select(name => name.Trim().ToLower())
            .Any(name => stage03validNames.Contains(name));

        Island02.interactable = hasPositive;
        Island03.interactable = hasPositive && hasNegative;
    }

}
