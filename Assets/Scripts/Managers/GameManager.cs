using System;
using Cinemachine;
using Controllers;
using Events;
using UnityEngine;
using Views;

namespace  Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject               _gameView;
        [SerializeField] private CinemachineVirtualCamera _cineMachineCameraCharacterFollower;
        [SerializeField] private CinemachineVirtualCamera _cineMachineCameraLevelEnd;

        private GameController _gameController;
        
        private void Start()
        {
            GameView view = Instantiate(_gameView).GetComponent<GameView>();
            
            _gameController = new GameController(view);
            _gameController.SubscribeEvents();
            _gameController.LoadLevel(_cineMachineCameraCharacterFollower, _cineMachineCameraLevelEnd);
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