using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class WayView : MonoBehaviour
    {
        private float     _moveSpeed;
        private Rigidbody _rigidbody;
        
        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
        }
        
        public void SetMoveSpeed(float moveSpeed)
        {
            _moveSpeed = moveSpeed;
        }
        
        public Tween MoveWay(float targetX)
        {
            return transform.DOMoveX(-targetX, _moveSpeed)
                            .SetEase(Ease.Linear)
                            .SetLoops(-1, LoopType.Yoyo);
        }
        
        public void TurnOffGravity()
        {
            _rigidbody.useGravity = false;
        }
        
        public void TurnOnGravity()
        {
            _rigidbody.useGravity = true;
            _rigidbody.constraints = RigidbodyConstraints.None;
            
            DOVirtual.DelayedCall(5f, () => Destroy(gameObject));
        }
    }
}