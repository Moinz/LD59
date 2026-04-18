using System;
using UnityEngine;

public class Cursor : MonoBehaviour
{
    [SerializeField] private PlayerInput playerInput;
    [SerializeField] private float worldZ;

    private Camera mainCamera;

    private void Awake()
    {
        mainCamera = Camera.main;

        UnityEngine.Cursor.visible = false;

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
        if (playerInput == null)
        {
            return;
        }

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
            
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        var hoverable = otherGameObject.GetComponent<Hoverable>();
        
        if (hoverable != null)
            hoverable.Hover();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        var otherGameObject = other.gameObject;
        var hoverable = otherGameObject.GetComponent<Hoverable>();
        
        if (hoverable != null)
            hoverable.UnHover();
    }
}