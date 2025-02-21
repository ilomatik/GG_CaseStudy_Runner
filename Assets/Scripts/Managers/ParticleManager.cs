using System;
using DG.Tweening;
using Events;
using UnityEngine;

namespace Managers
{
    public class ParticleManager : MonoBehaviour
    {
        [SerializeField] private GameObject _characterStepParticle;
        [SerializeField] private GameObject _levelEndParticle;
        [SerializeField] private GameObject _wayCuttingParticle;
        
        public void SubscribeEvents()
        {
            ViewEvents.OnCharacterLeftStep  += PlayCharacterLeftStepParticle;
            ViewEvents.OnCharacterRightStep += PlayCharacterRightStepParticle;
            ViewEvents.OnLevelSuccess       += PlayLevelEndParticle;
            ViewEvents.OnWayCuttingParticle += PlayWayCuttingParticle;
        }
        
        public void UnsubscribeEvents()
        {
            ViewEvents.OnCharacterLeftStep  -= PlayCharacterLeftStepParticle;
            ViewEvents.OnCharacterRightStep -= PlayCharacterRightStepParticle;
            ViewEvents.OnLevelSuccess       -= PlayLevelEndParticle;
            ViewEvents.OnWayCuttingParticle -= PlayWayCuttingParticle;
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
        
        private void PlayWayCuttingParticle(Vector3 spawnPosition, Vector3 targetPosition, Action stopAction)
        {
            if (_wayCuttingParticle == null) return;

            GameObject cuttingParticle = Instantiate(_wayCuttingParticle, spawnPosition, Quaternion.identity);
            
            cuttingParticle.transform.DOMove(targetPosition, 2f).OnComplete(() =>
            {
                stopAction?.Invoke();
                Destroy(cuttingParticle);
            });
        }
    }
}