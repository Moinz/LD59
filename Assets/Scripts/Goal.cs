using UnityEngine;
using Random = UnityEngine.Random;

public class Goal : MonoBehaviour
{
    public float _rotationSpeed;
    public float _varianceMagnitude;

    [SerializeField]
    private Target _target;

    private float _rotationMagnitude = 1f;
    
    private void Start()
    {
        var rotationSpeedModifier = _rotationSpeed * _varianceMagnitude;
        _rotationSpeed = Random.Range(_rotationSpeed - rotationSpeedModifier, _rotationSpeed + rotationSpeedModifier);

        _target._isHovered.OnChanged += OnHovered;
    }

    private void OnDestroy()
    {
        _target._isHovered.OnChanged -= OnHovered;
    }

    private void OnHovered(bool isHovered)
    {
        _rotationMagnitude = isHovered ? 2f : 1f;
        
        transform.localScale = isHovered ? Vector3.one * 1.2f : Vector3.one;
    }

    private void Update()
    {
        var sign = _rotationMagnitude == 2 ? -1 : 1;
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * _rotationMagnitude * sign);
    }
}