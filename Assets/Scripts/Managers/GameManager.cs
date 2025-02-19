using Cinemachine;
using Controllers;
using Events;
using ScriptableObjects;
using UnityEngine;
using Views;

namespace  Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameVariables            _gameVariables;
        [SerializeField] private GameObject               _gameView;
        [SerializeField] private CinemachineVirtualCamera _cineMachineCameraCharacterFollower;
        [SerializeField] private CinemachineVirtualCamera _cineMachineCameraLevelEnd;

        private GameController _gameController;
        
        private void Start()
        {
            GameView view = Instantiate(_gameView).GetComponent<GameView>();
            
            view.Initialize(_gameVariables);
            
            _gameController = new GameController(view);
            _gameController.SubscribeEvents();
            _gameController.LoadLevel(_gameVariables.LevelWayCount, _cineMachineCameraCharacterFollower, _cineMachineCameraLevelEnd);
        }

        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                ViewEvents.TouchScreen();
            }
        }

        private void OnDestroy()
        {
            _gameController.UnsubscribeEvents();
        }
    }
}