using UnityEngine;
using UnityEngine.Events;

namespace ZY
{
    [RequireComponent(typeof(Collider))]
    public class PropsController : MonoBehaviour
    {
        [Header("Spawn")]public UnityEvent OnSpawnHandler;
        [Header("Destroy")]public UnityEvent OndestroyHandler;
        public string contactTag = "Player";
        public float detectAccuracy;

        void Start()
        {
            //spawn tell GameManager
            OnSpawnHandler?.Invoke();
        }
        private void OnTriggerEnter(Collider other)
        {
            if (other.gameObject.tag == contactTag)
            {
                detectAccuracy = Vector3.Distance(other.transform.position, transform.position);
                if (detectAccuracy > 1)
                {
                    detectAccuracy = 1;
                }
                detectAccuracy = 1 - detectAccuracy;
                Debug.Log(detectAccuracy);
                Destroy(gameObject);
            }
        }
        private void OnTriggerStay(Collider other)
        {
            
        }
        private void OnDestroy()
        {
            OndestroyHandler?.Invoke();
            OndestroyHandler.RemoveAllListeners();
            OnSpawnHandler.RemoveAllListeners();
        }
        public void OnGenerate(GameObject origin)
        {
            GameObject genobj = Instantiate(origin, transform.position, Quaternion.identity, new ToolsFunction().FindOrCreate("EffectPool"));
            Destroy(genobj, 1f);
        }
        public void DestoryDirectObj(Object @object) => Destroy(@object);

    }
}
