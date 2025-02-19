using Enums;

namespace Datas
{
    public class GameData
    {
        public GameState State    { get; private set; }
        public int       WayCount { get; private set; }
        
        public GameData()
        {
            State = GameState.InGame;
            WayCount = 0;
        }
        
        public void IncreaseWayCount()
        {
            WayCount++;
        }
        
        public void ResetWayCount()
        {
            WayCount = 0;
        }
        
        public void SetState(GameState state)
        {
            State = state;
        }
        
        public void ResetState()
        {
            State = GameState.InGame;
        }
        
        public void LevelSuccess()
        {
            State = GameState.LevelSuccess;
        }
        
        public void LevelFail()
        {
            State = GameState.LevelFail;
        }
    }
}