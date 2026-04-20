using System;
using System.Collections;
using Shapes;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class Constraint : MonoBehaviour
{
    [SerializeField]
    private GameObject visuals;

    [SerializeField]
    private Image _constraintIndicator;

    [SerializeField]
    private Disc _disc;
    
    [SerializeField]
    private Vector2 _radiusRange = new(20f, 0.8f);

    [SerializeField]
    private CircleCollider2D _circleCollider;

    [SerializeField]
    private Target _target;
    
    private float _rotationSpeed = 50;
    private float _varianceMagnitude = 10f;
    private float _shrinkDuration = 2f;
    private float _rotationMagnitude = 1f;

    private float _shrinkValue;
    
    public void Init()
    {
        visuals.SetActive(true);
        
        var rotationSpeedModifier = _rotationSpeed * _varianceMagnitude;
        _rotationSpeed = Random.Range(_rotationSpeed - rotationSpeedModifier, _rotationSpeed + rotationSpeedModifier);

        transform.position = PlayerCursor.Instance.transform.position;
        
        _shrinkValue = 0f;
        _circleCollider.enabled = true;
    }
    
    public void Clear()
    {
        visuals.SetActive(false);
        _circleCollider.enabled = false;
    }
    
    private void Update()
    {
        IsInsideConstraint();
            
        if (!visuals.gameObject.activeSelf)
            return;
        
        _shrinkValue += Time.deltaTime * (1f / _shrinkDuration);
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * _rotationMagnitude);
        
        var radius = Mathf.Lerp(_radiusRange.x, _radiusRange.y, _shrinkValue);
        
        _disc.Radius = radius;
        _circleCollider.radius = radius;
    }
    
    private void IsInsideConstraint()
    {
        bool isInside = false;
        bool isNotActive = !visuals.gameObject.activeSelf;
        
        var distanceToCursor = Vector2.Distance(transform.position, PlayerCursor.Instance.transform.position);
        
        if (isNotActive)
            isInside = true;
        else
            isInside = distanceToCursor <= _disc.Radius;

        BeatScorer.IsInsideConstraint = isInside;
        
        var color = _constraintIndicator.color;
        if (isInside)
            color.a = 0f;
        else
            color.a = 0.1f;
        _constraintIndicator.color = color;
    }
}