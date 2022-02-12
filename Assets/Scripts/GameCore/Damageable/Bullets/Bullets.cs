using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace GameCore
{
    public abstract class Bullets : MonoBehaviour, IObjectPoolable
    {
        [SerializeField] protected float _minResponceDistance;
        [SerializeField] protected float _smoothMoveSpeed;

        protected float _damage;
        protected EDamageType _damageType;
        protected float _bulletSpeed;
        protected IDamageable _damageable;

        protected Transform _ownerTransform;
        protected IPoolObjects _poolObjects;

        public void DequeueObjectFromPool()
        {
            gameObject.SetActive(true);
        }

        public void InitPoolObject(IPoolObjects poolObjects)
        {
            _poolObjects = poolObjects;
            _ownerTransform = GetComponent<Transform>();
            _minResponceDistance *= _minResponceDistance;
        }

        public virtual void InitializeBullet(float damage, EDamageType damageType, float bulletSpeed, IDamageable damageable)
        {
            _damage = damage;
            _damageType = damageType;
            _bulletSpeed = bulletSpeed;
            _damageable = damageable;

            StartCoroutine(MoveToTargetUpdate());
        }

        private IEnumerator MoveToTargetUpdate()
        {
            var distanceToTarget = GetDistanceToTarget();

            while (distanceToTarget > _minResponceDistance)
            {
                _ownerTransform.position += (GetDirectionToTarget() / distanceToTarget) * _bulletSpeed * Time.deltaTime;

                yield return null;

                distanceToTarget = GetDistanceToTarget();
            }

            ApplyDamage();
            _poolObjects.ReturnObjectToPool(this);
        }

        private float GetDistanceToTarget()
        {
            return GetDirectionToTarget().magnitude;
        }

        private Vector3 GetDirectionToTarget()
        {
            return _damageable.GetTargetPosition() - _ownerTransform.position;
        }

        public virtual void ApplyDamage()
        {
            _damageable.ApplyDamage(_damage, _damageType);
        }

        public void EnqueueObjectPool()
        {
            gameObject.SetActive(false);
            StopAllCoroutines();
            _ownerTransform.localPosition = Vector3.zero;
        }

        public GameObject GetGameObject()
        {
            return gameObject;
        }
    }
}
