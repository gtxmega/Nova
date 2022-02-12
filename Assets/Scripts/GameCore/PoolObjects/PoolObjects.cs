using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public class PoolObjects : MonoBehaviour, IPoolObjects
    {
        [SerializeField] private Transform _parentPoolTransform;

        private Queue<IObjectPoolable> _objectsPool = new Queue<IObjectPoolable>();

        private GameObject _tempPoolObjectPrefab;

        public void AddToPool(GameObject objectPoolable)
        {
            var instanceObject = Instantiate(objectPoolable, _parentPoolTransform);
            instanceObject.SetActive(false);

            var poolObject = instanceObject.GetComponent<IObjectPoolable>();
            poolObject.InitPoolObject(this);

            _objectsPool.Enqueue(poolObject);
        }

        public void CreatePoolObjects(GameObject objectPoolable, int count)
        {
            _tempPoolObjectPrefab = objectPoolable;

            for (int i = 0; i < count; ++i)
            {
                AddToPool(objectPoolable);
            }
        }

        public IObjectPoolable GetAvailableObjectFromPool()
        {
            if(_objectsPool.Count == 0)
            {
                AddToPool(_tempPoolObjectPrefab);
            }

            var dequeueObject = _objectsPool.Dequeue();
            dequeueObject.DequeueObjectFromPool();

            return dequeueObject;
        }

        public T GetAvailableObjectFromPool<T>()
        {
            var dequeueObject = GetAvailableObjectFromPool();
            return (T)dequeueObject;
        }

        public void ReturnObjectToPool(IObjectPoolable objectPoolable)
        {
            objectPoolable.EnqueueObjectPool();
            _objectsPool.Enqueue(objectPoolable);
        }
    }
}
