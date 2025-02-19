using UnityEngine;

namespace Views
{
    public class LevelEndPlatform : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            Debug.Log("Collision Enter " + other.transform.name);
            if (other.transform.CompareTag("Player"))
            {
                Events.GameEvents.LevelSuccess();
            }
        }
    }
}