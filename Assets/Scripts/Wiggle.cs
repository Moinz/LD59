using TriInspector;
using UnityEngine;

namespace Seagulls.UI
{
    [DeclareBoxGroup("Rotation")]
    [DeclareBoxGroup("Scale")]
    [DeclareBoxGroup("Position")]
    public class UIWiggle : MonoBehaviour
    {
        private bool hasRectTransform;
        public RectTransform targetRectTransform;
        
        [Group("Rotation")]
        [SerializeField]
        private bool _animateRotation;
        
        [Group("Rotation"), ShowIf("_animateRotation")]
        [SerializeField]
        private float radius = 45f;

        [Group("Rotation"), ShowIf("_animateRotation")]
        [SerializeField]
        private float rotationTimeScale = 0.7f;
        private float rotationTimeOffset;
        
        [Group("Scale")]
        [SerializeField]
        private bool _animateScale;
            
        [Group("Scale"), ShowIf("_animateScale")]
        [SerializeField]
        private float defaultScale = 1f;

        [Group("Scale"), ShowIf("_animateScale")]
        [SerializeField]
        private float scaleMagnitude = 0.1f;
        
        [Group("Scale"), ShowIf("_animateScale")]
        [SerializeField]
        private float scaleTimeScale = 0.25f;
        private float scaleTimeOffset;
        
        [Group("Position")]
        [SerializeField]        
        private bool _animatePosition;
        private float positionOffset;
        
        [Group("Position"), ShowIf("_animatePosition")]
        [SerializeField]
        private float positionTimeScale = 0.2f;
        
        [Group("Position"), ShowIf("_animatePosition")]
        [SerializeField]
        private float distance = 0.05f;

        private Vector3 originalPosition;
        
        private void Start()
        {
            targetRectTransform ??= GetComponent<RectTransform>();
            hasRectTransform = targetRectTransform != null;

            rotationTimeOffset = Random.Range(-1f, 1f);
            scaleTimeOffset = Random.Range(-1f, 1f);

            rotationTimeScale = Random.Range(rotationTimeScale - .05f, rotationTimeScale + .05f);
            scaleTimeScale = Random.Range(scaleTimeScale - .025f, scaleTimeScale + .025f);

            originalPosition = transform.localPosition;
        }

        private void Update()
        {
            if (_animateRotation)
            {
                var rotation = Mathf.Sin(Time.time * rotationTimeScale + rotationTimeOffset) * radius;
                transform.localEulerAngles = new Vector3(0, 0, rotation);
            }

            if (_animateScale)
            {
                var scale = defaultScale + (Mathf.Cos(Time.time * scaleTimeScale + scaleTimeOffset) * scaleMagnitude);
                transform.localScale = Vector3.MoveTowards(transform.localScale, Vector3.one * scale, Time.deltaTime);
            }
            
            if (!_animatePosition) 
                return;
            
            var posX = Mathf.Sin(Time.time * positionTimeScale + positionOffset) * distance;
            var posY = Mathf.Sin(Time.time * positionTimeScale * 3 + positionOffset) * distance;
            transform.localPosition = new Vector3(posX, posY, 0) +  originalPosition;
        }
    }
}