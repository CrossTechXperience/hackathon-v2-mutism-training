using UnityEngine;
using System.IO;

public class AudioSaveFile : MonoBehaviour
{
    [SerializeField] protected string _folderName = "RecordedAudio";

    public string SaveClip(AudioClip clip, string fileName)
    {
        if (clip == null)
        {
            Debug.LogError("AudioSaver: No AudioClip provided!");
            return null;
        }

        // Make sure a folder exists
        string folderPath = Path.Combine(Application.persistentDataPath, _folderName);
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath);

        // Full path to the .wav file
        string filePath = Path.Combine(folderPath, fileName + ".wav");

        // Convert AudioClip → WAV byte data
        byte[] wavData = ConvertToWav(clip);

        // Write it to the file
        File.WriteAllBytes(filePath, wavData);

        Debug.Log("Audio saved to: " + filePath);
        return filePath;
    }

    // -------------------------------
    // AudioClip → WAV Conversion
    // -------------------------------
    private byte[] ConvertToWav(AudioClip clip)
    {
        float[] samples = new float[clip.samples * clip.channels];
        clip.GetData(samples, 0);

        MemoryStream stream = new MemoryStream();

        int frequency = clip.frequency;
        int channels = clip.channels;

        // Write WAV header first
        WriteWavHeader(stream, samples.Length, channels, frequency);

        // Convert float samples → 16-bit PCM
        foreach (float sample in samples)
        {
            short s = (short)(sample * short.MaxValue);
            stream.WriteByte((byte)(s));
            stream.WriteByte((byte)(s >> 8));
        }

        return stream.ToArray();
    }

    // -------------------------------
    // WAV Header Writer
    // -------------------------------
    private void WriteWavHeader(Stream stream, int samples, int channels, int frequency)
    {
        int byteRate = frequency * channels * 2;   // 16-bit audio = 2 bytes per sample

        using (BinaryWriter bw = new BinaryWriter(stream, System.Text.Encoding.UTF8, true))
        {
            bw.Write(System.Text.Encoding.ASCII.GetBytes("RIFF"));                // Chunk ID
            bw.Write(36 + samples * 2);                                           // Chunk Size
            bw.Write(System.Text.Encoding.ASCII.GetBytes("WAVE"));                // Format
            bw.Write(System.Text.Encoding.ASCII.GetBytes("fmt "));                // Subchunk1 ID
            bw.Write(16);                                                         // Subchunk1 Size
            bw.Write((short)1);                                                   // Audio Format (1 = PCM)
            bw.Write((short)channels);                                            // Channels
            bw.Write(frequency);                                                  // Sample Rate
            bw.Write(byteRate);                                                   // Byte Rate
            bw.Write((short)(channels * 2));                                      // Block Align
            bw.Write((short)16);                                                  // Bits per sample
            bw.Write(System.Text.Encoding.ASCII.GetBytes("data"));                // Subchunk2 ID
            bw.Write(samples * 2);                                                // Subchunk2 Size
        }
    }
}
