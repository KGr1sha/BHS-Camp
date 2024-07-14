using UnityEngine;

namespace BHSCamp
{
    public class MeleeAttack : MonoBehaviour, IAttack
    {
        [SerializeField] private int _damage;
        [SerializeField] private BoxCollider2D _hitCollider;
        [SerializeField] private LayerMask _layerToHit;
        [SerializeField] private int _maxTargets;

        public void Action()
        {
            if (_maxTargets == 0)
                Debug.LogError($"Max targets is set to 0: {gameObject.name}");

            ContactFilter2D filter = new();
            filter.useTriggers = true; 
            filter.layerMask = _layerToHit;
            Collider2D[] colliders = new Collider2D[_maxTargets];

            int collidersCount = Physics2D.OverlapCollider(_hitCollider, filter, colliders);
            for(int i = 0; i < collidersCount; i++)
            {
                IDamageable damageable = colliders[i].GetComponent<IDamageable>();
                damageable.TakeDamage(_damage);
            }
        }
    }
}