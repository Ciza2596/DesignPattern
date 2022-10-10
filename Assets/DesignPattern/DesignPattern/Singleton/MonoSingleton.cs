using UnityEngine;

namespace DesignPattern
{
    public abstract class MonoSingleton<T> : MonoBehaviour where T : MonoBehaviour
    {
        //private variable
        private static T _instance;
        private static object _lock = new object();
        private static bool _applicationIsQuitting = false;
        
        //public variable
        public static bool HasInstance => _instance != null;
        
        
        //unity callBack
        private void OnDestroy()
        {
            _applicationIsQuitting = true;
        }
        
        
        //public method
        public static T Instance
        {
            get
            {
                if (_applicationIsQuitting)
                {
                    Debug.LogWarning(
                        $"[MonoSingleton::Instance] Instance already destroyed on application quit. Won't create again returning null. Type : {typeof(T)}");
                    return null;
                }

                lock (_lock)
                {
                    if (_instance == null)
                    {
                        _instance = (T)FindObjectOfType(typeof(T));

                        if (FindObjectsOfType(typeof(T)).Length > 1)
                        {
                            Debug.LogError(
                                $"[MonoSingleton::Instance] Something went really wrong there should never be more than 1 singleton! Reopening the scene might fix it. Type : {typeof(T)}");
                            return _instance;
                        }

                        if (_instance == null)
                        {
                            GameObject singleton = new GameObject();
                            _instance = singleton.AddComponent<T>();
                            singleton.name = $"[MonoSingleton::Instance:{typeof(T).Name}]";

                            DontDestroyOnLoad(singleton);

                            Debug.Log(
                                $"[MonoSingleton::Instance] An instance is created in the scene. Type : {typeof(T)}");
                        }
                    }

                    return _instance;
                }
            }
        }
    }
}