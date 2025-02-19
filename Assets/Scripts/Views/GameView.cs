using Cinemachine;
using DG.Tweening;
using Enums;
using Events;
using ScriptableObjects;
using UnityEngine;

namespace Views
{
    public class GameView : MonoBehaviour
    {
        [SerializeField] private GameObject _characterPrefab;
        [SerializeField] private GameObject _wayObject;
        [SerializeField] private GameObject _levelEndPlatform;
        [SerializeField] private Transform  _characterParent;
        [SerializeField] private Transform  _wayParent;

        private GameVariables            _gameVariables;
        private WayView                  _previousWay;
        private WayView                  _currentWay;
        private CharacterView            _character;
        private GameObject               _levelEndPlatformObject;
        private bool                     _spawnRight = true;
        private bool                     _canMove = true;
        private bool                     _isFinished;
        private float                    _overlap;
        private float                    _wayMoveDistance;
        private Tween                    _moveTween;
        private Tween                    _cameraTween;
        private Transform                _levelEndCameraParent;
        private CinemachineVirtualCamera _cineMachineCameraCharacterFollower;
        private CinemachineVirtualCamera _cineMachineCameraLevelEnd;
        
        public void Initialize(GameVariables gameVariables)
        {
            _gameVariables = gameVariables;

            _wayMoveDistance = _gameVariables.WayMoveDistance;
        }

        public void StartLevel(int wayCount)
        {
            float wayLocalZScale = _wayObject.transform.localScale.z;
            
            _character              = Instantiate(_characterPrefab, 
                                                  new Vector3(0f, 0.5f, 0f),
                                                  Quaternion.identity, 
                                                  _characterParent)
                                                  .GetComponent<CharacterView>();
            _currentWay             = Instantiate(_wayObject, 
                                                  new Vector3(-2f, 0f, wayLocalZScale), 
                                                  Quaternion.identity, 
                                                  _wayParent)
                                                  .GetComponent<WayView>();
            _levelEndPlatformObject = Instantiate(_levelEndPlatform, 
                                                  new Vector3(0f, 0f, wayCount * wayLocalZScale), 
                                                  Quaternion.identity);
            _previousWay            = _currentWay;
            
            _cineMachineCameraCharacterFollower.Follow = _character.transform;
            _cineMachineCameraCharacterFollower.LookAt = _character.transform;
            
            _cineMachineCameraCharacterFollower.gameObject.SetActive(true);
            _cineMachineCameraLevelEnd         .gameObject.SetActive(false);

            if (_cameraTween != null)
            {
                _cameraTween.Kill();
                _cameraTween = null;
            }
            
            _character.SetSpeed(_gameVariables.CharacterMoveSpeed);
            _levelEndCameraParent.SetParent(_characterParent);
            _currentWay.SetMoveSpeed(_gameVariables.WayMoveDuration);
            
            MoveCurrentWay();
        }

        public void FinishLevel()
        {
            _canMove    = false;
            _isFinished = true;
            _moveTween.Kill();
            
            SetCharacterState(CharacterState.Dancing);
            RotateCameraAroundCharacter();
        }
        
        public void SetCineMachineCamera(CinemachineVirtualCamera cineMachineCameraCharacterFollower, CinemachineVirtualCamera cineMachineCameraLevelEnd)
        {
            _cineMachineCameraCharacterFollower = cineMachineCameraCharacterFollower;
            _cineMachineCameraLevelEnd          = cineMachineCameraLevelEnd;
            
            _levelEndCameraParent = cineMachineCameraLevelEnd.transform.parent;
        }

        #region Way Cutting

        public void OnTouchScreen()
        {
            if (!_canMove)    return;
            if (!CutObject()) return;
            if (_isFinished)  return;
                
            SpawnNextWay();
            MoveCurrentWay();
        }
        
        private void MoveCurrentWay()
        {
            if (_moveTween != null)
            {
                _moveTween.Kill();
                _moveTween = null;
            }

            float nextWayX = _spawnRight ? _wayMoveDistance : -_wayMoveDistance;
            
            _moveTween = _currentWay.MoveWay(-nextWayX);
        }

        private bool CutObject()
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
                float   overlapCenterX    = Mathf.Max(currentMinX, previousMinX) + _overlap / 2;
                Vector3 remainingPosition = new Vector3(overlapCenterX, currentBounds.center.y, currentBounds.center.z);
                Vector3 remainingSize     = new Vector3(_overlap, currentBounds.size.y, currentBounds.size.z);

                _currentWay.transform.position   = remainingPosition;
                _currentWay.transform.localScale = remainingSize;

                _previousWay = _currentWay;

                _moveTween.Kill();

                // Create non-overlapped object
                float nonOverlapSize = currentBounds.size.x - _overlap;
                
                if (nonOverlapSize > 0)
                {
                    float nonOverlapCenterX    = (currentMinX < previousMinX) ? currentMinX + nonOverlapSize / 2 : currentMaxX - nonOverlapSize / 2;
                    Vector3 nonOverlapPosition = new Vector3(nonOverlapCenterX, currentBounds.center.y, currentBounds.center.z);
                    Vector3 nonOverlapScale    = new Vector3(nonOverlapSize, currentBounds.size.y, currentBounds.size.z);

                    GameObject nonOverlapObject = Instantiate(_wayObject, nonOverlapPosition, Quaternion.identity, _wayParent);
                    nonOverlapObject.transform.localScale = nonOverlapScale;
                    nonOverlapObject.layer = 7; // Set the layer to "CuttedWay" layer

                    // Apply gravity and destroy after delay
                    Rigidbody rb = nonOverlapObject.AddComponent<Rigidbody>();
                    Destroy(nonOverlapObject, 2.0f); // Adjust the delay as needed
                }

                // Move character to the top center of the current way
                Vector3 characterPosition = new Vector3(remainingPosition.x, remainingPosition.y + remainingSize.y / 2, remainingPosition.z);
                _character.MoveTo(characterPosition);
                
                ViewEvents.WayCutSuccess();
            }
            else
            {
                Debug.Log("No overlap, game over!");
                _canMove = false;
                _isFinished = true;
                _moveTween.Kill();
            }

            return _overlap > 0;
        }

        private void SpawnNextWay()
        {
            float nextWayX = _spawnRight ? _wayMoveDistance : -_wayMoveDistance;
            _currentWay    = Instantiate(_wayObject, _wayParent).GetComponent<WayView>();
            
            _currentWay.SetMoveSpeed(_gameVariables.WayMoveDuration);
            
            _currentWay.transform.position   = new Vector3(nextWayX, _previousWay.transform.position.y, _previousWay.transform.position.z + _previousWay.transform.localScale.z);
            _currentWay.transform.localScale = new Vector3(_overlap, _currentWay.transform.localScale.y, _currentWay.transform.localScale.z);
            
            _spawnRight = !_spawnRight; 
        }
        
        #endregion

        #region Character

        public void SetCharacterState(CharacterState state)
        {
            _character.SetState(state);
        }

        #endregion

        private void RotateCameraAroundCharacter()
        {
            _cineMachineCameraCharacterFollower.gameObject.SetActive(false);
            _cineMachineCameraLevelEnd         .gameObject.SetActive(true);
            
            _levelEndCameraParent.localPosition = _character.transform.localPosition;
            
            _cameraTween = _levelEndCameraParent.DOLocalRotate(new Vector3(0, 360, 0), 5.0f, RotateMode.FastBeyond360)
                                                .SetEase(Ease.Linear)
                                                .SetLoops(-1, LoopType.Restart);
        }
    }
}