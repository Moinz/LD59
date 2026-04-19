using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private Sprite[] sprites;

    private bool party;
    
    private Sprite _lastSprite;
    
    private void Start()
    {
        PlayerCursor.playerClick += OnPlayerClick;
        
        MusicManager.markerUpdated += OnMarkerUpdated;
        MusicManager.beatUpdated += OnBeatUpdated;
    }

    private void OnMarkerUpdated()
    {
        if (MusicManager.lastMarkerString == "Party")
            party = true;
    }

    private void OnBeatUpdated()
    {
        if (party)
            GetUniqueSprite();
    }
    
    private void OnDestroy()
    {
        PlayerCursor.playerClick -= OnPlayerClick;
    }
    
    private void OnPlayerClick()
    {
        GetUniqueSprite();
    }

    private void GetUniqueSprite()
    {
        var sprite = spriteRenderer.sprite;
        
        while (sprite == _lastSprite)
            sprite = sprites.GetRandomEntry();
        
        _lastSprite = sprite;
        spriteRenderer.sprite = sprite;
    }
}