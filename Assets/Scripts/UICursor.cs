using UnityEngine;
using UnityEngine.InputSystem;

public class UICursor : MonoBehaviour
{
    private void LateUpdate()
    {
        transform.position = Mouse.current.position.ReadValue();
    }
}