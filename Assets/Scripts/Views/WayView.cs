using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class WayView : MonoBehaviour
    {
        private float _moveSpeed;
        
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
    }
}