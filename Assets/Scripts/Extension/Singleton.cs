
using UnityEngine;

public class Singleton<T> : MonoBehaviour where T:MonoBehaviour
{
    private static T _instance;

    public static T Instance
    {
        get
        {
            _instance = (T)FindObjectOfType(typeof(T));
            if (_instance == null)
            {
                GameObject obj = new GameObject();
                obj.AddComponent<T>();
                obj.name = typeof(T).ToString();
            }
            return _instance;
        }

    }
}
