using UnityEngine;

namespace BHSCamp
{
    public interface IMove
    {
        public void SetVelocity(Vector2 direction, float speed);
    }
}