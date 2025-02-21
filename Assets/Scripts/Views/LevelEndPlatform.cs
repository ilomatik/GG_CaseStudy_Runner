using Events;
using UnityEngine;

namespace Views
{
    public class LevelEndPlatform : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Player"))
            {
                GameEvents.LevelSuccess();
                ViewEvents.LevelSuccess(transform.position + Vector3.up);
            }
        }
    }
}