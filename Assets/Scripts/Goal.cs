using DG.Tweening;
using UnityEngine;
using Random = UnityEngine.Random;

public class Goal : MonoBehaviour
{
    public float _rotationSpeed;
    public float _varianceMagnitude;

    [SerializeField]
    private Target _hoverable;

    [SerializeField]
    private Transform _visualParent;

    private float _rotationMagnitude = 1f;

    private Collider2D _collider;

    public bool IsShown;
    
    private void Start()
    {
        var rotationSpeedModifier = _rotationSpeed * _varianceMagnitude;
        _rotationSpeed = Random.Range(_rotationSpeed - rotationSpeedModifier, _rotationSpeed + rotationSpeedModifier);

        _hoverable._isHovered.OnChanged += OnHovered;
        _hoverable.OnClick += Hide;
        
        _collider = GetComponent<Collider2D>();
    }

    private void OnDestroy()
    {
        _hoverable._isHovered.OnChanged -= OnHovered;
    }

    private void OnHovered(bool isHovered)
    {
        _rotationMagnitude = isHovered ? 2f : 1f;
        
        transform.localScale = isHovered ? Vector3.one * 1.2f : Vector3.one;
    }

    private void Update()
    {
        var sign = _rotationMagnitude == 2 ? -1 : 1;
        transform.Rotate(Vector3.forward, _rotationSpeed * Time.deltaTime * _rotationMagnitude * sign);
    }

    public void Show()
    {
        _visualParent.DOScale(Vector3.one, 0.3f).SetEase(Ease.OutBounce);
        
        gameObject.SetActive(true);
        IsShown = true;
        _collider.enabled = true;
    }
    
    public void Hide()
    {
        _visualParent.DOScale(Vector3.zero, 0.3f).SetEase(Ease.OutBounce);
        
        IsShown = false;
        _collider.enabled = false;
    }
}