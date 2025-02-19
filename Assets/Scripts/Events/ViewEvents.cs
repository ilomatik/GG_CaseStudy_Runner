using System;

namespace Events
{
    public static class ViewEvents
    {
        public static Action OnTouchScreen;
        public static Action OnWayCutSuccess;
        
        public static void TouchScreen()   { OnTouchScreen?.Invoke(); }
        public static void WayCutSuccess() { OnWayCutSuccess?.Invoke(); }
    }
}