using System;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCursor : MonoBehaviour
{
    public static PlayerCursor Instance;
    
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float worldZ;

    private readonly List<Target> hoverables = new();

    public delegate void ClickHandler();
    public static event ClickHandler playerClick;
    
    private Camera mainCamera;

    private void Awake()
    {
        Instance = this;
        mainCamera = Camera.main;

        Cursor.visible = false;

        if (playerInput == null)
        {
            playerInput = FindAnyObjectByType<PlayerInput>();
        }

        if (mainCamera == null)
        {
            Debug.LogWarning("Cursor: No main camera found. Disabling cursor movement.", this);
            enabled = false;
        }
    }

    private void LateUpdate()
    {
        if (Game.state == Game.GameState.BossAttack)
        {
            foreach (var hoverable in hoverables)
                hoverable.UnHover();
            
            hoverables.Clear();
        }
        
        if (playerInput == null)
            return;

        var screenPos = playerInput.CursorPosition;
        var cameraDistance = Mathf.Abs(worldZ - mainCamera.transform.position.z);
        var cursorPos = mainCamera.ScreenToWorldPoint(new Vector3(screenPos.x, screenPos.y, cameraDistance));
        cursorPos.z = worldZ;

        transform.position = cursorPos;
        transform.rotation = Quaternion.identity;
    }

    private void Update()
    {
        if (playerInput.WasLeftButtonPressed)
        {
            if (hoverables.Count == 0)
                return;
            
            hoverables[0].Click();
            playerClick?.Invoke();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        var hoverable = otherGameObject.GetComponent<Target>();

        if (hoverable != null)
        {
            hoverable.Hover();
            hoverables.Add(hoverable);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        var hoverable = otherGameObject.GetComponent<Target>();

        if (hoverable != null)
        {
            hoverable.UnHover();
            hoverables.Remove(hoverable);
        }
    }
}