using System;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Views
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _followTransform;
        
        public Transform FollowTransform => _followTransform;
        
        private float _moveSpeed;
        private bool  _isMoving;
        
        private readonly string IsRunning = "isRunning";
        private readonly string IsDancing = "isDancing";
        
        public void SetSpeed(float speed)
        {
            _moveSpeed = speed;
        }

        private void Update()
        {
            if (_isMoving)
            {
                transform.position += Vector3.forward * (_moveSpeed * Time.deltaTime);
            }
        }
        
        public void MoveX(float x)
        {
            transform.DOMoveX(x, _moveSpeed);
        }
        
        public void SetState(CharacterState state)
        {
            switch (state)
            {
                case CharacterState.Running:
                    _isMoving = true;
                    _animator.SetBool(IsRunning, true);
                    _animator.SetBool(IsDancing, false);
                    break;
                case CharacterState.Dancing:
                    _isMoving = false;
                    _animator.SetBool(IsRunning, false);
                    _animator.SetBool(IsDancing, true);
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}