using UnityEngine;
using UnityEngine.Rendering;

namespace ZY
{
    public class CameraFllow : MonoBehaviour
    {
        #region Properties
        [SerializeField]private Transform target;
        private Vector3 speed;
        private Quaternion myQuaternion;

        [SerializeField] private Vector3 offset = new Vector3(0, 7.5f, -5);
        [SerializeField,Range(0,0.5f)] private float smoothtime = .1f;

        private Vector3 zoom = Vector3.zero;
        #endregion

        void Start()
        {

            myQuaternion = transform.rotation;
        }

        void LateUpdate()
        {
            Track();
        }
        #region Tracking
        void Track()
        {

            Vector3 current;
            Vector3 targetV;

            current = transform.position;

            targetV = new Vector3(0, target.position.y, target.position.z) + Vector3.back + offset + zoom;

            transform.position = Vector3.SmoothDamp(current, targetV, ref speed, smoothtime);
            
        }
        #endregion

    }

}
