using System;

namespace Events
{
    public static class GameEvents
    {
        public static Action OnLevelSuccess;
        public static Action OnLevelFail;
        
        public static void LevelSuccess() { OnLevelSuccess?.Invoke(); }
        public static void LevelFail()    { OnLevelFail?.Invoke(); }
    }
}