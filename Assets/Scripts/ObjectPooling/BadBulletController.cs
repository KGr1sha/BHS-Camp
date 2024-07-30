using System.Collections;
using UnityEngine;

namespace ObjectPooling
{
    public class BadBulletController : MonoBehaviour
    {
        [SerializeField] private Bullet _bulletPrefab; 
        [SerializeField] private Transform _parent;
        [SerializeField] private float _shootTimeout;
        [SerializeField] private int _bulletAmount;
        
        private void Start()
        {
            StartCoroutine(ShootOnTimer());
        }
        
        private IEnumerator ShootOnTimer()
        {
            Shoot();
            yield return new WaitForSeconds(_shootTimeout);
            StartCoroutine(ShootOnTimer());
        }

        private void Shoot()
        {
            for (int i = 0; i < _bulletAmount; i++)
            {
                Vector2 randomDir = Vector2.zero;
                while (randomDir == Vector2.zero)
                    randomDir = new(
                        Random.Range(-1f, 1f),
                        Random.Range(-1f, 1f)
                    );
                
                Bullet bullet = Instantiate(_bulletPrefab, _parent);
                bullet.OnBulletHit += DestroyBullet;
                bullet.SetDirection(randomDir.normalized);
            }
        }
        
        private void DestroyBullet(Bullet bullet)
        {
            Destroy(bullet.gameObject);
        }
    }
}