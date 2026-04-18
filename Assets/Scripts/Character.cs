using UnityEngine;

public class Character : MonoBehaviour
{
    [SerializeField]
    private SpriteRenderer spriteRenderer;
    
    [SerializeField]
    private Sprite[] sprites;
    
    private void Start()
    {
        PlayerCursor.playerClick += OnPlayerClick;
    }
    
    private void OnDestroy()
    {
        PlayerCursor.playerClick -= OnPlayerClick;
    }
    
    private void OnPlayerClick()
    {
        spriteRenderer.sprite = sprites.GetRandomEntry();
    }
}