using System.Collections;
using UnityEngine;

namespace ObjectPooling
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private Transform _parent;
        [SerializeField] private float _shootTimeout;
        [SerializeField] private int _bulletAmount;
        
    
        private void Start()
        {
            _pool.Initialize(_parent);
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
                
                Bullet bullet = _pool.GetFromPool().GetComponent<Bullet>();
                bullet.OnBulletHit += DisableBullet;
                bullet.SetDirection(randomDir.normalized);
            }
        }

        private void DisableBullet(Bullet bullet)
        {
            bullet.OnBulletHit -= DisableBullet;
            bullet.transform.position = transform.position;
            bullet.SetDirection(Vector2.zero);
            _pool.ReturnToPool(bullet.gameObject);
        }
    }
}
