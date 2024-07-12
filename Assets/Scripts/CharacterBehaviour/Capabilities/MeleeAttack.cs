using UnityEngine;

public class MeleeAttack : MonoBehaviour, IAttack
{
    [SerializeField] private int _damage;

    public void Attack()
    {
        //box cast (dont forget layer mask)
        //find idamageable
        //for .. takeDamage()
    }
}