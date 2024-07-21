using System;
using UnityEngine;
using UnityEngine.UI;

namespace BHSCamp.UI
{
    public class HealthBar : MonoBehaviour
    {
        [SerializeField] private Health _healthComponent;
        [SerializeField] private Image _fillImage;
        private bool _parentIsUI;

        private void OnEnable()
        {
            _healthComponent.OnDamageTaken += UpdateHealthBar;
            _healthComponent.OnHealed += UpdateHealthBar;
        }

        private void OnDisable()
        {
            _healthComponent.OnDamageTaken -= UpdateHealthBar;
            _healthComponent.OnHealed -= UpdateHealthBar;
        }

        private void Start()
        {
            _parentIsUI = transform.parent.GetComponent<RectTransform>() != null;
        }

        private void UpdateHealthBar(int healthChange)
        {
            SetFill(
                (float)_healthComponent.CurrentHealth /
                _healthComponent.MaxHealth
            ); 
        }

        private void SetFill(float value)
        {
            _fillImage.fillAmount = Mathf.Clamp01(value);
        }

        private void Update()
        {
            if (_parentIsUI) return;
            
            transform.localScale = new Vector3(
                transform.parent.localScale.x,
                transform.localScale.y,
                transform.localScale.z
            );
        }
    }
}
