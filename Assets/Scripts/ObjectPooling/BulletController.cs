using System;
using UnityEngine;

namespace ObjectPooling
{
    public class BulletController : MonoBehaviour
    {
        [SerializeField] private ObjectPool _pool;
        [SerializeField] private Transform _parent;
        private void Start()
        {
            _pool.Initialize(_parent);
        }
    }
}
