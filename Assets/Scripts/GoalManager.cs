using UnityEngine;

public class GoalManager : MonoBehaviour
{
    [SerializeField]
    private Goal[] goals;

    [SerializeField]
    private GameObject crow;
    
    [SerializeField]
    private Constraint constraint;

    private string _lastMarker;
    
    private void Start()
    {
        MusicManager.beatUpdated += OnBeatUpdated;
        MusicManager.markerUpdated += OnMarkerUpdated;
    }

    private void OnMarkerUpdated()
    {
        _lastMarker = MusicManager.lastMarkerString;

        if (_lastMarker == "Boss")
        {
            crow.SetActive(true);
            constraint.Init();
        }
        
        if (_lastMarker == "Start")
        {
            crow.SetActive(false);
            constraint.Clear();
        }
    }

    private void OnBeatUpdated()
    {
        if (MusicManager.lastBeat % 2 == 0)
            goals.GetRandomEntry().gameObject.SetActive(true);
    }
}