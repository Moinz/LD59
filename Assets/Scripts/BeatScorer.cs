using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class BeatScorer : MonoBehaviour
{
    public TextMeshProUGUI _scoreTextMesh;
    public int score = 100;
    
    [SerializeField]
    private float perfectWindow = 0.05f;
    
    [SerializeField]
    private float goodWindow = 0.10f;
    
    [SerializeField]
    private float earlyThreshold = 0.08f;
    
    private float _lastBeatTime = -100f;
    private Queue<float> _beatTimes = new();

    public static bool IsInsideConstraint;

    public ScoreIndicator prefab;
    public Canvas canvas;
    
    private void Start()
    {
        PlayerCursor.PlayerClickGoal += OnPlayerClickGoal;
        PlayerCursor.PlayerClickMiss += OnPlayerClickMiss;
        MusicManager.beatUpdated += OnBeat;
    }

    private void OnPlayerClickMiss()
    {
        RegisterScore(-5);
    }

    private void OnPlayerClickGoal()
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
            score += 5;
            return;
        }
        
        if (minDelta <= perfectWindow) 
            RegisterScore(20);
        else if (minDelta <= goodWindow) 
            RegisterScore(10);
        
        else RegisterScore(5);
    }
    
    private void RegisterScore(int scoreToAdd)
    {
        score += scoreToAdd;
        _scoreTextMesh.text = score.ToString();
        
        var instantiatedScore = Instantiate(prefab, canvas.transform);
        instantiatedScore.Init(scoreToAdd);
        instantiatedScore.transform.position = Camera.main.WorldToScreenPoint(PlayerCursor.Instance.transform.position);
        instantiatedScore.gameObject.SetActive(true);
    }

    private void OnBeat()
    {
        if (!IsInsideConstraint)
            RegisterScore(-10);
        
        _beatTimes.Enqueue(Time.time);
        
        if (_beatTimes.Count > 10)
            _beatTimes.Dequeue();
    }
}