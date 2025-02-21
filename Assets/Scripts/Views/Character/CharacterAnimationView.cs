using Events;
using UnityEngine;

public class CharacterAnimationView : MonoBehaviour
{
    [SerializeField] private Transform  _leftFoot;
    [SerializeField] private Transform  _rightFoot;
        
    public void PlayLeftStepParticle()
    {
        ViewEvents.CharacterLeftStep(_leftFoot.position);
    }
        
    public void PlayRightStepParticle()
    {
        ViewEvents.CharacterRightStep(_rightFoot.position);
    }
}