using UnityEngine;

namespace Code.Utils
{
    public class Singleton<T> : MonoBehaviour where T : Singleton<T>
    {
        public static bool HasInstance => _instance != null;
        public static T Instance 
        {
            get
            {
                if (_instance == null)
                {
                    var instances = FindObjectsByType<T>(FindObjectsInactive.Include, FindObjectsSortMode.None);

                    if (instances.Length == 0)
                    {
                        
                    }
                    else if (instances.Length == 1)
                    {
                        _instance = instances[0];
                    }
                    else
                    {
                        Debug.LogWarning($"2 instance found for {instances[0].name}, check and delete one of them!");
                        _instance = instances[0];
                    }
                }

                return _instance;
            }
            private set
            {
                _instance = value;
            }
        }
        
        private static T _instance;
        
        protected virtual void Awake()
        {
            if (Instance != null && Instance != this)
            {
                Debug.LogWarning($"2nd copy found of {name}, destroying one of them!");
                if (Application.isPlaying)
                {
                    Destroy(gameObject);
                }
                return;
            }
        }
    }
}