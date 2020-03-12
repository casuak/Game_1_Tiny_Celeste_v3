using System;
using System.Collections;
using UnityEngine;

namespace TinyCeleste._01_Framework
{
    public class EntityComponent : MonoBehaviour
    {
        public T GetComponentNotNull<T>()
        {
            var cpt = GetComponent<T>();
            if (cpt == null)
                throw new Exception("Component" + typeof(T) + " Not Found");
            return cpt;
        }

        protected virtual void AfterFixedUpdate()
        {
            
        }

        protected void DoAfterFixedUpdate()
        {
            StartCoroutine(Co_AfterFixedUpdate());
        }

        private IEnumerator Co_AfterFixedUpdate()
        {
            yield return new WaitForFixedUpdate();
            AfterFixedUpdate();
        }
    }
}