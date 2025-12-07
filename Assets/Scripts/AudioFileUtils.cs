using System.Collections.Generic;
using System.IO;
using UnityEngine;

public static class AudioFileUtils
{
    public static List<string> GetWavFiles()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, "RecordedAudio");

        // If folder doesn't exist, return empty list
        if (!Directory.Exists(folderPath))
        {
            Debug.Log("Folder does not exist: " + folderPath);
            return new List<string>();
        }

        // Get all .wav files (full paths)
        string[] files = Directory.GetFiles(folderPath, "*.wav");

        // Extract the names without .wav extension
        List<string> fileNames = new List<string>();
        foreach (var f in files)
        {
            fileNames.Add(Path.GetFileNameWithoutExtension(f));
        }

        return fileNames;
    }
}
