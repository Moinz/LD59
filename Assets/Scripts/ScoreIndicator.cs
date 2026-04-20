using TMPro;
using UnityEngine;

public class ScoreIndicator : MonoBehaviour
{
    [SerializeField]
    private RectTransform _rectTransform;

    [SerializeField]
    private TextMeshProUGUI _textMeshProUGUI;
    
    [SerializeField]
    private float _lifeTime = 1f;

    private float _startTime;
    public void Init(int score)
    {
        _textMeshProUGUI.text = score.ToString();
        _rectTransform.sizeDelta = new Vector2(_textMeshProUGUI.preferredWidth, _textMeshProUGUI.preferredHeight);
        
        _textMeshProUGUI.color = Mathf.Sign(score) > 0 ? Color.green : Color.red;
        
        _startTime = Time.time;
    }
    
    private void Update()
    {
        if (Time.time - _startTime > _lifeTime)
            Destroy(gameObject);
        
        _rectTransform.localPosition += Vector3.up * (Time.deltaTime * 100);
    }
}