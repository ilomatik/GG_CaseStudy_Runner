using Events;
using UnityEngine;

namespace Managers
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField] private GameObject _characterStepParticle;
        [SerializeField] private GameObject _levelEndParticle;
        
        public void SubscribeEvents()
        {
            ViewEvents.OnCharacterLeftStep  += PlayCharacterLeftStepParticle;
            ViewEvents.OnCharacterRightStep += PlayCharacterRightStepParticle;
            ViewEvents.OnLevelSuccess       += PlayLevelEndParticle;
        }
        
        public void UnsubscribeEvents()
        {
            ViewEvents.OnCharacterLeftStep  -= PlayCharacterLeftStepParticle;
            ViewEvents.OnCharacterRightStep -= PlayCharacterRightStepParticle;
            ViewEvents.OnLevelSuccess       -= PlayLevelEndParticle;
        }

        private void PlayCharacterLeftStepParticle(Vector3 position)
        {
            if (_characterStepParticle == null) return;

            Instantiate(_characterStepParticle, position, Quaternion.identity);
        }
        
        private void PlayCharacterRightStepParticle(Vector3 position)
        {
            if (_characterStepParticle == null) return;
            
            Instantiate(_characterStepParticle, position, Quaternion.identity);
        }
        
        private void PlayLevelEndParticle(Vector3 position)
        {
            if (_levelEndParticle == null) return;

            Instantiate(_levelEndParticle, position, Quaternion.identity);
        }
    }
}