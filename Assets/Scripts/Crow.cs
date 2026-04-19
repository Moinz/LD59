using TriInspector;
using UnityEngine;

public class Crow : MonoBehaviour
{
    [SerializeField]
    private GameObject _crowParent;
    
    [SerializeField]
    private SpriteRenderer _legLeft;
    
    [SerializeField]
    private SpriteRenderer _legRight;

    [SerializeField]
    private SpriteRenderer _wing;
    
    [SerializeField]
    private SpriteRenderer _beak;

    [SerializeField]
    private SpriteRenderer _hat00;

    [SerializeField]
    private SpriteRenderer _hatSquawk00;
    
    [SerializeField]
    private SpriteRenderer _hatSquawk01;
    
    [SerializeField]
    private Sprite _defaultLeg, _squawkLeg;

    [SerializeField]
    private Sprite _defaultWing, _squawkWing;
    
    [SerializeField]
    private Sprite[] _beaks;
    
    private bool _isActivated;
    
    private void Start()
    {
        MusicManager.markerUpdated += OnMarkerUpdated;
    }
    
    private void OnDestroy()
    {
        MusicManager.markerUpdated -= OnMarkerUpdated;
    }

    private void OnMarkerUpdated()
    {
        if (MusicManager.lastMarkerString == "Boss")
            Activate();
        
        if (MusicManager.lastMarkerString == "Bye")
            Deactivate();
        
        if (MusicManager.lastMarkerString == "Squawk")
            Squawk();
        
        if (MusicManager.lastMarkerString == "Idle")
            Idle();
    }

    private void Activate()
    {
        _isActivated = true;
        _crowParent.SetActive(true);

        Game.state = Game.GameState.Boss;
    }

    private void Deactivate()
    {
        _isActivated = false;
        _crowParent.SetActive(false);
        
        Game.state = Game.GameState.Playing;
    }

    [Button]
    private void Idle()
    {
        _legLeft.sprite = _defaultLeg;
        _legLeft.flipX = false;
        _legRight.sprite = _defaultLeg;
        _wing.sprite = _defaultWing;
        
        _beak.sprite = null;
        
        _hat00.enabled = true;
        _hatSquawk00.enabled = false;
        _hatSquawk01.enabled = false;

        Game.state = Game.GameState.Boss;
    }

    [Button]
    private void Squawk()
    {
        _legLeft.sprite = _squawkLeg;
        _legLeft.flipX = true;
        _legRight.sprite = _squawkLeg;
        _wing.sprite = _squawkWing;
        
        _beak.sprite = _beaks.GetRandomEntry();
        
        _hat00.enabled = false;
        _hatSquawk00.enabled = true;
        _hatSquawk01.enabled = true;
        
        Game.state = Game.GameState.BossAttack;
    }
}