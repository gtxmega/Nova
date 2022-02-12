using UnityEngine;

namespace GameCore
{
    public interface IPoolObjects
    {
        void CreatePoolObjects(GameObject objectPoolable, int count);
        void AddToPool(GameObject objectPoolable);
        IObjectPoolable GetAvailableObjectFromPool();
        T GetAvailableObjectFromPool<T>();
        void ReturnObjectToPool(IObjectPoolable objectPoolable);
    }
}
