using UnityEngine;

namespace ScriptableObjects
{
    [CreateAssetMenu(fileName = "GameVariables", menuName = "ScriptableObjects/GameVariables", order = 1)]
    public class GameVariables : ScriptableObject
    {
        [SerializeField] private float _characterMoveSpeed;
        [SerializeField] private float _wayMoveDuration;
        [SerializeField] private float _wayMoveDistance;
        [SerializeField] private int   _levelWayCount;
        
        public float CharacterMoveSpeed => _characterMoveSpeed;
        public float WayMoveDuration    => _wayMoveDuration;
        public float WayMoveDistance    => _wayMoveDistance;
        public int   LevelWayCount      => _levelWayCount;
    }
}