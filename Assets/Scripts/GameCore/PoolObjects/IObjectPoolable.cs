using UnityEngine;

namespace GameCore
{
    public interface IObjectPoolable
    {
        void EnqueueObjectPool();
        void DequeueObjectFromPool();
        void InitPoolObject(IPoolObjects poolObjects);
        GameObject GetGameObject();
    }
}
