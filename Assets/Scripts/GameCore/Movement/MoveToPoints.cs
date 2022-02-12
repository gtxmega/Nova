using UnityEngine;
using Zenject;

namespace GameCore.Movement
{
    public class MoveToPoints : MonoBehaviour
    {
        private Transform[] _movePoints;

        [Inject] private IMovement _movement;

        private int _currentMovePointIndex;

        private void Awake()
        {
            _movement.OnDestination += OnDestination;
        }

        public void SetMovePoints(Transform[] pointsTransform)
        {
            _movePoints = pointsTransform;
        }

        public void RunMovement()
        {
            _currentMovePointIndex++;
            _movement.SetDestination(_movePoints[_currentMovePointIndex].position);
        }

        private void OnDestination(bool isDestintation)
        {
            if (_currentMovePointIndex < _movePoints.Length)
            {
                RunMovement();

            }
        }

        private void OnDisable()
        {
            _movement.OnDestination -= OnDestination;
        }
    }

}
