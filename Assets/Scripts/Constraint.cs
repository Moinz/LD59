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
    private Vector2 _scaleRange = new(30f, 1f);

    [SerializeField]
    private PolygonCollider2D _coliider;

    [SerializeField]
    private Target _target;
    
    private float _rotationSpeed = 15;
    private float _varianceMagnitude = 10f;
    private float _shrinkDuration = 1f;
    private float _rotationMagnitude = 1f;

    private float _shrinkValue;

    private void Start()
    {
        var rotationSpeedModifier = _rotationSpeed * _varianceMagnitude;
        _rotationSpeed = Random.Range(_rotationSpeed - rotationSpeedModifier, _rotationSpeed + rotationSpeedModifier);
    }
    
    public void Init()
    {
        visuals.SetActive(true);

        transform.position = PlayerCursor.Instance.transform.position;
        
        _shrinkValue = 0f;
        _coliider.enabled = true;
    }
    
    public void Clear()
    {
        visuals.SetActive(false);
        _coliider.enabled = false;
    }
    
    private void Update()
    {
        IsInsideConstraint();
            
        if (!visuals.gameObject.activeSelf)
            return;
        
        _shrinkValue += Time.deltaTime * (1f / _shrinkDuration);
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * _rotationMagnitude);
        
        var scale = Mathf.Lerp(_scaleRange.x, _scaleRange.y, _shrinkValue);

        transform.localScale = Vector3.one * scale;
    }
    
    private void IsInsideConstraint()
    {
        bool isInside = false;
        bool isNotActive = !visuals.gameObject.activeSelf;


        var hits = Physics2D.OverlapPointAll(PlayerCursor.Instance.transform.position);

        foreach (var colliders in hits)
        {
            if (colliders == _coliider)
            {
                isInside = true;
                break;
            }
        }

        if (isNotActive)
            isInside = true;
        
        BeatScorer.IsInsideConstraint = isInside;
        
        var color = _constraintIndicator.color;
        if (isInside)
            color.a = 0f;
        else
            color.a = 0.1f;
        _constraintIndicator.color = color;
    }
}