using DG.Tweening;
using UnityEngine;

namespace Views
{
    public class CharacterView : MonoBehaviour
    {
        public void MoveTo(Vector3 position)
        {
            transform.DOMove(position, 1.0f);
        }
    }
}