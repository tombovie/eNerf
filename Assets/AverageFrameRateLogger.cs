using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.CompilerServices;
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

    private DateTime startTime;
    private DateTime endTime;
    private double amountOfFrames;

    void Awake()
    {
        startTime = DateTime.Now;

        string formattedTime = startTime.ToString("HH:mm:ss");  // "HH" for 24-hour format, "hh" for 12-hour format
        string filePath = Path.Combine(Application.persistentDataPath, "average_fps.txt");
        string averageText = "Scene started at" + " timestamp: " + formattedTime + "\n";
        if (!File.Exists(filePath))
        {
            Debug.Log("don't exist");
            File.WriteAllText(filePath, averageText);
        }
        else
        {
            Debug.Log("exist");
            File.AppendAllText(filePath, averageText);
        }
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
        amountOfFrames++;
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

    public void printFR()
    {
        // Timestamp
        endTime = DateTime.Now;
        string formattedTime = endTime.ToString("HH:mm:ss");  // "HH" for 24-hour format, "hh" for 12-hour format

        TimeSpan timeSpan = endTime.Subtract(startTime);
        // Get the difference in seconds
        double seconds = timeSpan.TotalSeconds;

        double avg = amountOfFrames / seconds;

        // Put in txt file
        // Write average FPS to a text file
        string filePath = Path.Combine(Application.persistentDataPath, "average_fps.txt");
        string averageText = "Current scene: " + SceneManager.GetActiveScene().name + " PlayerName: " + PlayerPrefs.GetString("character") +
            " Average FPS"  + ": " + _currentAveraged.ToString() + " timestamp: " + formattedTime + " avg:" + avg + " frames:" + amountOfFrames + " seconds: " + seconds + "\n";
        if (!File.Exists(filePath))
        {
            Debug.Log("don't exist");
            File.WriteAllText(filePath, averageText);
        }
        else
        {
            Debug.Log("exist");
            File.AppendAllText(filePath, averageText);
        }
        
    }
}
