using UnityEngine;

namespace PathCreation.Examples
{
    public class PathFollower : MonoBehaviour
    {
        [SerializeField] private PathCreator _pathCreator;
        [SerializeField] private EndOfPathInstruction _endOfPathInstruction;

        private float _speed;
        private float _distanceTravelled;

        public void SetMovementSpeed(float speed)
        {
            _speed = speed;
        }

        private void OnEnable()
        {
            _pathCreator.pathUpdated += OnPathChanged;
        }

        private void OnDisable()
        {            
            _pathCreator.pathUpdated -= OnPathChanged;
        }

        private void Update()
        {
            if (_pathCreator != null)
            {
                _distanceTravelled += _speed * Time.deltaTime;
                transform.position = _pathCreator.path.GetPointAtDistance(_distanceTravelled, _endOfPathInstruction);
                transform.rotation = _pathCreator.path.GetRotationAtDistance(_distanceTravelled, _endOfPathInstruction);
            }
        }

        private void OnPathChanged()
        {
            _distanceTravelled = _pathCreator.path.GetClosestDistanceAlongPath(transform.position);
        }
    }
}
