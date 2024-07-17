using UnityEngine;
using System;
using System.Collections;

namespace BHSCamp
{
    public class ActionOnTimer : MonoBehaviour
    {
        public void ActionAfterTime(Action action, float time)
        {
            StartCoroutine(Timer(action, time));
        }

        private IEnumerator Timer(Action action, float time)
        {
            yield return new WaitForSeconds(time);
            action();
        }
    }
}