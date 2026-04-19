using Shapes;
using UnityEngine;

public class Constraint : MonoBehaviour
{
    [SerializeField]
    private GameObject visuals;

    [SerializeField]
    private Disc _disc;
    
    [SerializeField]
    private Vector2 _radiusRange = new(20f, 0.8f);
    
    private float _rotationSpeed = 50;
    private float _varianceMagnitude = 10f;
    private float _shrinkDuration = 2f;
    private float _rotationMagnitude = 1f;

    private float _shrinkValue;
    
    public void Init()
    {
        visuals.SetActive(true);
        gameObject.transform.parent = null;
        
        var rotationSpeedModifier = _rotationSpeed * _varianceMagnitude;
        _rotationSpeed = Random.Range(_rotationSpeed - rotationSpeedModifier, _rotationSpeed + rotationSpeedModifier);

        _shrinkValue = 0f;
    }
    
    public void Clear()
    {
        visuals.SetActive(false);
        gameObject.transform.parent = PlayerCursor.Instance.transform;
    }
    
    private void Update()
    {
        if (!visuals.gameObject.activeSelf)
            return;
        
        _shrinkValue += Time.deltaTime * (1f / _shrinkDuration);
        
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * _rotationMagnitude);
        _disc.Radius = Mathf.Lerp(_radiusRange.x, _radiusRange.y, _shrinkValue);
    }
}