using UnityEngine;

namespace BHSCamp
{
    public class Coin : PowerupBase 
    {
        [SerializeField] private int _scoreToAdd;

        public override void Apply(GameObject target)
        {
            GameManager.Instance.AddScore(_scoreToAdd);
        }
    }
}