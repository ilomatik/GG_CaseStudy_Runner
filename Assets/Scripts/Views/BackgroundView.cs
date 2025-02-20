using System.Collections.Generic;
using System.Threading.Tasks;
using Events;
using UnityEngine;

namespace Views
{
    public class BackgroundView : MonoBehaviour
    {
        private List<GameObject> _backgroundObjects;

        private void Start()
        {
            _backgroundObjects = new List<GameObject>();
            
            AddBackgroundObjects();
            
            GameEvents.OnLevelSuccess += DropBackgroundObjects;
            GameEvents.OnLevelFail    += DropBackgroundObjects;
        }

        private void OnDestroy()
        {
            GameEvents.OnLevelSuccess -= DropBackgroundObjects;
            GameEvents.OnLevelFail    -= DropBackgroundObjects;
        }

        private void AddBackgroundObjects()
        {
            int childCount = transform.childCount;
            
            for (int i = 0; i < childCount; i++)
            {
                _backgroundObjects.Add(transform.GetChild(i).gameObject);
            }
        }
        
        private async void DropBackgroundObjects()
        {
            foreach (GameObject backgroundObject in _backgroundObjects)
            {
                backgroundObject.GetComponent<Rigidbody>().useGravity = true;
                
                await Task.Delay(50);
            }
            
            await Task.Delay(3000);
            
            Destroy(gameObject);
        }
    }
}