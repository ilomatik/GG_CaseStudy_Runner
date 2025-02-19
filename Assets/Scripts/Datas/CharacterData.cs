using Enums;

namespace Datas
{
    public class CharacterData
    {
        public CharacterState State { get; private set; }
        
        public void SetState(CharacterState state)
        {
            State = state;
        }
        
        public void ResetState()
        {
            State = CharacterState.Idle;
        }
    }
}