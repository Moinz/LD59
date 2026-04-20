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
        var isPositive = Mathf.Sign(score) > 0;
        var scoreString = isPositive ? "+" + score : score.ToString();
        
        _textMeshProUGUI.text = scoreString;
        _rectTransform.sizeDelta = new Vector2(_textMeshProUGUI.preferredWidth, _textMeshProUGUI.preferredHeight);
        
        _textMeshProUGUI.color = isPositive ? Color.green : Color.red;
        _startTime = Time.time;
    }
    
    private void Update()
    {
        if (Time.time - _startTime > _lifeTime)
            Destroy(gameObject);
        
        _rectTransform.localPosition += Vector3.up * (Time.deltaTime * 100);
    }
}