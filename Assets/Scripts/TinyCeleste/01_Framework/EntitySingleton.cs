using UnityEngine;

namespace TinyCeleste._01_Framework
{
    public class EntitySingleton<T> : Entity where T : EntitySingleton<T>
    {
        public static T Instance { get; private set; }

        protected virtual void Awake()
        {
            if (Instance != null)
            {
                Debug.LogError("Singleton instantiate failed.");
                Destroy(gameObject);
            }
            else
            {
                Instance = this as T;
            }
        }

        private void OnDestroy()
        {
            if (Instance == this)
                Instance = null;
        }
    }
}