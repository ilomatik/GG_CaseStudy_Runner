using Cinemachine;
using Datas;
using Events;
using UnityEngine;
using Views;

namespace Controllers
{
    public class GameController
    {
        private GameData _data;
        private GameView _view;
        
        public GameController(GameView view)
        {
            _data = new GameData();
            _view = view;
        }
        
        public void LoadLevel(CinemachineVirtualCamera cineMachineCamera)
        {
            Debug.Log("Load Level");
            _view.SetCineMachineCamera(cineMachineCamera);
            _view.StartLevel();
        }
        
        public void SubscribeEvents()
        { 
            ViewEvents.OnTouchScreen += OnTouchScreen;
        }
        
        public void UnsubscribeEvents()
        {
            ViewEvents.OnTouchScreen -= OnTouchScreen;
        }
        
        private void OnTouchScreen()
        {
            _view.OnTouchScreen();
        }
        
        private void LevelSuccess()
        {
            Debug.Log("Level Success");
            _view.FinishLevel();
        }
        
        private void LevelFail()
        {
            Debug.Log("Level Fail");
            _view.FinishLevel();
        }
    }
}