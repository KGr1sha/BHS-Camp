using UnityEngine;

namespace BHSCamp
{
    [RequireComponent(typeof(AudioSource))]
    public class ButtonSoundPlayer : MonoBehaviour
    {
        [SerializeField] AudioSource _audioSource;

        private void OnEnable()
        {
            ButtonSound.OnButtonPressed += PlaySound;
        }
        
        private void OnDisable()
        {
            ButtonSound.OnButtonPressed -= PlaySound;
        }

        private void PlaySound(AudioClip sound)
        {
            _audioSource.PlayOneShot(sound);
        }
    }
}