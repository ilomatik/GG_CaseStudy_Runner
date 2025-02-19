using System;
using DG.Tweening;
using Enums;
using UnityEngine;

namespace Views
{
    public class CharacterView : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        
        private float _moveSpeed;
        private bool  _isMoving;
        
        public void SetSpeed(float speed)
        {
            _moveSpeed = speed;
        }
        
        public void MoveTo(Vector3 position)
        {
            transform.DOMove(position, _moveSpeed);
        }
        
        public void SetState(CharacterState state)
        {
            switch (state)
            {
                case CharacterState.Running:
                    _animator.SetTrigger("isRunning");
                    break;
                case CharacterState.Dancing:
                    _animator.SetTrigger("isDancing");
                    break;
                default:
                    throw new ArgumentOutOfRangeException(nameof(state), state, null);
            }
        }
    }
}