using Events;
using UnityEngine;

namespace Views
{
    public class WayBottomView : MonoBehaviour
    {
        private void OnCollisionEnter(Collision other)
        {
            if (other.transform.CompareTag("Player"))
            {
                GameEvents.LevelFail();
            }
        }
    }
}