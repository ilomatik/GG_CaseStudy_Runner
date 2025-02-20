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
        private int      _wayCount;
        
        public GameController(GameView view)
        {
            _data = new GameData();
            _view = view;
        }
        
        public void LoadLevel(int wayCount, CinemachineVirtualCamera cineMachineCameraCharacterFollower, CinemachineVirtualCamera cineMachineCameraLevelEnd)
        {
            _wayCount = wayCount;
            
            _data.ResetState();
            _view.SetCineMachineCamera(cineMachineCameraCharacterFollower, cineMachineCameraLevelEnd);
            _view.StartLevel(_wayCount);
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
            _data.IncreaseLevelWayCount();
            
            if (_data.LevelWayCount == _wayCount)
            {
                _view.SetCanSpawnNextWay(false);
            }
        }
        
        private void LevelSuccess()
        {
            _data.LevelSuccess();
            _view.OnLevelSuccess();
        }
        
        private void LevelFail()
        {
            _data.LevelFail();
            _view.OnLevelFail();
        }
    }
}