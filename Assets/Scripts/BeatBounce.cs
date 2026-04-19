using System.Collections.Generic;
using DG.Tweening;
using TriInspector;
using UnityEngine;

public class BeatBounce : MonoBehaviour
{
    [SerializeField]
    private float _scale = 0.1f;
    
    [SerializeField]
    private float _duration = 0.05f;
    
    [SerializeField]
    private int _vibrato = 4;
    
    [SerializeField]
    private float _elasticity = 0.5f;

    [SerializeField]
    private bool _specificBeats;
    
    [ShowIf(nameof(_specificBeats))]
    public List<int> beatsToReactTo;
    
    private void Start()
    {
        MusicManager.beatUpdated += OnBeatUpdated;
    }

    private void OnDestroy()
    {
        MusicManager.beatUpdated -= OnBeatUpdated;
    }

    private void OnBeatUpdated()
    {
        if (Game.state == Game.GameState.BossAttack)
            return;
        
        if (_specificBeats && !beatsToReactTo.Contains(MusicManager.lastBeat))
            return;
        
        transform.DOPunchScale(Vector3.one * _scale, _duration, _vibrato, _elasticity);
    }
}