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
            if (_objects.Count == 0)
            {
                GameObject newObject = CreateObject();
                return newObject;
            }
            
            var poolObject = _objects.Dequeue();
            poolObject.SetActive(true);
            return poolObject;
        }
        
        public void ReturnToPool(GameObject poolObject)
        {
            poolObject.SetActive(false);
            _objects.Enqueue(poolObject);
        }
        
        private GameObject CreateObject()
        {
            return Instantiate(_object, _parent);
        }

        private void CreateInstances()
        {
            for (int i = 0; i < _capacity; i++)
            {
                var newObject = CreateObject();
                newObject.SetActive(false);
                _objects.Enqueue(newObject);
            }
        }
    }
}
