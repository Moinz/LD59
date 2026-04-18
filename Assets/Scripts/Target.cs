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
        Debug.Log("Clicked");
        gameObject.SetActive(false);
        
        if (clickSound.IsNull)
            return;
        
        RuntimeManager.PlayOneShot(clickSound);
    }
    
    
}