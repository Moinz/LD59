using System;
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

        var color = isPositive ? Color.white : Color.red;
        if (score == 15)
        {
            color = Color.green;
            transform.localScale = new Vector3(1.5f, 1.5f, 1.5f);
        }
        if (score == 10)
        {
            transform.localScale = new Vector3(1.2f, 1.2f, 1.2f);
            color = Color.yellow;
        }

        _textMeshProUGUI.color = color;
        _startTime = Time.time;
    }
    
    private void Update()
    {
        if (Time.time - _startTime > _lifeTime)
            Destroy(gameObject);
        
        _rectTransform.localPosition += Vector3.up * (Time.deltaTime * 100);
    }
}