using UnityEngine;

namespace Views
{
    public class LevelEndPlatform : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Player"))
            {
                Events.GameEvents.LevelSuccess();
            }
        }
    }
}