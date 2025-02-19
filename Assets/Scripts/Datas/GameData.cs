using Enums;

namespace Datas
{
    public class GameData
    {
        public GameState State    { get; private set; }
        
        public GameData()
        {
            State = GameState.InGame;
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