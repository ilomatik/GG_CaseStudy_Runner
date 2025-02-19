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
        
        public void LoadLevel(CinemachineVirtualCamera cineMachineCameraCharacterFollower, CinemachineVirtualCamera cineMachineCameraLevelEnd)
        {
            Debug.Log("Load Level");
            _data.ResetState();
            _data.ResetWayCount();
            _view.SetCineMachineCamera(cineMachineCameraCharacterFollower, cineMachineCameraLevelEnd);
            _view.StartLevel();
        }
        
        public void SubscribeEvents()
        { 
            ViewEvents.OnTouchScreen   += OnTouchScreen;
            ViewEvents.OnWayCutSuccess += OnWayCutSuccess;
            
            GameEvents.OnLevelSuccess += LevelSuccess;
            GameEvents.OnLevelFail    += LevelFail;
        }
        
        public void UnsubscribeEvents()
        {
            ViewEvents.OnTouchScreen   -= OnTouchScreen;
            ViewEvents.OnWayCutSuccess -= OnWayCutSuccess;
            
            GameEvents.OnLevelSuccess -= LevelSuccess;
            GameEvents.OnLevelFail    -= LevelFail;
        }
        
        private void OnTouchScreen()
        {
            _view.OnTouchScreen();
        }
        
        private void OnWayCutSuccess()
        {
            _data.IncreaseWayCount();
            
            if (_data.WayCount >= 5)
            {
                GameEvents.LevelSuccess();
            }
        }
        
        private void LevelSuccess()
        {
            Debug.Log("Level Success");
            _data.LevelSuccess();
            _view.FinishLevel();
        }
        
        private void LevelFail()
        {
            Debug.Log("Level Fail");
            _data.LevelFail();
            _view.FinishLevel();
        }
    }
}