using System.Collections.Generic;
using UnityEngine;

public class BeatScorer : MonoBehaviour
{
    [SerializeField]
    private float perfectWindow = 0.05f;
    
    [SerializeField]
    private float goodWindow = 0.10f;
    
    [SerializeField]
    private float earlyThreshold = 0.08f;

    private float _lastBeatTime = -100f;
    private Queue<float> _beatTimes = new();
    
    private void Start()
    {
        PlayerCursor.playerClick += OnPlayerClick;
        MusicManager.beatUpdated += OnBeat;
    }

    private void OnPlayerClick()
    {
        float inputTime = Time.time;
        
        float closestBeat = -1f;
        float minDelta = float.MaxValue;

        foreach (float beat in _beatTimes)
        {
            float delta = Mathf.Abs(inputTime - beat);
            if (delta < minDelta)
            {
                minDelta = delta;
                closestBeat = beat;
            }
        }

        if (closestBeat == -1f) 
            return; // No beats recorded yet
        
        if (inputTime < (closestBeat - earlyThreshold))
        {
            Debug.Log("Miss.");
            return;
        }
        
    }

    private void OnBeat()
    {
        _beatTimes.Enqueue(Time.time);
        
        if (_beatTimes.Count > 10)
            _beatTimes.Dequeue();
    }
}