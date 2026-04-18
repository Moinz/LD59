using System;
using Shapes;
using UnityEngine;
using Random = UnityEngine.Random;

public class Goal : MonoBehaviour
{
    public float _rotationSpeed;
    public float _varianceMagnitude;

    public Disc indicatorDisc;
    public Vector2 radiusRange = new(1.6f, 0.8f);

    [SerializeField]
    private Target _target;

    private float _rotationMagnitude = 1f;
    private float currentShrinkDuration;
    public float shrinkRatio = 0.5f;
    public float bpm = 120f;

    private bool isAnimating;
    private float startTime;

    private void OnEnable()
    {
        startTime = Time.time;
        isAnimating = true;
        
        float beatInterval = 60f / bpm;
        currentShrinkDuration = beatInterval * shrinkRatio;
        
        indicatorDisc.Radius = radiusRange.x;
    }

    private void Update()
    {
        if (!isAnimating)
            return;
        
        float elapsed = Time.time - startTime;
        
        var progress = elapsed / currentShrinkDuration;
        var newRadius = Mathf.SmoothStep(radiusRange.x, radiusRange.y, progress);
        indicatorDisc.Radius = newRadius;

        if (progress >= 1.2f)
        {
            gameObject.SetActive(false);
            isAnimating = false;
        }
    }
}