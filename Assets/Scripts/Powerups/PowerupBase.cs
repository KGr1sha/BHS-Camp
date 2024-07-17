using UnityEngine;

namespace BHSCamp
{
    public abstract class PowerupBase : MonoBehaviour
    {
        public abstract void Apply(GameObject target);
    } 
}