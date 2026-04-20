using DG.Tweening;
using UnityEngine;

public class Warning : MonoBehaviour
{
    [SerializeField]
    private CanvasGroup _canvasGroup;
    
    private void Start()
    {
        MusicManager.markerUpdated += OnMarkerUpdated;
    }

    private void OnMarkerUpdated()
    {
        if (MusicManager.lastMarkerString == "Squawk")
            BlinkWarning();
    }

    private void BlinkWarning()
    {
        var sequence = DOTween.Sequence();

        var interval = 0.5f;
        sequence.Append(_canvasGroup.DOFade(1f, interval));
        sequence.AppendInterval(interval * 2f);
        sequence.Append(_canvasGroup.DOFade(0f, interval));

        sequence.Play();
    }
}