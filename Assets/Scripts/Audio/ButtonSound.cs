using System;
using UnityEngine;
using UnityEngine.UI;

public class ButtonSound : MonoBehaviour
{
    public static event Action<AudioClip> OnButtonClicked;
    [SerializeField] private AudioClip _clickSound;
    private Button _button;
    
    private void Awake()
    {
        _button = GetComponent<Button>();
        _button.onClick.AddListener(
            () => OnButtonClicked?.Invoke(_clickSound)
        );
    }
}
