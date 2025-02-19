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
        
        public void LoadLevel()
        {
            Debug.Log("Load Level");
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
    }
}