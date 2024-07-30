using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.PlayerLoop;

namespace ObjectPooling
{
    public class ObjectPool : MonoBehaviour
    {
        [SerializeField] private GameObject _object;
        [SerializeField] private int _capacity;
        private Transform _parent;
        private Queue<GameObject> _objects;

        public void Initialize(Transform parent)
        {
            _parent = parent;
            _objects = new Queue<GameObject>(_capacity);
            CreateInstances();
        }

        public GameObject GetFromPool()
        {
            var poolObject = _objects.Dequeue();
            poolObject.SetActive(true);
            return poolObject;
        }
        
        public void ReturnToPool(GameObject poolObject)
        {
            poolObject.SetActive(false);
            _objects.Enqueue(poolObject);
        }
        
        public GameObject Add()
        {
            var newObject = Instantiate(_object, _parent);
            newObject.SetActive(false);
            return newObject;
        }

        private void CreateInstances()
        {
            for (int i = 0; i < _capacity; i++)
            {
                var newObject = Instantiate(_object, _parent);
                newObject.SetActive(false);
                _objects.Enqueue(newObject);
            }
        }
    }
}
