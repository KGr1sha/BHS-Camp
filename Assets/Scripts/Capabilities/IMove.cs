using UnityEngine;

namespace BHSCamp
{
    public interface IMove
    {
        public void SetDirection(Vector2 direction, float speed);
    }
}