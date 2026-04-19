using System;
using FMODUnity;
using Seagulls;
using UnityEngine;

public class Target : MonoBehaviour
{
    public Observable<bool> _isHovered = new(false);

    [SerializeField]
    private EventReference hoverSound;
    
    [SerializeField]
    private EventReference unhoverSound;
    
    [SerializeField]
    private EventReference clickSound;

    public event Action OnClick;

    public void Hover()
    {
        _isHovered.SetValue(true);

        if (hoverSound.IsNull)
            return;
        
        RuntimeManager.PlayOneShot(hoverSound);
    }
    
    public void UnHover()
    {
        _isHovered.SetValue(false);
        
        if (unhoverSound.IsNull)
            return;
        
        RuntimeManager.PlayOneShot(unhoverSound);
    }

    public void Click()
    {
        OnClick?.Invoke();
        
        if (clickSound.IsNull)
            return;
        
        RuntimeManager.PlayOneShot(clickSound);
    }
}