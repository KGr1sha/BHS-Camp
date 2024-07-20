using UnityEngine;

namespace BHSCamp
{
    public class Coin : MonoBehaviour, IPowerup 
    {
        [SerializeField] private int _scoreToAdd;

        public void Apply(GameObject target)
        {
            if (false == target.GetComponent<Collider2D>().isTrigger) //чтобы монетки не собирались триггером атаки
                GameManager.Instance.AddScore(_scoreToAdd);
        }
    }
}