using UnityEngine;
using Random = UnityEngine.Random;

public class Goal : MonoBehaviour
{
    public float _rotationSpeed;
    public float _varianceMagnitude;

    [SerializeField]
    private Hoverable _hoverable;

    private float _rotationMagnitude = 1f;
    
    private void Start()
    {
        var rotationSpeedModifier = _rotationSpeed * _varianceMagnitude;
        _rotationSpeed = Random.Range(_rotationSpeed - rotationSpeedModifier, _rotationSpeed + rotationSpeedModifier);

        _hoverable._isHovered.OnChanged += OnHovered;
    }

    private void OnDestroy()
    {
        _hoverable._isHovered.OnChanged -= OnHovered;
    }

    private void OnHovered(bool isHovered)
    {
        Debug.Log("Hovered: " + isHovered);
        _rotationMagnitude = isHovered ? 2f : 1f;
    }

    private void Update()
    {
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * _rotationMagnitude);
    }
}