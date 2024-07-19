using UnityEngine;

namespace BHSCamp.UI
{
    public class MenuSlide : MonoBehaviour
    {
        [SerializeField] private RectTransform _slideBegin;
        [SerializeField] private RectTransform _slideEnd;
        [SerializeField] private AnimationCurve _curve;
        [SerializeField] private float _slideTime;
        private RectTransform _rectTransform;
        private float _time;
        private bool _move;

        private void Awake()
        {
            _rectTransform = GetComponent<RectTransform>();
        }

        private void Start()
        {
            Slide();
        }
        
        public void Slide()
        {
            _rectTransform.position = _slideBegin.position;
            _move = true;
        }

        private void Update()
        {
            if (false == _move) return;
            _time += Time.deltaTime;
            float t = Mathf.Clamp01(_time / _slideTime);
            _rectTransform.position = Vector3.Lerp(
                _slideBegin.position,
                _slideEnd.position,
               _curve.Evaluate((t))
            );
            if (1 <= t)
                _move = false;
        }
    }
}