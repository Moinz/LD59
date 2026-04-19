using UnityEngine;

public class SimpleOscillateScale : MonoBehaviour
{
    [SerializeField]
    private float _scale = 1f;
    
    [SerializeField]
    private float _scaleAmplitude = 0.1f;
    
    [SerializeField]
    private float _scaleFrequency = 1f;
    private void Update()
    {
        var scale = _scale + _scaleAmplitude * Mathf.Sin(_scaleFrequency * Time.time);
        transform.localScale = new Vector3(scale, scale, scale);
    }
}