using UnityEngine;

public class EnvironmentTrack : MonoBehaviour
{
    [SerializeField, Range(0f, 1f)]
    private float normalizedProgress;
    
    [SerializeField]
    private float moveSpeed = 0.05f;

    private void OnValidate()
    {
        SetPosition();  
    }
    
    private void Update()
    {
        normalizedProgress += moveSpeed * Time.deltaTime;
     
        SetPosition();
    }

    private void SetPosition()
    {
        normalizedProgress = Mathf.Clamp01(normalizedProgress);
        var zPos = Mathf.Lerp(EnvironmentTrackManager.MapRange.x, EnvironmentTrackManager.MapRange.y, normalizedProgress);
        transform.localPosition = new Vector3(0f, 0f, zPos);
    }
}