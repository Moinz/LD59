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
        
        if (_lastMarker is "Squawk")
        {
            constraint.Init();
            
            foreach (var goal in goals)
            {
                goal.Hide();
            }
        }

        if (_lastMarker is "Idle" or "Bye" or "BossDefeat")
            constraint.Clear();

        if (_lastMarker is "Whiteout")
            gameObject.SetActive(false);
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