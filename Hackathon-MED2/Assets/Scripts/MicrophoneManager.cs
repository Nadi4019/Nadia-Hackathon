using NUnit.Framework;
using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Rendering.Universal;
using UnityEngine.Rendering;
using TMPro;

public class MicrophoneManager : MonoBehaviour
{
    public float loudness = 0f; // Output: current loudness
    public float sensitivity = 100f; // Adjust this to scale loudness

    private AudioSource _audioSource;
    private const int SampleWindow = 128; // Number of samples to analyze

    private List<float> averageLoudness = new List<float>();
    [SerializeField] private MonsterController monsterController;

    [SerializeField] private TMP_Text soundText;

    void Start()
    {
        _audioSource = GetComponent<AudioSource>();

        // Start recording from the default microphone
        _audioSource.clip = Microphone.Start(null, true, 1, AudioSettings.outputSampleRate);
        _audioSource.loop = true;
        _audioSource.mute = true;

        // Wait until the microphone starts before playing
        while (!(Microphone.GetPosition(null) > 0)) { }

        _audioSource.Play();
    }

    void Update()
    {
        if(averageLoudness.Count >= 100)
        {
            averageLoudness.RemoveAt(0);
        }
        loudness = GetMaxVolume() * sensitivity;
        averageLoudness.Add(loudness);

        float average = 0;
        foreach(float f in averageLoudness)
        {
            average += f;
        }

        average /= averageLoudness.Count;

        soundText.text = average.ToString();

        if(average > 3f)
        {
            monsterController.monster.SetActive(true);
        }

        //if(average > 50f)
        //{
        //    UnityEngine.SceneManagement.SceneManager.LoadScene("Endscreen");
        //}
        
    }

    float GetMaxVolume()
    {
        float maxVolume = 0f;
        float[] waveData = new float[SampleWindow];
        int micPosition = Microphone.GetPosition(null) - (SampleWindow + 1);
        if (micPosition < 0) return 0;

        _audioSource.clip.GetData(waveData, micPosition);
        for (int i = 0; i < SampleWindow; i++)
        {
            float wavePeak = Mathf.Abs(waveData[i]);
            if (wavePeak > maxVolume)
            {
                maxVolume = wavePeak;
            }
        }
        return maxVolume;
    }
}
