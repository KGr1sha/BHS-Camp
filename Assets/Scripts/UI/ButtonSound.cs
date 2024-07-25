using System;
using UnityEngine;
using UnityEngine.UI;

namespace BHSCamp
{
    public class ButtonSound : MonoBehaviour
    {
        public static event Action<AudioClip> OnButtonPressed;
        [SerializeField] private AudioClip _clickSound;
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void Start()
        {
            _button.onClick.AddListener(
                () => OnButtonPressed?.Invoke(_clickSound)
            );
        }
    }
}