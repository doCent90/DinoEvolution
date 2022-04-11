using System;
using UnityEngine;

namespace RunnerMovementSystem.Examples
{
    public class MouseInput : MonoBehaviour
    {
        [SerializeField] private MovementSystem _roadMovement;
        [SerializeField] private StartTrigger _startTrigger;
        [SerializeField] private float _sensitivity = 0.01f;

        private Vector3 _mousePosition;
        private float _saveOffset;
        private bool _hasStartReached = false;

        public float CurrentOffset { get; private set; }
        public bool IsMoved { get; private set; }

        public event Action Moved;
        public event Action Stoped;

        private void OnEnable()
        {
            _startTrigger.Started += OnStarted;
            _roadMovement.PathChanged += OnPathChanged;
        }

        private void OnDisable()
        {
            _startTrigger.Started -= OnStarted;
            _roadMovement.PathChanged -= OnPathChanged;
        }

        private void OnPathChanged(PathSegment _)
        {
            _saveOffset = _roadMovement.Offset;
            _mousePosition = Input.mousePosition;
        }

        private void OnStarted()
        {
            _hasStartReached = true;
        }

        private void Update()
        {
            if (_roadMovement.enabled == false)
                return;

            if (_hasStartReached == false)
            {
                _roadMovement.MoveForward();
                IsMoved = true;
                return;
            }

            if (Input.GetMouseButtonDown(0))
            {
                _saveOffset = _roadMovement.Offset;
                _mousePosition = Input.mousePosition;
                IsMoved = true;
            }

            if (Input.GetMouseButton(0))
            {
                Vector3 offset = Input.mousePosition - _mousePosition;
                CurrentOffset = _saveOffset + offset.x * _sensitivity;
                _roadMovement.SetOffset(CurrentOffset);
                _roadMovement.MoveForward();
                Moved?.Invoke();
            }

            if (Input.GetMouseButtonUp(0))
            {
                IsMoved = false;
                Stoped?.Invoke();
            }
        }
    }
}
