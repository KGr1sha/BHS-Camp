using UnityEngine;

namespace BHSCamp
{
    public class Projectile : MonoBehaviour
    {
        [SerializeField] private float _speed;
        private Rigidbody2D _body;

        //STEP 8: Сделайте так, чтобы снаряд уничтожался при столкновении с любым физическим объектом
        // (для уничтожения объекта используйте Destroy(gameObject);)

        // STEP 6: Реализуйте данный метод, чтобы он задавал направление полета снаряду
        public void SetDirection(Vector2 direction)
        {
        }
    }
}