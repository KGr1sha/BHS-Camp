using UnityEngine;

namespace BHSCamp
{
    //интерфейс для взаимодействиями с компонентами, отвечающими за передвижение
    public interface IMove
    {
        void SetVelocity(Vector2 direction, float speed);
        void SetVelocityMultiplier(float multiplier);
    }
}