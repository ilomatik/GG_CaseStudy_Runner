using System;
using Controllers;
using Events;
using UnityEngine;
using Views;

namespace  Managers
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] private GameObject _gameView;

        private GameController _gameController;
        
        private void Start()
        {
            GameView view = Instantiate(_gameView).GetComponent<GameView>();
            
            _gameController = new GameController(view);
            _gameController.SubscribeEvents();
            _gameController.LoadLevel();
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