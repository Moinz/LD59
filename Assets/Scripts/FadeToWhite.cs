using DG.Tweening;
using TriInspector;
using UnityEngine;
using UnityEngine.UI;

public class FadeToWhite : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup canvasGroup;
    
    private void Start()
    {
        MusicManager.markerUpdated += OnMarker;
    }

    private void OnDestroy()
    {
        MusicManager.markerUpdated -= OnMarker;
    }

    private void OnMarker()
    {
        if (MusicManager.lastMarkerString == "Whiteout")
            Whiteout();
        
        if (MusicManager.lastMarkerString == "Party")
            Clear();
    }

    [Button]
    private void Clear()
    {
        canvasGroup.DOFade(0f, 0.2f);
    }

    [Button]
    private void Whiteout()
    {
        canvasGroup.DOFade(1f, 0.2f);
    }
}