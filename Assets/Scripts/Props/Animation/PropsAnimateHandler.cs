using UnityEngine;
namespace ZY
{
    public class PropsAnimateHandler : MonoBehaviour
    {
        IPropsAnimator propsAnimator;
        public float animateSpeed = .1f;

        private void Awake()
        {
            propsAnimator = new PropsFloatingAnima(animateSpeed, transform.position);
        }
        
        void Update()
        {
            propsAnimator.Animate(transform);
        }
    }
}
