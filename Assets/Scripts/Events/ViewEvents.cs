using System;
using UnityEngine;

namespace Events
{
    public static class ViewEvents
    {
        public static Action OnTouchScreen;
        public static Action OnWayCutSuccess;
        
        public static Action<Vector3> OnCharacterLeftStep;
        public static Action<Vector3> OnCharacterRightStep;
        public static Action<Vector3> OnLevelSuccess;
        
        public static Action<Vector3, Vector3, Action> OnWayCuttingParticle;

        public static void TouchScreen()   { OnTouchScreen?.Invoke(); }
        public static void WayCutSuccess() { OnWayCutSuccess?.Invoke(); }
        
        public static void CharacterLeftStep(Vector3 position)  { OnCharacterLeftStep?.Invoke(position); }
        public static void CharacterRightStep(Vector3 position) { OnCharacterRightStep?.Invoke(position); }
        public static void LevelSuccess(Vector3 position)       { OnLevelSuccess?.Invoke(position); }
        
        public static void WayCuttingParticle(Vector3 spawnPosition, Vector3 targetPosition, Action stopAction)
        {
            OnWayCuttingParticle?.Invoke(spawnPosition, targetPosition, stopAction);
        }
    }
}