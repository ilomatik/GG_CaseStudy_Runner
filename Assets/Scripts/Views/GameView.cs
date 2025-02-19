using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject _wayObject;
        [SerializeField] private Transform  _wayParent;
        [SerializeField] private float      _moveSpeed = 2.0f;

        private GameObject _previousWay;
        private GameObject _currentWay;
        private bool       _isMovingRight = true;
        private bool       _canMove = true;
        private float      _overlap;
        private Tween      _moveTween;

        private void Start()
        {
            _currentWay = Instantiate(_wayObject, _wayParent);
            _previousWay = _currentWay;
            MoveCurrentWay();
        }

        public void OnTouchScreen()
        {
            if (_canMove)
            {
                CutObject();
                SpawnNextWay();
                MoveCurrentWay();
            }
        }

        private void MoveCurrentWay()
        {
            float targetX = _isMovingRight ? 3.0f : -3.0f;
            _moveTween = _currentWay.transform.DOMoveX(targetX, _moveSpeed).SetLoops(-1, LoopType.Yoyo).SetEase(Ease.Linear);
        }

        private void CutObject()
        {
            Bounds currentBounds  = _currentWay.GetComponent<Renderer>().bounds;
            Bounds previousBounds = _previousWay.GetComponent<Renderer>().bounds;

            float currentMinX  = currentBounds.min.x;
            float currentMaxX  = currentBounds.max.x;
            float previousMinX = previousBounds.min.x;
            float previousMaxX = previousBounds.max.x;

            _overlap = Mathf.Min(currentMaxX, previousMaxX) - Mathf.Max(currentMinX, previousMinX);

            if (_overlap > 0)
            {
                float overlapCenterX = Mathf.Max(currentMinX, previousMinX) + _overlap / 2;
                Vector3 remainingPosition = new Vector3(overlapCenterX, currentBounds.center.y, currentBounds.center.z);
                Vector3 remainingSize = new Vector3(_overlap, currentBounds.size.y, currentBounds.size.z);

                _currentWay.transform.position = remainingPosition;
                _currentWay.transform.localScale = remainingSize;

                _previousWay = _currentWay;
                
                _moveTween.Kill();
                
                // Create non-overlapped object
                float nonOverlapSize = currentBounds.size.x - _overlap;
                if (nonOverlapSize > 0)
                {
                    float nonOverlapCenterX = (currentMinX < previousMinX) ? currentMinX + nonOverlapSize / 2 : currentMaxX - nonOverlapSize / 2;
                    Vector3 nonOverlapPosition = new Vector3(nonOverlapCenterX, currentBounds.center.y, currentBounds.center.z);
                    Vector3 nonOverlapScale = new Vector3(nonOverlapSize, currentBounds.size.y, currentBounds.size.z);

                    GameObject nonOverlapObject = Instantiate(_wayObject, nonOverlapPosition, Quaternion.identity, _wayParent);
                    nonOverlapObject.transform.localScale = nonOverlapScale;

                    // Apply gravity and destroy after delay
                    Rigidbody rb = nonOverlapObject.AddComponent<Rigidbody>();
                    Destroy(nonOverlapObject, 2.0f); // Adjust the delay as needed
                }
            }
            else
            {
                Debug.Log("No overlap, game over!");
                _canMove = false;
                _moveTween.Kill();
            }
        }

        private void SpawnNextWay()
        {
            float nextWayX = _currentWay.transform.position.x;
            _currentWay = Instantiate(_wayObject, _wayParent);
            _currentWay.transform.position = new Vector3(nextWayX, _previousWay.transform.position.y, _previousWay.transform.position.z + _previousWay.transform.localScale.z);
            _currentWay.transform.localScale = new Vector3(_overlap, _currentWay.transform.localScale.y, _currentWay.transform.localScale.z);
        }
    }
}