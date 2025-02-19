using System;

namespace Events
{
    public static class ViewEvents
    {
        public static Action OnTouchScreen;
        
        public static void TouchScreen() { OnTouchScreen?.Invoke(); }
    }
}