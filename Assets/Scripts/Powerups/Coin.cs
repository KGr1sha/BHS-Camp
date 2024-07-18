using UnityEngine;

namespace BHSCamp
{
    public class Coin : MonoBehaviour, IPowerup 
    {
        [SerializeField] private int _scoreToAdd;

        public void Apply(GameObject target)
        {
            GameManager.Instance.AddScore(_scoreToAdd);
        }
    }
}