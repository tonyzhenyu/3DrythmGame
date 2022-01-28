using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ZY{
    [System.Serializable]
    public class PropsFloatingAnima : IPropsAnimator
    {
        float speed = 1f;
        Vector3 startpoint = Vector3.zero;
        public PropsFloatingAnima(float speed,Vector3 startpoint)
        {
            this.speed = speed;
            this.startpoint = startpoint;
        }
        public void Animate(Transform t)
        {
            t.localPosition = new Vector3(t.localPosition.x, .5f * Mathf.Sin(Time.time * speed) + startpoint.y, t.localPosition.z);
        }
    }

}
