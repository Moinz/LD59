using UnityEngine;

public class GoalManager : MonoBehaviour
{
    private Goal[] goals;

    [SerializeField]
    private GameObject crow;
    
    [SerializeField]
    private Constraint constraint;

    private string _lastMarker;
    private bool inBoss;
    
    private void Start()
    {
        MusicManager.beatUpdated += OnBeatUpdated;
        MusicManager.markerUpdated += OnMarkerUpdated;
        
        goals = GetComponentsInChildren<Goal>(true);
    }

    private void OnMarkerUpdated()
    {
        _lastMarker = MusicManager.lastMarkerString;
        
        if (_lastMarker == "Squawk")
        {
            constraint.Init();
            
            foreach (var goal in goals)
            {
                goal.Hide();
            }
        }

        if (_lastMarker == "Idle")
        {
            constraint.Clear();
        }
    }

    private void OnBeatUpdated()
    {
        if (Game.state == Game.GameState.BossAttack)
            return;
        
        var randomEntry = goals.GetRandomEntry();
        var maxAttempts = 10;
        
        while (randomEntry.IsShown && maxAttempts > 0)
        {
            maxAttempts--;
            randomEntry = goals.GetRandomEntry();
        }
        
        goals.GetRandomEntry().Show();
    }
}