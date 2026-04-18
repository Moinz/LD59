using FMODUnity;
using Seagulls;
using UnityEngine;

public class Hoverable : MonoBehaviour
{
    public Observable<bool> _isHovered = new(false);

    [SerializeField]
    private EventReference hoverSound;
    
    [SerializeField]
    private EventReference unhoverSound;

    public void Hover()
    {
        _isHovered.SetValue(true);

        if (hoverSound.IsNull)
            return;
        
        //RuntimeManager.PlayOneShot(hoverSound);
    }
    
    public void UnHover()
    {
        _isHovered.SetValue(false);
        
        if (unhoverSound.IsNull)
            return;
        
        //RuntimeManager.PlayOneShot(unhoverSound);
    }
}