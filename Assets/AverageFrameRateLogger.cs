using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
// Based on: https://forum.unity.com/threads/fps-counter.505495/

public class AverageFrameRateLogger : MonoBehaviour
{

    private Dictionary<int, string> CachedNumberStrings = new();
    private int[] _frameRateSamples;
    private int _cacheNumbersAmount = 300;
    private int _averageFromAmount = 30;
    private int _averageCounter = 0;
    private int _currentAveraged;

    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);

        // Cache strings and create array
        {
            for (int i = 0; i < _cacheNumbersAmount; i++)
            {
                CachedNumberStrings[i] = i.ToString();
            }
            _frameRateSamples = new int[_averageFromAmount];
        }
    }
    void Update()
    {
        // Sample
        {
            var currentFrame = (int)Math.Round(1f / Time.smoothDeltaTime); // If your game modifies Time.timeScale, use unscaledDeltaTime and smooth manually (or not).
            if (currentFrame > 0)
            {
                _frameRateSamples[_averageCounter] = currentFrame;
            }     
        }

        // Average
        {
            var average = 0f;

            foreach (var frameRate in _frameRateSamples)
            {
                average += frameRate;
            }

            _currentAveraged = (int)Math.Round(average / _averageFromAmount);
            _averageCounter = (_averageCounter + 1) % _averageFromAmount;
        }

        
    }

    private void OnApplicationQuit()
    {
        // Put in txt file
        // Write average FPS to a text file
        string filePath = Application.dataPath + "/Out/average_fps.txt"; // Path within your project's data folder
        string averageText = "Current scene: " + SceneManager.GetActiveScene().name +
            " Average FPS" + PlayerPrefs.GetString("character") + ": " + _currentAveraged.ToString() + "\n";
        if (!File.Exists(filePath))
        {
            File.WriteAllText(filePath, averageText);
        }
        File.AppendAllText(filePath, averageText);
    }
}
